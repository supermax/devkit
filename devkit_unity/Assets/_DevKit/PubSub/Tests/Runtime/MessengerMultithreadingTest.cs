using System;
using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using DevKit.Tests.Messaging.Fixtures;
using UnityEngine;
using UnityEngine.TestTools;

namespace DevKit.Tests.Messaging
{
    [TestFixture]
    public class MessengerMultithreadingTest : BaseMessengerTest
    {
        private MessengerTestPayload<int> _payload;

        [UnityTest]
        public IEnumerator TestPublishFromNewThread()
        {
            Assert.That(Messenger, Is.Not.Null);

            var instance = Messenger.Subscribe<MessengerTestPayload<int>>(OnPublishFromNewThreadCallback);
            Assert.That(instance, Is.Not.Null);
            Assert.That(Messenger, Is.SameAs(instance));
            Debug.LogFormat($"{nameof(Environment.CurrentManagedThreadId)}: {Environment.CurrentManagedThreadId}");

            _payload = new MessengerTestPayload<int> {Data = Environment.CurrentManagedThreadId};

            void Action() => PublishFromNewThreadMethod(Environment.CurrentManagedThreadId);
            Task.Run(Action);

            while (_payload.CallbackCount < 1)
            {
                yield return null;
            }
        }

        private void PublishFromNewThreadMethod(object threadIdObj)
        {
            Assert.That(Messenger, Is.Not.Null);

            var threadId = (int)threadIdObj;

            Debug.LogFormat($"{nameof(Environment.CurrentManagedThreadId)}: {Environment.CurrentManagedThreadId}" +
                            $", {nameof(threadId)}: {threadId}");
            //Assert.That(Environment.CurrentManagedThreadId, Is.Not.EqualTo(threadId));

            Messenger.Publish(_payload);
        }

        private void OnPublishFromNewThreadCallback(MessengerTestPayload<int> payload)
        {
            Debug.LogFormat($"[{nameof(OnPublishFromNewThreadCallback)}] Int Payload: {0} (Thread ID: {1})",
                payload.Data, Environment.CurrentManagedThreadId);

            Assert.That(Environment.CurrentManagedThreadId, Is.EqualTo(payload.Data));

            payload.CallbackCount = 1;
        }

        [UnityTest]
        public IEnumerator TestPublishAsync()
        {
            Assert.That(Messenger, Is.Not.Null);

            for (var i = 1; i <= 4; i++)
            {
                var count = i;
                Task.Run(() =>
                {
                    var instance = Messenger.Publish(new MessengerTestPayload<string>{Data = $"Hello World! [{count}]"});
                    Assert.That(instance, Is.Not.Null);
                });
                yield return null;
            }
        }
    }
}
