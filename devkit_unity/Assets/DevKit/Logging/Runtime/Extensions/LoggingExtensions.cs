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
    }
}
