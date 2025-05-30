using System;
using System.Collections;
using NUnit.Framework;
using DevKit.PubSub;
using DevKit.Tests.Messaging.Fixtures;
using UnityEngine;
using UnityEngine.TestTools;
using Debug = System.Diagnostics.Debug;

namespace DevKit.Tests.Messaging
{
    [TestFixture]
    public class SubscriberTest : MessengerTest
    {
        private readonly Type _filteredPayloadType = typeof (FilteredPayload);

        private Reference<MessengerWeakReferenceCallback> _ref;

        private const int StateNumber = 2018;

        [SetUp]
        public void Setup()
        {
            _ref = new Reference<MessengerWeakReferenceCallback> { Ref = new MessengerWeakReferenceCallback() };
            Assert.That(_ref, Is.Not.Null);
            Assert.That(_ref.Ref, Is.Not.Null);
        }

        private static Subscriber GetSubscriber(Type payloadType, Delegate callback, Delegate predicate, object stateObj)
        {
            var subscriber = new Subscriber(payloadType, callback, predicate, stateObj);
            Assert.That(subscriber, Is.Not.Null);
            return subscriber;
        }

        private static Subscriber GetSubscriber(Type payloadType, Delegate predicate,  object stateObj)
        {
            var subscriber = new Subscriber(payloadType, predicate, stateObj);
            Assert.That(subscriber, Is.Not.Null);
            return subscriber;
        }

        private Subscriber GetSubscriber()
        {
            var payload = new MessengerTestPayload<int> {Data = StateNumber};
            var subscriber = GetSubscriber(_filteredPayloadType, (Action<FilteredPayload, object>)OnSubscriberCallback, (Func<FilteredPayload, object, bool>)SubscriberPredicate, payload);
            return subscriber;
        }

        private static void OnSubscriberCallback(FilteredPayload payload, object stateObj)
        {
            Assert.That(payload, Is.Not.Null);
            Assert.That(stateObj, Is.Not.Null);
            Assert.That(stateObj.GetType(), Is.EqualTo(typeof(MessengerTestPayload<int>)));

            var state = (MessengerTestPayload<int>)stateObj;
            Assert.That(state.Data, Is.EqualTo(StateNumber));

            payload.CallbackCount++;
        }

        private static bool SubscriberPredicate(FilteredPayload payload, object stateObj)
        {
            Assert.That(payload, Is.Not.Null);
            Assert.That(stateObj, Is.Not.Null);
            Assert.That(stateObj.GetType(), Is.EqualTo(typeof(MessengerTestPayload<int>)));

            var state = (MessengerTestPayload<int>)stateObj;
            Assert.That(state.Data, Is.EqualTo(StateNumber));

            return payload.IsFilterOn;
        }

        [Test]
        public void TestCtorArgsValidation()
        {
            var callback = (Action<FilteredPayload, object>)OnSubscriberCallback;
            var predicate = (Func<FilteredPayload, object, bool>)SubscriberPredicate;

            // ctor with callback only
            Assert.Throws<ArgumentNullException>(() => GetSubscriber(null, null!, null, null));
            Assert.Throws<ArgumentNullException>(() => GetSubscriber(_filteredPayloadType, null!, null, null));
            Assert.Throws<ArgumentNullException>(() => GetSubscriber(_filteredPayloadType, callback, null, null));
            Assert.That(() => GetSubscriber(_filteredPayloadType, callback, null, null), Is.Not.Null);

            // ctor with callback and predicate
            Assert.Throws<ArgumentNullException>(() => GetSubscriber(_filteredPayloadType, null!, predicate, null));
            Assert.Throws<ArgumentNullException>(() => GetSubscriber(_filteredPayloadType, callback, predicate, null));
            Assert.That(() => GetSubscriber(typeof(FilteredPayload), callback, predicate, null), Is.Not.Null);

            // ctor with predicate only
            Assert.Throws<ArgumentNullException>(() => GetSubscriber(_filteredPayloadType, predicate, null, null));
            Assert.That(() => GetSubscriber(_filteredPayloadType, predicate, null), Is.Not.Null);
        }

        [Test]
        public void TestInvokeArgsValidation()
        {
            using var subscriber = GetSubscriber();
            Assert.That(subscriber, Is.Not.Null);
            Assert.That(subscriber.IsAlive, Is.True);
            Assert.That(subscriber.IsPredicate, Is.False);
            Assert.That(subscriber.Id, Is.Not.Zero);
            Assert.That(subscriber.GetHashCode(), Is.Not.Zero);

            Assert.Throws<ArgumentNullException>(() => subscriber.Invoke<FilteredPayload>(null!));
            Assert.DoesNotThrow(() => subscriber.Invoke(new FilteredPayload()));
        }

        [Test]
        public void TestDispose()
        {
            Subscriber subscriber;
            using (subscriber = GetSubscriber())
            {
                Assert.That(subscriber, Is.Not.Null);
                Assert.That(subscriber.IsAlive, Is.True);
                Assert.DoesNotThrow(() => subscriber.Invoke(new FilteredPayload()));
            }
            Assert.That(subscriber.IsAlive, Is.False);
            Assert.DoesNotThrow(() => subscriber.Invoke(new FilteredPayload()));
        }

        [UnityTest]
        public IEnumerator TestWeakRef()
        {
            Assert.That(_ref, Is.Not.Null);
            Assert.That(_ref?.Ref, Is.Not.Null);

            Debug.Assert(_ref != null, $"{nameof(_ref)} != null");
            Debug.Assert(_ref.Ref != null, $"{nameof(_ref.Ref)} != null");
            Debug.WriteLine("Tested {0}->{1}", _ref, _ref.Ref);

            var subscriber = GetSubscriber(typeof(MessengerTestPayload<int>), (Action<MessengerTestPayload<int>>)_ref.Ref.Callback, null);
            Assert.That(subscriber, Is.Not.Null);

            var payload = new MessengerTestPayload<int>();
            Assert.DoesNotThrow(() => subscriber.Invoke(payload));

            _ref.Dispose();
            _ref.Ref = null;

            yield return new WaitForSeconds(3);

            Assert.That(subscriber.IsAlive, Is.False);
            Assert.DoesNotThrow(() => subscriber.Invoke(payload));
        }
    }
}
