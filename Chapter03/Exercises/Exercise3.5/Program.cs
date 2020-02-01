using System;
using static System.Console;

namespace Exercise3._5
{
    class Program
    {
        static void Main(string[] args)
        {
            // What are the values of x and y after the following statements execute?

            // x = 3
            // y = 2 + ++x
            // A: x = 4, y = 6
            // Check:
            int x = 3;
            int y = 2 + ++x;
            WriteLine($"#1: x = {x}, y = {y}");


            // x = 3 << 2;  - bitwise shift left
            // y = 10 >> 1; - bitwise shift right
            // 3 = 0000 0011, 10 = 0000 1010
            // A: 3 << 2 = 0000 1100 = 12,   10 >> 1 = 0000 0101 = 5
            // Check:
            x = 3 << 2;
            y = 10 >> 1;
            WriteLine($"#2: 3 << 2 = {x}");
            WriteLine($"    10 >> 1 = {y}");

            // x = 10 & 8;  - AND operator  (both are 1's)
            // y = 10 | 7;  - OR operator   (any are 1's)
            //  10 = 0000 1010     |  10 = 0000 1010
            //   8 = 0000 1000     |   7 = 0000 0111
            // AND = 0000 1000 = 8 |  OR = 0000 1111 = 15
            // Check:
            x = 10 & 8;
            y = 10 | 7;
            WriteLine($"#3: 10 & 8 = {x}");
            WriteLine($"    10 | 7 = {y}");
        }
    }
}
