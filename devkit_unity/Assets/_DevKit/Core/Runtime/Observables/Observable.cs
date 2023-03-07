using System;
using System.Runtime.Serialization;
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

        public virtual event PropertyChangedEventHandler<T> PropertyChanged;

        public IObservableObject<T> Subscribe(DevKit.Core.Observables.API.IObserver<T> observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer));
            }
            PropertyChanged += observer.OnPropertyChanged;
            return this;
        }

        public IObservableObject<T> Unsubscribe(DevKit.Core.Observables.API.IObserver<T> observer)
        {
            if (observer == null)
            {
                throw new ArgumentNullException(nameof(observer));
            }
            PropertyChanged -= observer.OnPropertyChanged;
            return this;
        }

        public IObservableObject<T> BeginUpdate()
        {
            throw new NotImplementedException();
        }

        public IObservableObject<T> EndUpdate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Invokes 'PropertyValueChanged' event in a safe way
        /// </summary>
        /// <param name="name"></param>
        /// <param name="prevValue"></param>
        /// <param name="newValue"></param>
        protected virtual void InvokePropertyChanged(string name, object prevValue, object newValue)
        {
            if (PropertyChanged == null)
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
