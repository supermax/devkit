using System;

namespace DevKit.Logging
{
    [Flags]
    public enum LogType
    {
        None = 0,
        Info = 1,
        Warning = 2,
        Error = 4,
        All = Info | Warning | Error,
    }
}
