using System;
using System.Collections.Generic;
using static System.Console;

using System.Collections.Immutable;

namespace Working_With_Lists
{
    class Program
    {
        static void Main(string[] args)
        {
            // working with lists
            WriteLine("== Working with Lists ==");
            var cities = new List<string>();                            // creating and adding to lists
            cities.Add("London");
            cities.Add("Paris");
            cities.Add("Milan");
            WriteLine("Initial list: ");
            foreach (string city in cities)
            {
                WriteLine($" {city}");
            }
            WriteLine($"The first city is {cities[0]}.");
            WriteLine($"The last city is {cities[cities.Count - 1]}.");
            cities.Insert(0, "Sydney");                                 // inserting
            WriteLine("After inserting Sydney at index 0: ");
            foreach (string city in cities)
            {
                WriteLine($" {city}");
            }
            cities.RemoveAt(1);                                         // removing
            cities.Remove("Milan");
            WriteLine("After removing two cities: ");
            foreach (string city in cities)
            {
                WriteLine($" {city}");
            }

            // using immutable
            // immutable means it cannot be added to or remove elements from
            WriteLine("\n== Using immutable ==");
            var immutableCities = cities.ToImmutableList();
            var newList = immutableCities.Add("Rio");
            Write("Immutable list of cities:");
            foreach (string city in immutableCities)
            {
                Write($" {city}");
            }
            WriteLine();
            Write("New list of cities:");
            foreach (string city in newList)
            {
                Write($" {city}");
            }
            WriteLine();
        }
    }
}
