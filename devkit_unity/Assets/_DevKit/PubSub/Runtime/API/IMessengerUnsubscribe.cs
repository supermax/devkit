using System;

namespace DevKit.PubSub.API
{
    /// <summary>
    /// Interface for Unsubscription
    /// </summary>
    public interface IMessengerUnsubscribe
    {
        /// <summary>
        /// Unsubscribe given callback from receiving payload
        /// </summary>
        /// <param name="callback">The callback that subscribed to receive payload</param>
        /// <typeparam name="T">Type of payload to unsubscribe from</typeparam>
        /// <returns>Instance of the Messenger</returns>
        IMessengerUnsubscribe Unsubscribe<T>(Action<T> callback) where T : class;

        /// <summary>
        /// Unsubscribe given callback from receiving payload
        /// </summary>
        /// <param name="callback">The callback that subscribed to receive payload</param>
        /// <typeparam name="TC">Type of payload to unsubscribe from</typeparam>
        /// <typeparam name="TS">The type of state object for the given callback</typeparam>
        /// <returns>Instance of the Messenger</returns>
        IMessengerUnsubscribe Unsubscribe<TC, TS>(Action<TC, TS> callback) where TC : class;

        /// <summary>
        /// Unsubscribe given predicate from receiving payload
        /// </summary>
        /// <param name="predicate">The predicate that subscribed to receive payload</param>
        /// <typeparam name="T">Type of predicate to unsubscribe from</typeparam>
        /// <returns>Instance of the Messenger</returns>
        IMessengerUnsubscribe Unsubscribe<T>(Predicate<T> predicate) where T : class;

        /// <summary>
        /// Unsubscribe given predicate from receiving payload
        /// </summary>
        /// <param name="predicate">The predicate that subscribed to receive payload</param>
        /// <typeparam name="TC">Type of predicate to unsubscribe from</typeparam>
        /// <typeparam name="TS">The type of state object for the given callback</typeparam>
        /// <returns>Instance of the Messenger</returns>
        IMessengerUnsubscribe Unsubscribe<TC, TS>(Func<TC, TS, bool> predicate) where TC : class;

        /// <summary>
        /// Unsubscribe all callbacks from receiving payload of the given type
        /// </summary>
        /// <typeparam name="T">Type of payload to unsubscribe from</typeparam>
        /// <returns>Instance of the Messenger</returns>
        IMessengerUnsubscribe UnsubscribeAll<T>() where T : class;
    }
}
