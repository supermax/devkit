using System.Collections.Generic;
using DevKit.Analytics.Events.API;
using DevKit.Analytics.Services;
using DevKit.Core.Threading;

namespace DevKit.Analytics.Tests.Editor
{
    public class AnalyticsServiceImplementation : AnalyticsService
    {
        protected override void SendEventsToService(IEnumerable<IAnalyticsEvent> analyticsEvents)
        {
            // TODO
        }

        public AnalyticsServiceImplementation(IThreadDispatcher dispatcher) : base(dispatcher)
        {
        }
    }
}
