﻿using System;
using System.Collections.Generic;
using static System.Console;

namespace Working_With_Dictionaries
{
    class Program
    {
        static void Main(string[] args)
        {
            // working with Dictionaries
            WriteLine("== Working with Dictionaries ==");            
            var keywords = new Dictionary<string, string>();
            keywords.Add("int", "32-bit integer data type");
            keywords.Add("long", "64-bit integer data type");
            keywords.Add("float", "Single precision floating point number");
            WriteLine("Keywords and their definitions");
            foreach (KeyValuePair<string, string> item in keywords)
            {
                WriteLine($" {item.Key}: {item.Value}");
            }
            WriteLine($"The definition of long is {keywords["long"]}");
        }
    }
}
