using System;
using static System.Console;

namespace Exercise3._2
{
    class Program
    {
        static void Main(string[] args)
        {
            // 3.2.0
            // the following loop will repeatedly loop through the possible values of the byte value type.
            try
            {
                checked
                {
                    int max = 500;
                    for (byte i = 0; i < max; i++)
                    {
                        Write($"{i}.");
                    }
                }
            }
            catch (OverflowException)
            {
                WriteLine("\nByte overflow encountered!");
            }
        }
    }
}
