namespace DevKit.Logging.Extensions
{
    public static class LogTargetExtensions
    {
        public static bool HasFlagFast(this LogTarget value, LogTarget flag)
        {
            return (value & flag) != 0;
        }
    }
}
