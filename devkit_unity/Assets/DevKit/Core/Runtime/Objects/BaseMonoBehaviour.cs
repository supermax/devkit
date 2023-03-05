using System.Runtime.CompilerServices;
using UnityEngine;

namespace DevKit.Core.Objects
{
    public abstract class BaseMonoBehaviour : MonoBehaviour
    {
        [SerializeField] private bool _isLoggingEnabled;

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is destroyed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is destroyed; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsDestroyed { get; protected set; }

        /// <summary>
        /// Gets or sets the log severity.
        /// </summary>
        /// <value>
        /// The log severity.
        /// </value>
        public virtual bool IsLoggingEnabled
        {
            get { return _isLoggingEnabled; }
            set { _isLoggingEnabled = value; }
        }

        /// <summary>
        /// Logs the method.
        /// </summary>
        /// <param name="callerName">The name of the calling method.</param>
        protected virtual void LogMethod([CallerMemberName] string callerName = "")
        {
            if (IsLoggingEnabled)
            {
                Debug.LogFormat("{0}->{1}", GetType().Name, callerName);
            }
        }

        private void Awake()
        {
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            LogMethod();
        }

        private void Start()
        {
            OnStart();
        }

        protected virtual void OnStart()
        {
            LogMethod();
        }

        /// <summary>
        ///     Called when this instance is destroyed
        /// </summary>
        protected virtual void OnDestroy()
        {
            IsDestroyed = true;
            LogMethod();
        }
    }
}
