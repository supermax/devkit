using System;

namespace DevKit.Logging
{
    public class ConsoleLoggerConfig : ILoggerConfig
    {
        public string Name { get; set; } = "Console Logger";

        public LogTarget Target { get; set; } = LogTarget.Console;

        public LogType Type { get; set; } = LogType.All;

        public bool IsEnabled { get; set; } = true;

        public TimeSpan MessageTimeSpan { get; set; }
    }
}
