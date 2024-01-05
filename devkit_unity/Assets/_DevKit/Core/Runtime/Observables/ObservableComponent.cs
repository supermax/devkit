using DevKit.Core.Objects;
using DevKit.Core.Observables.API;

namespace DevKit.Core.Observables
{
    public abstract class ObservableComponent
        : BaseMonoBehaviour
            , IObservableObject
    {
        #region IDisposable

        public void Dispose()
        {
            // TODO implement
        }

        #endregion

        #region IObservableObject

        protected bool IsUpdateSuspended { get; set; }

        private PropertyChangedEventHandler _propertyChanged;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                _propertyChanged += value;
            }
            remove
            {
                _propertyChanged -= value;
            }
        }

        public IObservableObject Subscribe(IObserver observer)
        {
            PropertyChanged += observer.OnPropertyChanged;
            return this;
        }

        public IObservableObject Unsubscribe(IObserver observer)
        {
            PropertyChanged -= observer.OnPropertyChanged;
            return this;
        }

        public IObservableObject BeginUpdate()
        {
            IsUpdateSuspended = true;
            return this;
        }

        public IObservableObject EndUpdate()
        {
            IsUpdateSuspended = false;
             return this;
        }

        /// <summary>
        /// Invokes 'PropertyValueChanged' event in a safe way
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="prevValue"></param>
        /// <param name="newValue"></param>
        protected virtual void InvokePropertyChanged(string propertyName, object prevValue, object newValue)
        {
            if (_propertyChanged == null || IsUpdateSuspended)
            {
                return;
            }

            var args = new PropertyChangedEventArgs(this, propertyName, prevValue, newValue);
            _propertyChanged.Invoke(this, args);
        }

        #endregion
    }
}
