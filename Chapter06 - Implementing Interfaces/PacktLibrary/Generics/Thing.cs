// making types safely reusable with Generics
using System;

namespace Packt.Shared
{
    public class Thing
    {
        public object Data = default(object);
        public string Process(object input)
        {
            if (Data == input)
            {
                return "Data and input are the same.";
            }
            else
            {
                return "Data and input are NOT the same.";
            }
        }
        // thing is currently flexible because any type can be set for Data and input parameters.
        // We can fix that using Generics. See GenericThing class.
    }
}