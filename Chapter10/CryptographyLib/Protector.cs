using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Xml.Linq;
using static System.Convert;

namespace CryptographyLib
{
    public static class Protector
    {
        // ========== START EncryptionApp requirements ==========
        private static readonly byte[] salt = Encoding.Unicode.GetBytes("7BANANAS");    // salt size must be at leasty 8 bytes, we will use 16.
        private static readonly int iterations = 2000;  // iterations must be at least 1000, we will use 2000.

        public static string Encrypt(string plainText, string password)
        {
            // set variables
            byte[] encryptedBytes;
            byte[] plainBytes = Encoding.Unicode.GetBytes(plainText);
            var aes = Aes.Create();
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);

            // set aes variables
            aes.Key = pbkdf2.GetBytes(32);      // set a 256-bit key
            aes.IV = pbkdf2.GetBytes(16);       // set a 128-bit key

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(plainBytes, 0, plainBytes.Length);
                }
                encryptedBytes = ms.ToArray();
            }
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string cryptoText, string password)
        {
            // set variables
            byte[] plainBytes;
            byte[] cryptoBytes = Convert.FromBase64String(cryptoText);
            var aes = Aes.Create();
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);

            // set aes variables
            aes.Key = pbkdf2.GetBytes(32);      // set a 256-bit key
            aes.IV = pbkdf2.GetBytes(16);       // set a 128-bit key

            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cryptoBytes, 0, cryptoBytes.Length);
                }
                plainBytes = ms.ToArray();
            }
            return Encoding.Unicode.GetString(plainBytes);
        }

        // Create Users to login
        private static Dictionary<string, User> Users = new Dictionary<string, User>();

        private static string SaltAndHashPassword(string password, string salt)
        {
            var sha = SHA256.Create();
            var saltedPassword = password + salt;
            return Convert.ToBase64String(sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));
        }

        public static User Register(string username, string password, string[] roles = null)
        {
            // generate random salt
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);        // create an array of length saltBytes with random values
            var saltText = Convert.ToBase64String(saltBytes);

            //generate the salted and hashed password
            var saltedHashedPassword = SaltAndHashPassword(password, saltText);

            var user = new User
            {
                Name = username,
                Salt = saltText,
                SaltedHashedPassword = saltedHashedPassword,
                Roles = roles,
            };
            Users.Add(user.Name, user);
            return user;
        }

        public static bool CheckPassword(string username, string password)
        {
            if (!Users.ContainsKey(username))
            {
                return false;
            }
            var user = Users[username];
            // re-generate the salted and hashed password
            var saltedhashedPassword = SaltAndHashPassword(password, user.Salt);
            return (saltedhashedPassword == user.SaltedHashedPassword);
        }
        // ========== END EncryptionApp ==========

        // ========== START SigningApp ==========
        public static string PublicKey;

        // serialize RSAParameters structure
        public static string ToXmlStringExt(this RSA rsa, bool includePrivateParameters)
        {
            var p = rsa.ExportParameters(includePrivateParameters);
            XElement xml;
            if (includePrivateParameters)
            {
                xml = new XElement("RSAKeyValue",
                    new XElement("Modulus", ToBase64String(p.Modulus)),
                    new XElement("Exponent", ToBase64String(p.Exponent)),
                    new XElement("P", ToBase64String(p.P)),
                    new XElement("Q", ToBase64String(p.Q)),
                    new XElement("DP", ToBase64String(p.DP)),
                    new XElement("DQ", ToBase64String(p.DQ)),
                    new XElement("InverseQ", ToBase64String(p.InverseQ))
                );
            }
            else
            {
                xml = new XElement("RSAKeyValue",
                    new XElement("Modulus", ToBase64String(p.Modulus)),
                    new XElement("Exponent", ToBase64String(p.Exponent)));
            }
            return xml?.ToString();
        }
        public static void FromXmlStringExt(this RSA rsa, string parametersAsXml)
        {
            var xml = XDocument.Parse(parametersAsXml);
            var root = xml.Element("RSAKeyValue");
            var rsaParams = new RSAParameters
            {
                Modulus = FromBase64String(root.Element("Modulus").Value),
                Exponent = FromBase64String(root.Element("Exponent").Value)
            };

            if (root.Element("P") != null)
            {
                rsaParams.P = FromBase64String(root.Element("P").Value);
                rsaParams.Q = FromBase64String(root.Element("Q").Value);
                rsaParams.DP = FromBase64String(root.Element("DP").Value);
                rsaParams.DQ = FromBase64String(root.Element("DQ").Value);
                rsaParams.InverseQ = FromBase64String(
                root.Element("InverseQ").Value);
            }
            rsa.ImportParameters(rsaParams);
        }

        public static string GenerateSignature(string data)
        {
            byte[] dataBytes = Encoding.Unicode.GetBytes(data);

            var sha = SHA256.Create();
            var rsa = RSA.Create();
            var hashedData = sha.ComputeHash(dataBytes);

            PublicKey = rsa.ToXmlStringExt(false); // exclude private key
            return ToBase64String(rsa.SignHash(hashedData, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
        }
        public static bool ValidateSignature(string data, string signature)
        {
            byte[] dataBytes = Encoding.Unicode.GetBytes(data);
            byte[] signatureBytes = FromBase64String(signature);

            var sha = SHA256.Create();
            var rsa = RSA.Create();
            var hashedData = sha.ComputeHash(dataBytes);

            rsa.FromXmlStringExt(PublicKey);
            return rsa.VerifyHash(hashedData, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
        // ========== END SigningApp ==========

        // ========== START Generating random numbers for cryptography ==========
        public static byte[] GetRandomKeyOrIV(int size)
        {
            var r = RandomNumberGenerator.Create();
            var data = new byte[size];
            r.GetNonZeroBytes(data);
            // data is an array filled with cryptographically strong random bytes
            return data;
        }
        // ========== END Generating random numbers for cryptography ==========

        // ========== START User Login Code ==========
        public static void LogIn(string username, string password)
        {
            if (CheckPassword(username, password))
            {
                var identity = new GenericIdentity(username, "MitchAuth");
                var principal  = new GenericPrincipal(identity, Users[username].Roles);
                System.Threading.Thread.CurrentPrincipal = principal;
            }
        }


    }

}
