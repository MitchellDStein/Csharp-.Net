// to be used with the IPlayable.cs class
using static System.Console;

namespace Packt.Shared
{
    public class DvdPlayer : IPlayable
    {
        public void Pause()
        {
            WriteLine("Pausing DVD player.");
        }
        public void Play()
        {
            WriteLine("Playing DVD Player.");
        }
    }
}