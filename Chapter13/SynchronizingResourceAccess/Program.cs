using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using static System.Console;

namespace SynchronizingResourceAccess
{
    class Program
    {
        static Random r = new Random();
        static string Message; // <- shared resource
        static object magicConch = new object();

        static int Counter; // another shared resource

        static void Main(string[] args)
        {
            WriteLine("Please wait for the tasks to complete.");
            Stopwatch watch = Stopwatch.StartNew();
            Task a = Task.Factory.StartNew(MethodA);
            Task b = Task.Factory.StartNew(MethodB);
            Task.WaitAll(new Task[] { a, b });
            WriteLine();
            WriteLine($"{Counter} string modifications.");
            WriteLine($"Results: {Message}.");
            WriteLine($"{watch.ElapsedMilliseconds:#,##0} elapsed milliseconds.");
        }

        static void MethodA()
        {
            try
            {
                Monitor.TryEnter(magicConch, TimeSpan.FromSeconds(15));
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(r.Next(2000));
                    Message += "A";
                    Interlocked.Increment(ref Counter);
                    Write("A");
                }
            }
            finally
            {
                Monitor.Exit(magicConch);
            }
        }

        static void MethodB()
        {
            try
            {
                Monitor.TryEnter(magicConch, TimeSpan.FromSeconds(15));
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(r.Next(2000));
                    Message += "B";
                    Interlocked.Increment(ref Counter);
                    Write("B");
                }
            }
            finally
            {
                Monitor.Exit(magicConch);
            }
        }
    }
}
