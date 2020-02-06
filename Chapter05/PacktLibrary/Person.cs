using System;
using System.Collections.Generic;
using static System.Console;

namespace Packt.Shared
{
    public class Person : System.Object
    {
        public string Name;
        public DateTime DateOfBirth;
        public WondersOfTheAncientWorld FavoriteWonder;
        public WondersOfTheAncientWorld BucketList;
        public List<Person> Children = new List<Person>(); // List<Person> is read as "List of person"

        // when making a constant value that will never change use const
        public const string Species = "Homo Sapien";

        // a better option for never changing fields is to make them read-only
        // you can also combine static and readonly for shared fields that never change
        public readonly string HomePlanet = "Earth";

        // initializing fields with constructors
        public readonly DateTime Instantiated;

        // constructiors
        public Person()
        {
            // set default values for fields
            // including read-only fields
            Name = "Unknown";
            HomePlanet = "Unknown";
            Instantiated = DateTime.Now;
        }
    }
}
