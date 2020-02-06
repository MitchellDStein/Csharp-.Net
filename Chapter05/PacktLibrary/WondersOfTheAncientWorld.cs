namespace Packt.Shared
{
    // enum types are used to store a limited set of selections or options
    [System.Flags] // We can combine multiple choices into a single value using flags.
    public enum WondersOfTheAncientWorld : byte
    {
        // We set the byte values so that when we select multiple, such as Giza and Rhodes
        // the value of that choice will be 0b_0010_0001, giving us a value of 33
        None                        = 0b_0000_0000, // i.e. 0
        GreatPyramidOfGiza          = 0b_0000_0001, // i.e. 1
        HangingGardensOfBabylon     = 0b_0000_0010, // i.e. 2
        StatueOfZeusAtOlympia       = 0b_0000_0100, // i.e. 4
        TempleOfArtemisAtEphesus    = 0b_0000_1000, // i.e. 8
        MausoleumAtHalicarnassus    = 0b_0001_0000, // i.e. 16
        ColossusOfRhodes            = 0b_0010_0000, // i.e. 32
        LighthouseOfAlexandria      = 0b_0100_0000  // i.e. 64
    }
}