using System;
using static System.Console;
using static System.Convert;

namespace CastingConverting
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 10;
            double b = a; // an int can be safeny cast into a double
            WriteLine(b);

            double c = 9.8;
            // int d = c; // compiler gives an error for this line
            // you must use explicit casting
            int d = (int)c; // d is 9, losing the .8 portion
            WriteLine(d);

            // explicit casting must also be used to convert from
            // larger and smaller intergers such as long to int.
            long e = 10;
            int f = (int)e;
            WriteLine($"e is {e:N0} and f is {f:N0}");

            e = long.MaxValue;
            f = (int)e;
            WriteLine($"e is {e:N0} and f is {f:N0}");

            e = 5_000_000_000;
            f = (int)e;
            WriteLine($"e is {e:N0} and f is {f:N0}");


            // using System.Convert
            double g = 9.8;
            int h = ToInt32(g);
            WriteLine($"g is {g} and h is {h}");

            // testing if C# rounds up or down on .5
            double[] doubles = new[] { 9.49, 9.5, 9.51, 10.49, 10.5, 10.51 };
            foreach (double n in doubles)
            {
                WriteLine($"ToInt({n}) is {ToInt32(n)}");
            }

            // using MidpointRounding.AwayFromZero - more familiar. 0.5 rounds up
            foreach (double n in doubles)
            {
                WriteLine(format:
                "Math.Round({0}, 0, MidpointRounding.AwayFromZero) is {1}",
                arg0: n,
                arg1: Math.Round(value: n, digits: 0, mode: MidpointRounding.AwayFromZero));
            }

            // converting any type to a string
            int number = 12;
            WriteLine(number.ToString());

            bool boolean = true;
            WriteLine(boolean.ToString());

            DateTime now = DateTime.Now;
            WriteLine(now.ToString());

            object me = new object();
            WriteLine(me.ToString());

            // converyt from binary object to a string
            // used to convert binaryt like objects such as a video or image
            // mainly used to store or transmit using raw bits.

            byte[] binaryObject = new byte[128]; // allocate array of 128 bytes

            // populate array with random bytes
            (new Random()).NextBytes(binaryObject);
            WriteLine("binary Object as bytes:");

            for (int index = 0; index < binaryObject.Length; index++)
            {
                Write($"{binaryObject[index]:X} ");
            }
            WriteLine();
            // convert to Base64 string and output as text
            string encoded = Convert.ToBase64String(binaryObject);
            WriteLine($"Binary Object as Base64: {encoded}");


            // parsing from strings to numbers or dates and times
            int age = int.Parse("27");
            DateTime birthday = DateTime.Parse("4 July 1980");

            WriteLine($"I was born {age} years ago.");
            WriteLine($"My birthday is {birthday}");
            WriteLine($"My birthday is {birthday:D}");

            // errors can be thrown if the value to parse cannot become an int
            // int count = int.Parse("abc"); // Unhandled exception. System.FormatException: Input string was not in a correct format.
            // WriteLine(count);


            // to avoid errors you can use the TryParse method
            Write("How many eggs are there? ");
            int count;
            string input = Console.ReadLine();
            if (int.TryParse(input, out count)) // parse input to int and send value to count
            {
                WriteLine($"There are {count} eggs.");
            }
            else
            {
                WriteLine("I could not parse the input.");
            }
        }
    }
}
