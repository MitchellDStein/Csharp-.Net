using System;
using System.Collections.Generic;

namespace Packt.Shared
{
    class ThinngsOfDefaults
    {
        public int Population;
        public DateTime When;
        public string Name;
        public List<Person> People;

        public ThinngsOfDefaults()
        {
            // C# 2.0 and later
            // Population = default(int);
            // When = default(DateTime);
            // Name = default(string);
            // People = default(List<Person>);

            // C# 7.1 and later
            Population = default;
            When = default;
            Name = default;
            People = default;
        }
    }
}