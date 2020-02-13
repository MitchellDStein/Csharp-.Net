// Working with generic methods
using System;
using System.Threading;

namespace Packt.Shared
{
    public static class Squarer // non generic
    {
        public static double Square<T>(T input) where T : IConvertible // genertic
        {
            // convert using the current culture
            double d = input.ToDouble(Thread.CurrentThread.CurrentCulture); // current culture specifies the region used by your computer

            return d * d;
        }
    }
}