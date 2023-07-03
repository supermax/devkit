using System.Runtime.CompilerServices;
using DevKit.Core.Extensions;

namespace DevKit.Logging.Extensions
{
    // TODO cover all required extensions for useful logging
    public static class LoggingExtensions
    {
        private static void InitCallerName(object obj, ref string callerName)
        {
            if (obj == null)
            {
                return;
            }

            callerName = !callerName.IsNullOrEmpty()
                ? $"{obj.GetType().Name}->{callerName}"
                : $"{obj.GetType().Name}";
        }

        public static ILogger LogInfo(this object obj, string message, [CallerMemberName] string callerName = "")
        {
            InitCallerName(obj, ref callerName);
            return Logger.Default.LogInfo(message, callerName);
        }

        public static ILogger LogWarning(this object obj, string message, [CallerMemberName] string callerName = "")
        {
            InitCallerName(obj, ref callerName);
            return Logger.Default.LogWarning(message, callerName);
        }

        public static ILogger LogError(this object obj, string message, [CallerMemberName] string callerName = "")
        {
            InitCallerName(obj, ref callerName);
            return Logger.Default.LogError(message, callerName);
        }

        public static bool HasFlag(this LogTarget value, LogTarget flag)
        {
            return (value & flag) != 0;
        }
    }
}
