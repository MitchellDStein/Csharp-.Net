using System;
using static System.Console;

namespace Excersise3._1
{
    class Program
    {
        static void Main(string[] args)
        {

            // 3.1.1 dividing an int by zero results in an error.
            try
            {
                int intByZero = 5;
                WriteLine($"Dividing int {intByZero} by zero: {intByZero / 0}.");
            }
            catch (System.Exception)
            {
                WriteLine("Caught an exception dividing an int by zero.\n");
            }

            //3.1.2 dividing a double by zero results in infinity.
            double doubleByZero = 5;
            WriteLine($"Dividing int {doubleByZero} by zero: {doubleByZero / 0}.\n");

            // 3.1.3 An int overflows to the most negative value it can become.
            int overflowInt = int.MaxValue;
            WriteLine($"Overflowing an int ({overflowInt}) results in {overflowInt + 1}.\n");

            // 3.1.4 the difference between ++y and y++ is that y++ increments after the value is used.
            int y = 3;
            WriteLine($"y = {y}.\nUsing x = ++y:");
            int x = ++y;
            WriteLine($"x = {x}, y = {y}");
            WriteLine($"y = {y}.\nUsing z = y++:");
            int z = y++;
            WriteLine($"z = {z}, y = {y}\n");

            // 3.1.5
            // Break terminates the closest enclosing loop.
            for (int i = 1; i <= 10; i++)
            {
                Write(i);
                if (i > 7)
                {
                    WriteLine(" - Breaking at 7/10");
                    break;
                }
                if (i == 10)
                {
                    // using break at 7 this WriteLine should never be reached.
                    WriteLine("Reached end of loop.");
                }
            }
            // The continue statement passes control to the next iteration of the enclosing loop.
            // anything between 'continue' and the end of the loop are skipped.
            for (int i = 0; i <= 10; i++)
            {
                if (i < 2 || i > 7)
                {
                    // only continue when the value is between 2 and 7.
                    continue;
                }
                Write(i);
            }
            Write(" - Continue between 2 and 7.\n");

            // return when used in a loop works similarly to break but will also exit Main();

            // 3.1.6 the parts of a for loop are:
            // for(initilizer; condition; iterator){body}

            // 3.1.7
            // = is an arithmetic operator meaning a value is equal to another.
            // == is an equality operator. == will return true if the value is the same as another.

            // 3.1.8
            // for(;true;); does compile and is a forever loop.

            // 3.1.9
            // _ represents default in the abbreviated switch expression format

            // 3.1.10
            // a foreach statement uses the IEnumerable interface.
        }
    }
}
