using System;
using CryptographyLib;
using System.Security.Cryptography;
using static System.Console;

namespace EncryptionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter a message that you want to encrypt: ");
            string message = ReadLine();

            Write("Enter a password: ");
            string password = ReadLine();

            string cryptoText = Protector.Encrypt(message, password);

            WriteLine($"Encrypted text: {cryptoText}");
            Write("Enter the password: ");
            string passwordVerify = ReadLine();

            try
            {
                string clearText = Protector.Decrypt(cryptoText, passwordVerify);
                WriteLine($"Decrypted text: {clearText}");
            }
            catch (CryptographicException ex)
            {
                WriteLine("{0}\nMore details: {1}",
                    "You entered the wrong password!",
                    ex.Message);
            }
            catch (Exception ex)
            {
                WriteLine("Non-cryptographic exception: {0}, {1}",
                    ex.GetType().Name,
                    ex.Message);
            }
        }
    }
}
