using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using DevKit.Core.Extensions;
using UnityEngine;

namespace DevKit.Logging
{
    internal class UnityConsoleLogger : ILogger
    {
        public ILoggerConfig Config { get; set; } = new ConsoleLoggerConfig();

        private const string LineSplitter = "/r/n";

        private bool IsEnabled
        {
            get
            {
                return Config.IsEnabled;
            }
        }

        private static void AddCallerName(ref string callerName, ref string message)
        {
            if (!callerName.IsNullOrEmpty())
            {
                message = $"[{callerName}]: {message}";
            }
        }

        public ILogger LogInfo(string message, [CallerMemberName] string callerName = "")
        {
            if (!IsEnabled) return this;
            AddCallerName(ref callerName, ref message);
            Debug.Log(message);
            return this;
        }

        public ILogger LogInfo(string format, string message, [CallerMemberName] string callerName = "")
        {
            if (!IsEnabled) return this;
            var msg = string.Format(format, message);
            return LogInfo(msg, callerName);
        }

        public ILogger LogInfo(string[] messages, [CallerMemberName] string callerName = "")
        {
            if (!IsEnabled) return this;
            var msg = string.Join(LineSplitter, messages);
            return LogInfo(msg, callerName);
        }

        public ILogger LogInfo(string format, string[] messages, [CallerMemberName] string callerName = "")
        {
            if (!IsEnabled) return this;
            var msg = string.Join(LineSplitter, messages);
            msg = string.Format(format, msg);
            return LogInfo(msg, callerName);
        }

        public ILogger LogInfo(string format, object[] messages, [CallerMemberName] string callerName = "")
        {
            if (!IsEnabled) return this;
            AddCallerName(ref callerName, ref format);
            Debug.LogFormat(format, messages);
            return this;
        }

        public ILogger LogWarning(string message, [CallerMemberName] string callerName = "")
        {
            if (!IsEnabled) return this;
            AddCallerName(ref callerName, ref message);
            Debug.LogWarning(message);
            return this;
        }

        public ILogger LogWarning(string[] messages, [CallerMemberName] string callerName = "")
        {
            if (!IsEnabled) return this;
            var msg = string.Join(LineSplitter, messages);
            return LogWarning(msg, callerName);
        }

        public ILogger LogWarning(string format, string message, [CallerMemberName] string callerName = "")
        {
            if (!IsEnabled) return this;
            var msg = string.Format(format, message);
            return LogWarning(msg, callerName);
        }

        public ILogger LogWarning(string format, string[] messages, [CallerMemberName] string callerName = "")
        {
            if (!IsEnabled) return this;
            var msg = string.Join(LineSplitter, messages);
            msg = string.Format(format, msg);
            return LogWarning(msg, callerName);
        }

        public ILogger LogWarning(string format, object[] messages, [CallerMemberName] string callerName = "")
        {
            if (!IsEnabled) return this;
            AddCallerName(ref callerName, ref format);
            Debug.LogWarningFormat(format, messages);
            return this;
        }

        public ILogger LogError(string error, [CallerMemberName] string callerName = "")
        {
            if (!IsEnabled) return this;
            AddCallerName(ref callerName, ref error);
            Debug.LogError(error);
            return this;
        }

        public ILogger LogError(Exception error, [CallerMemberName] string callerName = "")
        {
            if (!IsEnabled) return this;
            var msg = error.ToString();
            return LogInfo(msg, callerName);
        }

        public ILogger LogError(string[] errors, [CallerMemberName] string callerName = "")
        {
            if (!IsEnabled) return this;
            var error = string.Join(LineSplitter, errors);
            return LogError(error, callerName);
        }

        public ILogger LogError(string format, Exception error, [CallerMemberName] string callerName = "")
        {
            if (!IsEnabled) return this;
            var msg = string.Format(format, error);
            return LogError(msg, callerName);
        }

        public ILogger LogError(IEnumerable<Exception> errors, [CallerMemberName] string callerName = "")
        {
            if (!IsEnabled) return this;
            var str = new StringBuilder();
            foreach (var error in errors)
            {
                str.AppendLine(error?.ToString());
            }
            return LogError(str.ToString(), callerName);
        }

        public ILogger LogError(string format, IEnumerable<Exception> errors, [CallerMemberName] string callerName = "")
        {
            if (!IsEnabled) return this;
            var msg = string.Join(LineSplitter, errors);
            return LogError(string.Format(format, msg), callerName);
        }

        public ILogger LogError(string format, object[] errors, [CallerMemberName] string callerName = "")
        {
            if (!IsEnabled) return this;
            AddCallerName(ref callerName, ref format);
            Debug.LogErrorFormat(format, errors);
            return this;
        }
    }
}
