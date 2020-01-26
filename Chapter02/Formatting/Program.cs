using System;
using static System.Console;

// becaise of the System.Console we do not need to specify the Console in the WriteLine

namespace Formatting
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfApples = 12;
            decimal pricePerApple = 0.35M; // M means decimal literal value

            WriteLine(
                format: "{0} apples costs {1:C}",
                arg0: numberOfApples,
                arg1: pricePerApple * numberOfApples);

            string formatted = string.Format(
                format: "{0} apples costs {1:C}",
                arg0: numberOfApples,
                arg1: pricePerApple * numberOfApples);

            //WriteToFile(formatted); // writes the string into a file
            WriteLine($"{formatted}");
            WriteLine($"{numberOfApples} apples costs {pricePerApple * numberOfApples:C}");


            string applesText = "Apples";
            int applesCount = 1234;
            string bananasText = "Bananas";
            int bananasCount = 56789;

            WriteLine(
                format: "{0,-8} {1,6:N0}",  // :N0 is a format meaning a number with no decimals
                arg0: "Name",               // and separated at each thousand by a comma. E.g: 23,000
                arg1: "Count");

            WriteLine(
                format: "{0,-8} {1,6:N0}",
                arg0: applesText,
                arg1: applesCount);

            WriteLine(
                format: "{0,-8} {1,6:N0}",
                arg0: bananasText,
                arg1: bananasCount);

            // left-align bananaText to a width of 8 character and right align count to 6 characters
            // return babanaCount as thousands separated by a comma.
            WriteLine($"{bananasText,-8} {bananasCount,6:N0}");


            // input and output
            Write("Press any key combinatoin: ");
            ConsoleKeyInfo key = ReadKey();
            WriteLine();
            WriteLine("Key: {0}, Char: {1}, Modifiers: {2}",
                arg0: key.Key,
                arg1: key.KeyChar,
                arg2: key.Modifiers);
        }
    }
}
