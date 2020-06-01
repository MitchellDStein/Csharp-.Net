using System;
using System.Text.RegularExpressions;
using static System.Console;

namespace Working_With_Regex
{
    class Program
    {
        static void Main(string[] args)
        {
            // Checking for digits entered as text
            WriteLine("\n== Checking for digits entered as text == ");
            Write("Enter your age: ");
            string input = ReadLine();
            var ageChecker = new Regex(@"\d");      // @ disables escape characters (e.g: "/") while /d means digit
            if (ageChecker.IsMatch(input))
            {
                WriteLine("Thank you! \t\t <-- @\"\\d\" does not check before and after digits");
            }
            else
            {
                WriteLine($"This is not a valid age: {input} \t\t <-- @\"\\d\" does not check before and after digits");
            }
            var ageChecker2 = new Regex(@"^\d+$");
            if (ageChecker2.IsMatch(input))
            {
                WriteLine("Thank you! \t\t <-- @\"^\\d+$\" does check before and after digits");
            }
            else
            {
                WriteLine($"This is not a valid age: {input} \t\t <-- @\"^\\d+$\" does check before and after digits");
            }
            //regex cheat sheet
            // ^    Start of input                  $       End of input
            // \d   A single digit                  \D      A single NON-digit
            // \w   Whitespace                      \W      NON-whitespace
            // [A-Za-z0-9]  Range(s) of characters  \^      ^ (caret) character
            // [aeiou]      Set of characters       [^aeiou]    NOT in a set of characters
            // .    Any single character            \.      . (dot) character
            // +    One or more                     ?       One or none
            // {3}  Exactly three                   {3,5}   Three to five
            // {3,} At least three                  {,3}    Up to three


            // Splitting a complex comma-separated string
            WriteLine("\n== Splitting a complex comma-separated string ==");
            string films = "\"Monsters, Inc.\",\"I, Tonya\",\"Lock, Stock and Two Smoking Barrels\"";
            var csv = new Regex("(?:^|,)(?=[^\"]|(\")?)\"?((?(1)[^\"]*|[^,\"]*))\"?(?=,|$)");
            MatchCollection filmsSmart = csv.Matches(films);
            WriteLine("Regex splitting:");
            foreach (Match film in filmsSmart)
            {
                WriteLine(film.Groups[2].Value);
            }
        }
    }
}
