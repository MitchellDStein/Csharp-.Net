using System;
using Packt.Shared;
using static System.Console;

namespace PeopleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var john = new Person("John", "Earth");
            john.Name = "John Doe";
            john.DateOfBirth = new DateTime(1992, 6, 19);

            WriteLine(format: "{0} was born on {1:dddd, d MMMM yyyy}", // dddd: name of day, d: number of day, MMMM: name of month, yyyy: full year
                arg0: john.Name,
                arg1: john.DateOfBirth);

            WriteLine(format: "{0} was born on {1:dd MMM yy}",
                arg0: john.Name,
                arg1: john.DateOfBirth);
            WriteLine();

            // ==========Enums==========
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
            WriteLine();

            // ==========Objects with Lists==========
            // calling from the public List<Person> in PacktLibrary Person.cs
            john.Children.Add(new Person("Jane", john.HomePlanet));
            john.Children.Add(new Person("Jack", john.HomePlanet));
            WriteLine($"{john.Name} has {john.Children.Count} children:");
            for (int child = 0; child < john.Children.Count; child++)
            {
                WriteLine($"    {john.Children[child].Name}");
            }
            WriteLine();

            // ==========Using static values==========
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
            WriteLine();


            // ==========Contrcutors==========
            // Contrcutors with arguments
            // using PersonArgs.cs
            var mike = new Person("Mike", "Mars");
            WriteLine(format: "{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}.",
                arg0: mike.Name,
                arg1: mike.HomePlanet,
                arg2: mike.Instantiated);

            // using methods
            mike.WriteToConsole();
            WriteLine(mike.GetOrigin());
            WriteLine();

            // ==========Tuples==========
            // using tuples
            (string, int) fruit = mike.GetFruit();
            WriteLine($"{fruit.Item1}, {fruit.Item2} there are.");

            var fruitNamed = mike.GetNamedFruit();
            WriteLine($"There are {fruitNamed.Number} {fruitNamed.Name}.");

            // variable use of tuples
            var thing1 = ("George", 4);
            WriteLine($"{thing1.Item1} has {thing1.Item2} Children.");

            var thing2 = (john.Name, john.Children.Count);
            WriteLine($"{thing2.Name} has {thing2.Count} children.");

            // Deconstructing tuples
            (string fruitName, int fruitNumber) = mike.GetFruit();
            WriteLine($"Deconstructed: {fruitName}, {fruitNumber}");
            // taking the values held in GetFruit() and assigning the tuple to the new variables
            WriteLine();


            // ==========Param Methods and Overloading==========
            WriteLine(mike.SayHello());
            WriteLine(mike.SayHello("John"));
            WriteLine();

            // ==========Optional Parameters==========
            WriteLine(mike.OptionalParameters()); // will output default values in PersonArgs.cs
            WriteLine(mike.OptionalParameters("Jump!", 98.5)); // overwrites the default values

            // delcaring some parameters can be skipped if you name the parameters based on the method
            WriteLine(mike.OptionalParameters(number: 29.1, command: "Hide!")); // parameter calues can be named when declaring them
            WriteLine(mike.OptionalParameters("Walk!", active: false));
            WriteLine();

            // ==========Controlling How Parameters are Passed==========
            // by value (default)(in), ref(in-out), and as an out parameter(out)
            int a = 10;
            int b = 20;
            int c = 30;

            WriteLine($"Before: a = {a}, b = {b}, c = {c}.");
            mike.PassingParameters(a, ref b, out c);
            WriteLine($"After: a = {a}, b = {b}, c = {c}.");
            // When passing a value as a parameter by default, the current VALUE
            // is passed, not the variable itself. a before and after is 10.
            // When passing y as a reference to b, b increments as y increments.
            // When using an out, the value in z overwrites the value of c.

            // In C# 7.0 we can simpify code that uses the out variable:
            int d = 10;
            int e = 20;
            WriteLine($"Before: d = {d}, e = {e}, f doesn't exist yet!");
            // simplified syntax for the out parameter
            mike.PassingParameters(d, ref e, out int f);
            WriteLine($"After: d = {d}, e = {e}, f ={f}");
            WriteLine();

            // ==========Using Properties in C# 6+==========
            // Using PersonAutoGen.cs
            var sam = new Person
            {
                Name = "Sam",
                DateOfBirth = new DateTime(1928, 1, 27)
            };

            WriteLine(sam.Origin);
            WriteLine(sam.Greeting);
            WriteLine(sam.Age);

            // settable properties
            sam.FavoriteIceCream = "Chocolate Fudge";
            WriteLine($"Sam's favorite ice cream is {sam.FavoriteIceCream}.");

            sam.FavoritePrimaryColor = "red";
            WriteLine($"Sam's favorite color is {sam.FavoritePrimaryColor}.");


            // ==========Indexers==========
            sam.Children.Add(new Person { Name = "Charlie" });
            sam.Children.Add(new Person { Name = "Ella" });

            WriteLine($"Sam's first child is {sam.Children[0].Name}");
            WriteLine($"Sam's second child is {sam.Children[1].Name}");
            WriteLine($"Sam's first child is {sam[0].Name}");
            WriteLine($"Sam's second child is {sam[1].Name}");
        }
    }
}
