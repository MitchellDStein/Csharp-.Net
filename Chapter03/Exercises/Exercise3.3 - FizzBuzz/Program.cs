using System;
using static System.Console;

namespace Exercise3._3___FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i <= 100; i++)
            {
                if (i % 3 == 0 && i % 5 == 0)
                {
                    Write("FizzBuzz, ");
                }
                else if (i % 3 == 0)
                {
                    Write("Fizz, ");
                }
                else if (i % 5 == 0)
                {
                    Write("Buzz, ");
                }
                else
                {
                    Write($"{i}, ");
                }
            }
        }
    }
}
