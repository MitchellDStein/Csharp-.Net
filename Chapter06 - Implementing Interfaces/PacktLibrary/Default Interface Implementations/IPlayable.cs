// Defining interfaces with default implementations
using static System.Console;

namespace Packt.Shared
{
    public interface IPlayable
    {
        void Play();

        void Pause();

        // C#8.0 Allows an interfact to add new membets after release
        // as long as they have a default implementation.
        void Stop(){ // Will result in build errors CS8370 and CS8701 unless we edit the .csproj runtime to netstandard2.1"
            WriteLine("Default implementation of stop");
        }
    }
}