using System;
using DevKit.Core.Extensions;
using DevKit.PubSub.API;

namespace DevKit.PubSub.Extensions
{
    /// <summary>
    /// Messenger's Extensions
    /// </summary>
    public static class MessengerExtensions
    {
        /// <summary>
        /// Subscribe given callback to receive payload
        /// </summary>
        /// <param name="subscriber">The subscriber</param>
        /// <param name="callback">The callback that will receive the payload</param>
        /// <param name="predicate">Callback's predicate</param>
        /// <typeparam name="TS">The type of subscriber</typeparam>
        /// <typeparam name="TP">The type of payload to receive</typeparam>
        /// <exception cref="ArgumentNullException">Thrown in case <param name="subscriber"/> or <param name="callback"/> is null</exception>
        /// <returns>Instance of the subscriber</returns>
        public static IMessengerSubscriber Subscribe<TP>(
            this IMessengerSubscriber subscriber
            , Action<TP> callback
            , Predicate<TP> predicate = null)
            where TP : class
        {
            subscriber.ThrowIfDefault(nameof(subscriber));

            Messenger.Default.Subscribe(callback, predicate);
            return subscriber;
        }

        public static IMessengerSubscriber Unsubscribe<TP>(
            this IMessengerSubscriber subscriber
            , Action<TP> callback
            , Predicate<TP> predicate = null)
            where TP : class
        {
            subscriber.ThrowIfDefault(nameof(subscriber));

            Messenger.Default.Unsubscribe(callback);
            return subscriber;
        }

        /// <summary>
        /// Subscribe given callback to receive payload with state object
        /// </summary>
        /// <param name="subscriber">The subscriber</param>
        /// <param name="callback">The callback that will receive the payload</param>
        /// <param name="predicate">Callback's predicate</param>
        /// <param name="stateObj">The state object</param>
        /// <typeparam name="TS">The type of subscriber</typeparam>
        /// <typeparam name="TC">The type of payload to receive for the given callback</typeparam>
        /// <typeparam name="TO">The type of state object to receive for the given callback</typeparam>
        /// <exception cref="ArgumentNullException">Thrown in case <param name="subscriber"/> or <param name="callback"/> is null</exception>
        /// <returns>Instance of the subscriber</returns>
        public static IMessengerSubscriber Subscribe<TC, TO>(
            this IMessengerSubscriber subscriber
            , Action<TC, TO> callback
            , Func<TC, TO, bool> predicate = null
            , TO stateObj = default)
            where TC : class
        {
            subscriber.ThrowIfDefault(nameof(subscriber));

            Messenger.Default.Subscribe(callback, predicate, stateObj);
            return subscriber;
        }

        /// <summary>
        /// Subscribe predicate to filter irrelevant payloads per given type <typeparam name="TP"/>
        /// </summary>
        /// <param name="subscriber">The subscriber</param>
        /// <param name="predicate">The predicate to filter irrelevant payloads</param>
        /// <typeparam name="TS">The type of subscriber</typeparam>
        /// <typeparam name="TP">The type of payload to receive</typeparam>
        /// <exception cref="ArgumentNullException">Thrown in case <param name="subscriber"/> or <param name="predicate"/> is null</exception>
        /// <returns>Instance of the subscriber</returns>
        public static IMessengerSubscriber Subscribe<TP>(
            this IMessengerSubscriber subscriber
            , Predicate<TP> predicate)
            where TP : class
        {
            subscriber.ThrowIfDefault(nameof(subscriber));

            Messenger.Default.Subscribe(predicate);
            return subscriber;
        }

        /// <summary>
        /// Subscribe predicate to filter irrelevant payloads per given type <typeparam name="TC"/>
        /// </summary>
        /// <param name="predicate">The predicate to filter irrelevant payloads</param>
        /// <param name="stateObj">The state object</param>
        /// <param name="subscriber">The subscriber</param>
        /// <typeparam name="TS">The type of subscriber</typeparam>
        /// <typeparam name="TC">The type of payload to receive</typeparam>
        /// <typeparam name="TO">The type of state object to receive for the given callback</typeparam>
        /// <exception cref="ArgumentNullException">Thrown in case <param name="subscriber"/> or <param name="predicate"/> is null</exception>
        /// <returns>Instance of the subscriber</returns>
        public static IMessengerSubscriber Subscribe<TC, TO>(
            this IMessengerSubscriber subscriber
            , Func<TC, TO, bool> predicate
            , TO stateObj = default)
            where TC : class
        {
            subscriber.ThrowIfDefault(nameof(subscriber));

            Messenger.Default.Subscribe(predicate, stateObj);
            return subscriber;
        }

        /// <summary>
        /// Publish given payload to relevant subscribers
        /// </summary>
        /// <param name="payload">Instance of payload to publish</param>
        /// <param name="subscriber">The subscriber</param>
        /// <typeparam name="TS">The type of subscriber</typeparam>
        /// <typeparam name="TP">The type of payload to publish</typeparam>
        /// <exception cref="ArgumentNullException">Thrown in case <param name="subscriber"/> or <param name="payload"/> is null</exception>
        /// <returns>Instance of the subscriber</returns>
        public static TS Publish<TS, TP>(
            this TS subscriber, TP payload)
            where TS : IMessengerSubscriber
            where TP : class
        {
            subscriber.ThrowIfDefault(nameof(subscriber));

            Messenger.Default.Publish(payload);
            return subscriber;
        }

        /// <summary>
        /// Publish payload
        /// </summary>
        /// <param name="subscriber">The subscriber</param>
        /// <typeparam name="TS">The type of subscriber</typeparam>
        /// <param name="payloadType">The type of the payload</param>
        /// <param name="payload">The payload</param>
        /// <exception cref="ArgumentNullException">Exception is thrown in case <param name="payloadType"></param> is null</exception>
        /// <exception cref="ArgumentNullException">Exception is thrown in case <param name="payload"></param> is null</exception>
        /// <exception cref="InvalidCastException">Exception is thrown in case <param name="payload"></param> type doesn't match <param name="payloadType"></param></exception>
        /// <exception cref="ArgumentNullException">Thrown in case <param name="subscriber"/> or <param name="payloadType"/> or <param name="payload"/> is null</exception>
        /// <returns>Instance of the subscriber</returns>
        public static TS Publish<TS>(this TS subscriber, Type payloadType, object payload)
            where TS : IMessengerSubscriber
        {
            subscriber.ThrowIfDefault(nameof(subscriber));

            Messenger.Default.Publish(payloadType, payload);
            return subscriber;
        }
    }
}
