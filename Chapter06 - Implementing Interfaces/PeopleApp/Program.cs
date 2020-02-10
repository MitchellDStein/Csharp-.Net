using System;
using Packt.Shared;
using static System.Console;

namespace PeopleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var harry = new Person { Name = "Harry" };
            var jill = new Person { Name = "Jill" };
            var mary = new Person { Name = "Mary" };

            // call instance method
            var baby1 = mary.ProcreateWith(harry);

            // call static method
            var baby2 = Person.Procreate(harry, jill);

            // call an operator
            var baby3 = harry * mary;

            WriteLine($"{harry.Name} has {harry.Children.Count} children.");
            WriteLine($"{mary.Name} has {mary.Children.Count} children.");
            WriteLine($"{jill.Name} has {jill.Children.Count} children.");

            WriteLine(
                format: "{0}'s first child is named \"{1}\".",
                arg0: harry.Name,
                arg1: harry.Children[0].Name);

            // using local function method
            WriteLine($"5! is {Person.Factorial(5)}");

            // using delegates
            harry.Shout = Harry_Shout;
            for (int i = 0; i < 4; i++)
            {
                harry.Poke();
            }
        }

        private static void Harry_Shout(object sender, EventArgs e) 
        // naming convention for methods that handle event are ObjectName_EventName
        {
            Person p = (Person)sender;
            WriteLine($"{p.Name} is this angry: {p.AngerLevel}.");
        }
    }
}
