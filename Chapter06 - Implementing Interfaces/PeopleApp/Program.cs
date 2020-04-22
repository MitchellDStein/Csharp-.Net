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
            harry.Shout += Harry_Shout;
            for (int i = 0; i < 4; i++)
            {
                harry.Poke();
            }

            // using interfaces
            Person[] people = {
                new Person{Name = "Simon"},
                new Person{Name = "George"},
                new Person{Name = "Adam"},
                new Person{Name = "Richard"}
            };

            WriteLine("Initial list of people:");
            foreach (var person in people)
            {
                WriteLine($"{person.Name}");
            }

            WriteLine("Use Person's IComparable implementatin to sort: ");
            Array.Sort(people);
            foreach (var person in people)
            {
                WriteLine($"{person.Name}");
            }

            // using PersonComparer.cs
            WriteLine("Use PersonComparer's IComparer implementatin to sort:");
            Array.Sort(people, new PersonComparer());
            foreach (var person in people)
            {
                WriteLine($"{person.Name}");
            }

            // Utilizing Thing.cs class for genetics
            var t1 = new Thing();
            t1.Data = 42;
            WriteLine($"Thing with an integer {t1.Process(42)}");

            var t2 = new Thing();
            t2.Data = "apple";
            WriteLine($"Thing with a string: {t2.Process("apple")}");

            // thing is currently flexible because any type can be set for Data and input parameters.
            // We can fix that using Generics. See GenericThing class.
            var gt1 = new GenericThing<int>();
            gt1.Data = 42;
            WriteLine($"GenericThing with an integer {gt1.Process(42)}");

            var gt2 = new GenericThing<string>();
            gt2.Data = "Banana";
            WriteLine($"GeneticThing with a string: {gt2.Process("Banana")}");

            // working with generic methods
            string number1 = "4";
            WriteLine("{0} squared is {1}.",
                arg0: number1,
                arg1: Squarer.Square<string>(number1));

            byte number2 = 3;
            WriteLine("{0} squared is {1}",
                arg0: number2,
                arg1: Squarer.Square(number2));


            // Working with Struct types
            var dv1 = new DisplacementVector(3, 5);
            var dv2 = new DisplacementVector(-2, 7);
            var dv3 = dv1 + dv2;
            WriteLine($"({dv1.X}, {dv1.Y}) + ({dv2.X},{dv2.Y}) = ({dv3.X},{dv3.Y})");


            // using class inherittance
            Employee john = new Employee
            {
                Name = "John Jones",
                DateOfBirth = new DateTime(1996, 10, 1)
            };

            john.WriteToConsole(); // using new WriteToConsole from Employee.cs
            john.EmployeeCode = "JJ1001";
            john.HireDate = new DateTime(2015, 11, 23);
            WriteLine($"{john.Name} was hired on: {john.HireDate:dd/MM/yy}.");

            WriteLine(john.ToString());

            Employee aliceInEmployee = new Employee { Name = "Alice", EmployeeCode = "AA332" };

            Person aliceInPerson = aliceInEmployee;

            aliceInEmployee.WriteToConsole();
            aliceInPerson.WriteToConsole();

            WriteLine(aliceInEmployee.ToString());
            WriteLine(aliceInPerson.ToString());


            // explicit casting
            // Employee explicitAlice = aliceInPerson; // fails as we cannot cast Person as Employee
            // Employee explicitAlice = (Employee)aliceInPerson; // this could lead to InvalidCastException
            // instead use a if statement
            if (aliceInPerson is Employee)
            {
                WriteLine($"{nameof(aliceInPerson)} IS an Employee");

                Employee explicitAlice = (Employee)aliceInPerson;
                    // safely do someting with explicitAlice
            }

            // you may also use AS to explicitly cast
            Employee aliceAsEmployee = aliceInPerson as Employee;

            if (aliceAsEmployee != null)
            {
                WriteLine($"{nameof(aliceInPerson)} AS an Employee");
                    // do something with aliceAsEmployee
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
