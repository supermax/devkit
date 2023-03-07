using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DevKit.Logging
{
    public interface ILogger
    {
        ILoggerConfig Config { get; set; }

        ILogger LogInfo(string message, [CallerMemberName] string callerName = "");

        ILogger LogInfo(string[] messages, [CallerMemberName] string callerName = "");

        ILogger LogInfo(string format, string message, [CallerMemberName] string callerName = "");

        ILogger LogInfo(string format, string[] messages, [CallerMemberName] string callerName = "");

        ILogger LogInfo(string format, object[] messages, [CallerMemberName] string callerName = "");

        ILogger LogWarning(string message, [CallerMemberName] string callerName = "");

        ILogger LogWarning(string[] messages, [CallerMemberName] string callerName = "");

        ILogger LogWarning(string format, string message, [CallerMemberName] string callerName = "");

        ILogger LogWarning(string format, string[] messages, [CallerMemberName] string callerName = "");

        ILogger LogWarning(string format, object[] messages, [CallerMemberName] string callerName = "");

        ILogger LogError(string error, [CallerMemberName] string callerName = "");

        ILogger LogError(Exception error, [CallerMemberName] string callerName = "");

        ILogger LogError(string[] errors, [CallerMemberName] string callerName = "");

        ILogger LogError(string format, Exception error, [CallerMemberName] string callerName = "");

        ILogger LogError(IEnumerable<Exception> errors, [CallerMemberName] string callerName = "");

        ILogger LogError(string format, IEnumerable<Exception> errors, [CallerMemberName] string callerName = "");

        ILogger LogError(string format, object[] errors, [CallerMemberName] string callerName = "");
    }
}
