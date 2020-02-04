using System;

namespace UnitTests
{
    public class Calculator
    {
        // using a deliberate bug
        public double Add(double a, double b)
        {
            return a * b;
        }
    }
}
