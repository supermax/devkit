using System;
using System.Runtime.Serialization;
using DevKit.Core.Extensions;
using DevKit.Core.Observables.API;

namespace DevKit.Core.Observables
{
     /// <summary>
    /// Base observable class
    /// </summary>
    /// <remarks>
    /// Used for objects that have observers like UI elements or any Game Objects or other simple objects
    /// </remarks>
    [Serializable]
    [DataContract]
    public abstract class Observable : IObservableObject
    {
        protected bool IsUpdateSuspended;

        protected bool IsDisposing;

        protected bool IsDisposed;

        // TODO consider using weak ref delegate
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public IObservableObject Subscribe(IObserver observer)
        {
            observer.ThrowIfNull(nameof(observer));
            PropertyChanged += observer.OnPropertyChanged;
            return this;
        }

        public IObservableObject Unsubscribe(IObserver observer)
        {
            observer.ThrowIfNull(nameof(observer));
            PropertyChanged -= observer.OnPropertyChanged;
            return this;
        }

        public virtual IObservableObject BeginUpdate()
        {
            IsUpdateSuspended = true;
            // TODO suspend events
            return this;
        }

        public virtual IObservableObject EndUpdate()
        {
            IsUpdateSuspended = true;
            // TODO resume events + check if [] is triggering all props changed event
            InvokePropertyChanged("[]", null, null);
            return this;
        }

        /// <summary>
        /// Invokes 'PropertyValueChanged' event in a safe way
        /// </summary>
        /// <param name="name"></param>
        /// <param name="prevValue"></param>
        /// <param name="newValue"></param>
        protected virtual void InvokePropertyChanged(string name, object prevValue, object newValue)
        {
            if (PropertyChanged == null || IsUpdateSuspended)
            {
                return;
            }
            var args = new PropertyChangedEventArgs(this, name, prevValue, newValue);
            PropertyChanged.Invoke(this, args);
        }

        public virtual void Dispose()
        {
            if (IsDisposed || IsDisposing)
            {
                return;
            }
            try
            {
                IsDisposing = true;
                Dispose(true);
            }
            finally
            {
                IsDisposed = true;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                PropertyChanged = null;
            }
        }
    }
}
