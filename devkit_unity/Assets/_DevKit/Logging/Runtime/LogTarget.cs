using System;

namespace DevKit.Logging
{
    [Flags]
    public enum LogTarget
    {
        None = 0,
        Console = 1,
        File = 2,
        Network = 4,
        All = Console | File | Network
    }
}
