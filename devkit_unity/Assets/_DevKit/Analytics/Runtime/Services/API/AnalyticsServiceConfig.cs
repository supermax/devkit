namespace DevKit.Analytics.Services.API
{
    // TODO init config values from server or from local file
    public class AnalyticsServiceConfig
    {
        public bool IsEnabled { get; set; }

        public bool IsEventsRecoveryEnabled { get; set; }

        public int EventsPerWriteRequest { get; set; } = 10;
    }
}
