using System;

namespace DevKit.Logging
{
    [Flags]
    public enum LogTarget
    {
        Console,
        File,
        Network
    }
}
