// using while statements and other iterative methods

using System;
using static System.Console;

namespace IterationStatements
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 0;
            while (x < 10)
            {
                WriteLine(x);
                x++;
            }

            string password = string.Empty;
            // int i = 0;
            do
            {
                Write("Enter your password: ");
                password = ReadLine();
                // i++;
            } while (password != "Pa$$w0rd" && i < 10);
            // } while (password != "Pa$$w0rd" && i< 10);
            // if (i >= 10)
            // {
            //     WriteLine("Too many attempts");
            // }
            // else
            // {
            //     WriteLine("Correct!");
            // }


            // using for statments
            for (int y = 0; y <= 10; y++)
            {
                WriteLine(y);
            }

            // foreach statement
            string[] names = { "Adam", "Barry", "Charlie" };
            foreach (string name in names)
            {
                WriteLine($"{name} has {name.Length} characters");
            }
            // Foreach is similar to the following pseudocode:
            // IEnumerator e = names.GetEnumerator();
            // while (e.MoveNext())
            // {
            //     string name = (string)e.Current; // Current is read-only!
            //     WriteLine($"{name} has {name.Length} characters.");
            // }
        }
    }
}
