using System;

namespace Exercise
{
    public class Circle : Shape
    {
        public override double Area
        {
            get
            {
                return Math.PI * Radius * Radius;
            }
        }

        public double Radius { get; set; }
    }
}