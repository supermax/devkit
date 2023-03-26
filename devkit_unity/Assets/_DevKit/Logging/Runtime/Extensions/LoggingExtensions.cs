using System.Runtime.CompilerServices;
using DevKit.Core.Extensions;

namespace DevKit.Logging.Extensions
{
    // TODO cover all required extensions for useful logging
    public static class LoggingExtensions
    {
        public static ILogger LogInfo(this object obj, string message, [CallerMemberName] string callerName = "")
        {
            if (callerName.IsNullOrEmpty() && obj != null)
            {
                callerName = obj.GetType().Name;
            }
            return Logger.Default.LogInfo(message, callerName);
        }

        public static ILogger LogWarning(this object obj, string message, [CallerMemberName] string callerName = "")
        {
            if (callerName.IsNullOrEmpty() && obj != null)
            {
                callerName = obj.GetType().Name;
            }
            return Logger.Default.LogWarning(message, callerName);
        }

        public static ILogger LogError(this object obj, string message, [CallerMemberName] string callerName = "")
        {
            if (callerName.IsNullOrEmpty() && obj != null)
            {
                callerName = obj.GetType().Name;
            }
            return Logger.Default.LogError(message, callerName);
        }

        public static bool HasFlagFast(this LogTarget value, LogTarget flag)
        {
            return (value & flag) != 0;
        }
    }
}
