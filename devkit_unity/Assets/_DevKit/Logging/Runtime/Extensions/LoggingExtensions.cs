using System.Runtime.CompilerServices;
using DevKit.Core.Extensions;

namespace DevKit.Logging.Extensions
{
    // TODO cover all required extensions for useful logging
    public static class LoggingExtensions
    {
        private static void InitCallerName(System.Type type, ref string callerName)
        {
            if (type == null)
            {
                return;
            }

            callerName = !callerName.IsNullOrEmpty()
                ? $"{type.Name}->{callerName}"
                : $"{type.Name}";
        }

        private static void InitCallerName(object obj, ref string callerName)
        {
            if (obj == null)
            {
                return;
            }
            if (obj is System.Type type)
            {
                InitCallerName(type, ref callerName);
                return;
            }
            InitCallerName(obj.GetType(), ref callerName);
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
    }
}
