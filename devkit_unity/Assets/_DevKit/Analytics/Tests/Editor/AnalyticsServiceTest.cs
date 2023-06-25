using System;
using System.Collections.Generic;
using DevKit.Analytics.Events;
using DevKit.Analytics.Events.API;
using DevKit.Core.Threading;
using DevKit.Serialization.Json.Extensions;
using NUnit.Framework;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DevKit.Analytics.Tests.Editor
{
    [TestFixture]
    public class AnalyticsServiceTest
    {
        [Test]
        public void AnalyticsService_SendEvent_Test()
        {
            var service = new AnalyticsServiceImplementation(UnityMainThreadDispatcher.Default);
            var analyticsEvent = new AnalyticsEvent(AnalyticsEventType.Gameplay
                , "enemyKill"
                , new Dictionary<string, object>
                {
                    {"enemiesCount", 1}
                });
            Assert.DoesNotThrow(() => service.SendEvent(analyticsEvent));

            var json = analyticsEvent.ToJson();
            Debug.Log(json);
            Assert.That(json, Is.Not.Null);

            var deserializedEvent = json.ToObject<AnalyticsEvent>();
            Debug.Log(deserializedEvent);
            Assert.That(deserializedEvent, Is.Not.Null);
        }

        [Test]
        public void AnalyticsService_SendEvents_Test()
        {
            var service = new AnalyticsServiceImplementation(UnityMainThreadDispatcher.Default);

            var analyticsEvents = new IAnalyticsEvent[100];
            for (var i = 0; i < analyticsEvents.Length; i++)
            {
                var eventType = (AnalyticsEventType)Random.Range(0, 5);
                var propCount = i + 1;
                var analyticsEvent = new AnalyticsEvent(eventType
                    , $"eventName{propCount}"
                    , new Dictionary<string, object>
                    {
                        {$"prop{propCount++}", propCount},
                        {$"prop{propCount++}", $"text{propCount}"},
                        {$"prop{propCount++}", new Dictionary<string, object>
                            {
                                {$"prop{propCount++}", true},
                                {$"prop{propCount}", DateTime.Now},
                            }},
                    });
                analyticsEvents[i] = analyticsEvent;
            }
            Assert.DoesNotThrow(() => service.SendEvents(analyticsEvents));

            var json = analyticsEvents.ToJson();
            Debug.Log(json);
            Assert.That(json, Is.Not.Null);

            var deserializedEvents = json.ToObject<AnalyticsEvent[]>();
            Debug.Log(deserializedEvents);
            Assert.That(deserializedEvents, Is.Not.Null);
            Assert.That(deserializedEvents.Length, Is.EqualTo(analyticsEvents.Length));
        }
    }
}
