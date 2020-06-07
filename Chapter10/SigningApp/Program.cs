using System;
using CryptographyLib;
using System.Security.Cryptography;
using static System.Console;

namespace SigningApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter some text to sign: ");
            string userInput = ReadLine();

            var signature = Protector.GenerateSignature(userInput);
            WriteLine($"Signature: {signature}");
            WriteLine("\nPublic key used to check signature:");
            WriteLine(Protector.PublicKey);

            if (Protector.ValidateSignature(userInput, signature))
            {
                WriteLine("\nCorrect! Signature is valid.");
            }
            else
            {
                WriteLine("\nInvalid signature.");
            }


            // simulate a fake signature by replacing the first character with an X
            // var fakeSignature = signature.Replace(signature[0], 'X');
            // if (Protector.ValidateSignature(userInput, fakeSignature))
            // {
            //     WriteLine("\nCorrect! Signature is valid.");
            // }
            // else
            // {
            //     WriteLine($"\nInvalid signature: {fakeSignature}");
            // }
        }
    }
}
