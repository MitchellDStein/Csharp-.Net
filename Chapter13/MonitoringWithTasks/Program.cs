using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace MonitoringWithTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            // RunSync();
            // RunAsync();

            WriteLine("Passing the result of one task as an input into another.");
            var runContinuationTask = Task.Factory
                .StartNew(CallWebService)
                .ContinueWith(previousTask => CallStoredProcedure(previousTask.Result));
            WriteLine($"Result: {runContinuationTask.Result}");
        }

        static void RunSync()
        {
            WriteLine("Running methods synchronously on one thread.");
            var timer = Stopwatch.StartNew();
            MethodA(); MethodB(); MethodC();
        }
        static void RunAsync()
        {
            WriteLine("Running methods asynchronously on multiple threads");
            var timer = Stopwatch.StartNew();

            Task taskA = new Task(MethodA);
            taskA.Start();

            Task taskB = Task.Factory.StartNew(MethodB);

            Task taskC = Task.Run(new Action(MethodC));

            Task[] tasks = { taskA, taskB, taskC };
            Task.WaitAll(tasks);

            WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");
        }
        static void MethodA()
        {
            WriteLine("Starting Method A...");
            Thread.Sleep(3_000); // simulating 3 seconds of work
            WriteLine("Finished Method A");
        }
        static void MethodB()
        {
            WriteLine("Starting Method B...");
            Thread.Sleep(2_000); // simulate two seconds of work
            WriteLine("Finished Method B.");
        }
        static void MethodC()
        {
            WriteLine("Starting Method C...");
            Thread.Sleep(1_000); // simulate one second of work
            WriteLine("Finished Method C.");
        }

        static decimal CallWebService()
        {
            WriteLine("Starting call to web service...");
            Thread.Sleep((new Random()).Next(2000, 4000));
            WriteLine("Finished call to web service.");
            return 89.99M;
        }
        static string CallStoredProcedure(decimal amount)
        {
            WriteLine("Starting call to stored procedure...");
            Thread.Sleep((new Random()).Next(2000, 4000));
            WriteLine("Finished call to stored procedure.");
            return $"12 products cost more than {amount:C}.";
        }
    }
}
