using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using static System.Console;

namespace LinqWithObjects
{
    class Program
    {
        static void Output(IEnumerable<string> cohort, string description = "")
        {
            if (!string.IsNullOrEmpty(description))
            {
                WriteLine(description);
            }
            Write(" ");
            WriteLine(string.Join(", ", cohort.ToArray()));
        }

        static void Main(string[] args)
        {
            // var names = new string[] { "Michael", "Pam", "Jim", "Dwight", "Angela", "Kevin", "Toby", "Creed" };
            // LinqStringArray(names);
            // LambdaLinqStringArray(names);
            // LinqExceptionsArray();

            // used with Output method
            var cohort1 = new string[] { "Rachel", "Gareth", "Jonathan", "George" };
            var cohort2 = new string[] { "Jack", "Stephen", "Daniel", "Jack", "Jared" };
            var cohort3 = new string[] { "Declan", "Jack", "Jack", "Jasmine", "Connor" };
            Output(cohort1, "Cohort 1");
            Output(cohort2, "Cohort 2");
            Output(cohort3, "Cohort 3");
            WriteLine();
            Output(cohort2.Distinct(), "cohort2.Distinct():");
            WriteLine();
            Output(cohort2.Union(cohort3), "cohort2.Union(cohort3):");
            WriteLine();
            Output(cohort2.Concat(cohort3), "cohort2.Concat(cohort3):");
            WriteLine();
            Output(cohort2.Intersect(cohort3), "cohort2.Intersect(cohort3):");
            WriteLine();
            Output(cohort2.Except(cohort3), "cohort2.Except(cohort3):");
            WriteLine();
            Output(cohort1.Zip(cohort2, (c1, c2) => $"{c1} matched with {c2}"), "cohort1.Zip(cohort2):");
            // with zip, cohorts without a number will not show
        }

        /// <summary>
        /// Returns an array of strings matching the query
        /// Uses NameLongerThanFour method to return names > 4
        /// </summary>
        /// <param name="names">String Array</param>
        static void LinqStringArray(string[] names)
        {
            var query = names.Where(new Func<string, bool>(NameLongerThanFour));
            // for each string variable passed to the method,
            // the method must return a bool value. If the method returns true
            // it indicates that we should include the name/string in the results

            // == Simplifying the code by removing the explicit delegate instantiation = //
            var querySimplified = names.Where(NameLongerThanFour);


            foreach (string name in querySimplified)
            {
                WriteLine(name);
            }
        }
        static bool NameLongerThanFour(string name)
        {
            return name.Length > 4;
        }

        /// <summary>
        /// Uses Lambda Expressions to return the query.
        /// (query => query.length > 4)
        /// </summary>
        /// <param name="names">String Array</param>
        static void LambdaLinqStringArray(string[] names)
        {
            var lambdaQuery = names
                .Where(name => name.Length > 4) // return names greater than 4 in length 
                .OrderBy(name => name.Length)   // order the names by smallest first
                .ThenBy(name => name);          // then order names alphabetically

            foreach (string name in lambdaQuery)
            {
                WriteLine(name);
            }
        }

        /// <summary>
        /// This is to demonstrate sorting by different types
        /// </summary>
        static void LinqExceptionsArray()
        {
            var errors = new Exception[]{
                new ArgumentException(),
                new SystemException(),
                new IndexOutOfRangeException(),
                new InvalidOperationException(),
                new NullReferenceException(),
                new InvalidCastException(),
                new OverflowException(),
                new DivideByZeroException(),
                new ApplicationException()
            };

            var arithmeticErrors = errors.OfType<ArithmeticException>();    // returns all errors of type ArithmeticException
            foreach (var error in arithmeticErrors)
            {
                WriteLine(error);
            }
        }
    }
}
