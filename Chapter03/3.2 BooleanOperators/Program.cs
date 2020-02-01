using System;
using static System.Console;

namespace BooleanOperators
{
    class Program
    {
        static void Main(string[] args)
        {
            bool a = true;
            bool b = false;
            WriteLine("a = true, b = false");
            WriteLine($"AND  | a     | b    ");
            WriteLine($"a    | {a & a,-5} | {a & b,-5}");
            WriteLine($"b    | {b & a,-5} | {b & b,-5}\n");
            WriteLine();
            WriteLine($"OR   | a     | b    ");
            WriteLine($"a    | {a | a,-5} | {a | b,-5}");
            WriteLine($"b    | {b | a,-5} | {b | b,-5}\n");

            WriteLine($"XOR  | a     | b    ");
            WriteLine($"a    | {a ^ a,-5} | {a ^ b,-5}");
            WriteLine($"b    | {b ^ a,-5} | {b ^ b,-5}\n");

            WriteLine($"a & DoStuff() = {a & DoStuff()}");
            WriteLine($"b & DoStuff() = {b & DoStuff()}\n");

            WriteLine($"a && DoStuff() = {a && DoStuff()}");
            WriteLine($"b && DoStuff() = {b && DoStuff()}"); // DoStuff does not run

        }

        private static bool DoStuff()
        {
            WriteLine("I am doing some stuff.");
            return true;
        }
    }
}
