using System;
using static System.Console;

namespace Working_with_Text
{
    class Program
    {
        static void Main(string[] args)
        {
            string city = "London";
            WriteLine($"{city} is {city.Length} characters long");
            WriteLine($"First char is {city[0]} and third is {city[2]}.");

            // splitting a string
            WriteLine("\n== Splitting a string ==");
            string cities = "Paris,Berlin,Madrid,New York";
            string[] citiesArray = cities.Split(',');
            foreach (string item in citiesArray)
            {
                WriteLine(item);
            }

            // getting part of a string
            WriteLine("\n== Getting part of a string ==");
            string fullName = "Alan Jones";
            int indexOfTheSpace = fullName.IndexOf(' ');
            string firstName = fullName.Substring(startIndex: 0, length: indexOfTheSpace);  // get all characters from beginning to space index
            string lastName = fullName.Substring(startIndex: indexOfTheSpace + 1);          // get all characters after space index (not including space)
            WriteLine($"Space index: {indexOfTheSpace}");
            WriteLine($"{lastName}, {firstName}");

            // Checking a string for content
            WriteLine("\n== Checking a string for content ==");
            string company = "Microsoft";
            bool startsWithM = company.StartsWith("M");
            bool containsN = company.Contains("N");
            WriteLine($"Starts with M: {startsWithM}, contains an N:{ containsN}");

            // Joining string members
            WriteLine("\n== Joining string members ==");
            string recombined = string.Join(" => ", citiesArray);   //take an array of string values and combine them to form a string
            WriteLine(recombined);

            // Formatting string members
            WriteLine("\n== Formatting string members ==");
            string fruit = "Apples";
            decimal price = 0.39M;
            DateTime when = DateTime.Today;
            WriteLine($"{fruit} cost {price:C} on {when:dddd}.");
            WriteLine(string.Format("{0} cost {1:C} on {2:dddd}.", fruit, price, when));

        }
    }
}
