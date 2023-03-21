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
    public abstract class Observable<T> : IObservableObject<T> where T : class
    {
        protected bool IsDisposing;

        protected bool IsDisposed;

        protected bool IsUpdateSuspended;

        // TODO consider using weak ref delegate
        [field: NonSerialized]
        public event PropertyChangedEventHandler<T> PropertyChanged;

        public IObservableObject<T> Subscribe(API.IObserver<T> observer)
        {
            observer.ThrowIfNull(nameof(observer));
            PropertyChanged += observer.OnPropertyChanged;
            return this;
        }

        public IObservableObject<T> Unsubscribe(API.IObserver<T> observer)
        {
            observer.ThrowIfNull(nameof(observer));
            PropertyChanged -= observer.OnPropertyChanged;
            return this;
        }

        public virtual IObservableObject<T> BeginUpdate()
        {
            IsUpdateSuspended = true;
            // TODO suspend events
            return this;
        }

        public virtual IObservableObject<T> EndUpdate()
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
            var args = PropertyChangedEventArgs<T>.Create(this, name, prevValue, newValue);
            PropertyChanged.Invoke(this, args);
        }

        public virtual void Dispose()
        {
            if (IsDisposed || IsDisposing)
            {
                // TODO
                return;
            }
            try
            {
                IsDisposing = true;

                PropertyChanged = null;
                Dispose(true);
            }
            finally
            {
                IsDisposed = true;
            }
        }

        protected virtual void Dispose(bool disposing)
        {

        }
    }
}
