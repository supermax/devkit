using DevKit.Core.Objects;

namespace DevKit.Logging
{
    /// <summary>
    /// Default Logger
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    // TODO Format all log lines in JSON format
    public sealed class Logger : Singleton<ILogger, UnityConsoleLogger> { }
}
