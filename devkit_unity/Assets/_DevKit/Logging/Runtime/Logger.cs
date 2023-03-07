using DevKit.Core.Objects;

namespace DevKit.Logging
{
    /// <summary>
    /// Default Logger
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class Logger : Singleton<ILogger, UnityConsoleLogger> { }
}
