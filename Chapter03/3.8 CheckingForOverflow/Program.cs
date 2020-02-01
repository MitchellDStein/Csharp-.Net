using System;
using static System.Console;

namespace CheckingForOverflow
{
    class Program
    {
        static void Main(string[] args)
        {
            // try-catch block to catch and report the overflow error.
            try
            {
                // checked not allow a value to overflow
                checked
                {
                    int x = int.MaxValue - 1;   // 2147483646
                    WriteLine($"Initial x value: {x}");
                    x++;                        // 2147483647
                    WriteLine($"After incrementing: {x}");
                    x++;                        // -2147483648
                    WriteLine($"After incrementing: {x}");
                    x++;                        // -2147483647
                    WriteLine($"After incrementing: {x}");
                }
            }
            catch (OverflowException)
            {
                WriteLine("The code overflowed but I caught the exception");
            }

            // unchecked will allow for a value to overflow
            unchecked
            {
                int y = int.MaxValue + 1;   // -2147483648
                WriteLine($"Initial value: {y}");
                y--;                        // 2147483647
                WriteLine($"After decrementing: {y}");
                y--;                        // 2147483646
                WriteLine($"After decrementing: {y}");
            }
        }
    }
}
