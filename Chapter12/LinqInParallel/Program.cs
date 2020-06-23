using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace LinqInParallel
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Press 'ENTER' to start: ");
            ReadLine();
            var stopWatch = Stopwatch.StartNew();

            IEnumerable<int> numbers = Enumerable.Range(1, 200_000_000);

            // var squares = numbers.Select(number => number * number).ToArray();       // est. time = 592ms
            var squares = numbers.AsParallel().Select(number => number * number).ToArray();     // est. time = 1,716ms

            stopWatch.Stop();
            WriteLine("{0:#,##0} elapsed milliseconds.", stopWatch.ElapsedMilliseconds);
        }
    }
}
