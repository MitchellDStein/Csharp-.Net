using System;
using Packt.Shared;
using static System.Console;

namespace PeopleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var john = new Person();
            john.Name = "John Doe";
            john.DateOfBirth = new DateTime(1992, 6, 19);

            WriteLine(format: "{0} was born on {1:dddd, d MMMM yyyy}", // dddd: name of day, d: number of day, MMMM: name of month, yyyy: full year
                arg0: john.Name,
                arg1: john.DateOfBirth);

            WriteLine(format: "{0} was born on {1:dd MMM yy}",
                arg0: john.Name,
                arg1: john.DateOfBirth);

            // Using enums in PacktLibrary WondersOfTheAncientWorld.cs
            john.FavoriteWonder = WondersOfTheAncientWorld.HangingGardensOfBabylon;
            WriteLine(format: "{0}'s favorite wonder is {1}. It's integer is {2}",
                arg0: john.Name,
                arg1: john.FavoriteWonder,
                arg2: (int)john.FavoriteWonder); // enum valuesa are initialy stored as an int for efficiency.

            // enums can also be called and set using int values when useing the decoration [System.Flags] and inheriting from byte
            john.BucketList = WondersOfTheAncientWorld.ColossusOfRhodes | WondersOfTheAncientWorld.GreatPyramidOfGiza;
            WriteLine($"{john.Name}'s bucket list is {john.BucketList}");
            john.BucketList = (WondersOfTheAncientWorld)33;
            WriteLine($"{john.Name}'s bucket list is {john.BucketList}");


            // calling from the public List<Person> in PacktLibrary Person.cs
            john.Children.Add(new Person { Name = "Jane" });
            john.Children.Add(new Person { Name = "Jack" });
            WriteLine($"{john.Name} has {john.Children.Count} children:");
            for (int child = 0; child < john.Children.Count; child++)
            {
                WriteLine($"    {john.Children[child].Name}");
            }

            // using BankAccount.cs class
            BankAccount.InterestRate = 0.12M; // store a shared value, InterestRate is static

            var doeAccount = new BankAccount();
            doeAccount.AccountName = "Mr. Doe";
            doeAccount.Balance = 43_600;

            WriteLine(format: "{0} earned {1:C} interest.", // :C - currency format
                arg0: doeAccount.AccountName,
                arg1: doeAccount.Balance * BankAccount.InterestRate);

            var smithAccount = new BankAccount();
            smithAccount.AccountName = "Mr. Smith";
            smithAccount.Balance = 1_400;

            WriteLine(format: "{0} earned {1:C} interest.", // :C - currency format
                arg0: smithAccount.AccountName,
                arg1: smithAccount.Balance * BankAccount.InterestRate);

            // using const value from Person.cs
            WriteLine($"{john.Name} is a {Person.Species}. (We hope)");

            // using readonly value from Person.cs
            WriteLine($"{john.Name} is from {john.HomePlanet}. (If he is a Homo Sapien)");


            // using the constructors in Person.cs with only constructors
            var blankPerson = new Person();

            WriteLine(format: "{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.",
                arg0: blankPerson.Name,
                arg1: blankPerson.HomePlanet,
                arg2: blankPerson.Instantiated);


            // using the constructor with arguments in PersonArgs.cs
            var definetlyHuman = new PersonArgs("Nosrep Namuh", "Mars");
            WriteLine(format: "{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.",
                arg0: definetlyHuman.Name,
                arg1: definetlyHuman.HomePlanet,
                arg2: definetlyHuman.Instantiated);
        }
    }
}
