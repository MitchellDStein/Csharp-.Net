// Asks the user for two numbers in the range 0-255
// and then divides the first number by the second3
using System;
using static System.Console;

namespace Exercise3._4
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Enter a number from 0 to 255: ");
            string firstInput = ReadLine();
            Write("Enter another number between 0 and 255: ");
            string secondInput = ReadLine();

            try
            {
                byte firstInt = byte.Parse(firstInput);
                byte secondInt = byte.Parse(secondInput);
                WriteLine($"{firstInt} divided by {secondInt} is {firstInt/secondInt}");
            }
            catch (FormatException ex)
            {
                WriteLine($"{ex.GetType()} says {ex.Message}");
                return;
            }
            catch (OverflowException ex)
            {
                WriteLine($"{ex.GetType()} says {ex.Message}");
                return;
            }
        }
    }
}
