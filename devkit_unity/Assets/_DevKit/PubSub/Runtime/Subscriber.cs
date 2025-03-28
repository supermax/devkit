﻿using System;
using DevKit.Logging;
using DevKit.Core.WeakRef;
using DevKit.Logging.Extensions;

namespace DevKit.PubSub
{
    /// <summary>
    /// Pub-Sub Messenger Subscriber
    /// </summary>
    /// <remarks>
    /// A holder of weak delegate reference.
    /// Used in <see cref="Messenger"/> class.
    /// </remarks>
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global -> required for IDispose pattern
    public class Subscriber : IDisposable
    {
        // reference to callback
        private WeakReferenceDelegate _callback;

        // reference to predicate
        private WeakReferenceDelegate _predicate;

        private object _stateObj;

        /// <summary>
        /// Indicates if callback owner is alive
        /// </summary>
        public bool IsAlive
        {
            get
            {
                if(_callback == null)
                {
                    return false;
                }
                var isAlive = _callback.IsAlive;
                return isAlive;
            }
        }

        /// <summary>
        /// The type of the payload
        /// </summary>
        public Type PayloadType
        {
            get;
        }

        /// <summary>
        /// The ID if this instance
        /// </summary>
        public int Id
        {
            get;
        }

        public bool IsPredicate
        {
            get;
        }

        public override int GetHashCode()
        {
            return Id;
        }

        /// <summary>
        /// Subscriber's Constructor
        /// </summary>
        /// <param name="payloadType">The type of the payload</param>
        /// <param name="predicate">The predicate delegate</param>
        /// <param name="logger">The logger to log info or errors</param>
        /// <param name="stateObj">The state object</param>
        /// <exception cref="ArgumentNullException">
        /// The exception is thrown in case 'payloadType' or 'predicate' null
        /// </exception>
        public Subscriber(Type payloadType, Delegate predicate, object stateObj)
            : this(payloadType, predicate, null, true, stateObj)
        { }

        /// <summary>
        /// Subscriber's Constructor
        /// </summary>
        /// <param name="payloadType">The type of the payload</param>
        /// <param name="callback">The callback delegate</param>
        /// <param name="predicate">The predicate delegate</param>
        /// <param name="stateObj">The state object</param>
        /// <exception cref="ArgumentNullException">
        /// The exception is thrown in case 'payloadType' or 'callback' null
        /// </exception>
        public Subscriber(Type payloadType, Delegate callback, Delegate predicate, object stateObj)
            : this(payloadType, callback, predicate, false, stateObj)
        { }

        /// <summary>
        /// Subscriber's Constructor
        /// </summary>
        /// <param name="payloadType">The type of the payload</param>
        /// <param name="callback">The callback delegate</param>
        /// <param name="predicate">The predicate delegate</param>
        /// <param name="isPredicate">Indicator is passed callback is predicate</param>
        /// <param name="stateObj">The state object</param>
        /// <exception cref="ArgumentNullException">
        /// The exception is thrown in case 'payloadType' or 'callback' null
        /// </exception>
        private Subscriber(Type payloadType, Delegate callback, Delegate predicate, bool isPredicate, object stateObj)
        {
            if(callback == null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            // assign values to vars
            PayloadType = payloadType ?? throw new ArgumentNullException(nameof(payloadType));
            Id = callback.GetHashCode();
            _callback = new WeakReferenceDelegate(callback);
            IsPredicate = isPredicate;
            _stateObj = stateObj;

            // --- init predicate ---
            if (predicate == null)
            {
                return;
            }
            _predicate = new WeakReferenceDelegate(predicate);
        }

        /// <summary>
        /// Invokes callback method with given payload instance
        /// </summary>
        /// <param name="payload"></param>
        /// <typeparam name="T"></typeparam>
        public object Invoke<T>(T payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }
            if (_isDisposed)
            {
                this.LogError($"This instance of {nameof(Subscriber)} is disposed [{this}].");
                return null;
            }
            // validate callback method info
            if(_callback == null)
            {
                this.LogError($"{nameof(_callback)} is null [{this}].");
                return null;
            }
            if(!_callback.IsAlive)
            {
                this.LogWarning($"{nameof(_callback)} is not alive [{this}].");
                return null;
            }

            // check predicate state
            if (!InvokePredicate(payload))
            {
                return null;
            }

            // invoke callback method
            var args = _stateObj != null ?
                new [] {payload, _stateObj}: new object[] {payload};
            var res = _callback.Invoke(args);
            return res;
        }

        private bool InvokePredicate<T>(T payload)
        {
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }
            if (_predicate == null)
            {
                return true;
            }

            // get reference to the predicate function owner
            // check if predicate returned 'true'
            var args = _stateObj != null ?
                new [] {payload, _stateObj}: new object[] {payload};
            var isAccepted = (bool)_predicate.Invoke(args);
            return isAccepted;
        }

       #region IDisposable Support
       private bool _isDisposed;

        /// <summary>
        /// Dispose this instance
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed || !disposing)
            {
                return;
            }

            _stateObj = null;
            
            if(_callback != null)
            {
                _callback.Dispose();
                _callback = null;
            }

            if (_predicate != null)
            {
                _predicate.Dispose();
                _predicate = null;
            }
            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Subscriber()
        {
            Dispose(false);
        }
        #endregion

        public override bool Equals(object obj)
        {
            var other = obj as Subscriber;
            var isSame = other != null && Id == other.Id && PayloadType == other.PayloadType;
            return isSame;
        }

        public override string ToString()
        {
            var id = (_callback?.ToString() ?? PayloadType?.Name) ?? Id.ToString();
            return id;
        }

        public string GetCallbackString()
        {
            var id = $"{_callback.Target}.{_callback.Method.Name}()";
            return id;
        }
    }
}
