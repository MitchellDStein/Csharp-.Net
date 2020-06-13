// using a separate class to compare objects
using System.Collections.Generic;

namespace Packt.Shared
{
    public class PersonComparer : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            // Compate the Name lengths ...
            int result = x.Name.Length.CompareTo(y.Name.Length);

            // ... if they are equal
            if (result == 0)
            {
                // ... then compare by alphabetical order
                return x.Name.CompareTo(y.Name);
            }
            else
            {
                // ... otherwise compare by the lengths
                return result;
            }
        }
    }
}