using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DevKit.Core.Observables.API;

namespace DevKit.Core.Observables
{
    [Serializable]
    [DataContract]
    public class ObservableCollection<TKey, TValue> : Dictionary<TKey, TValue>, IObservableCollection<TKey, TValue>
    {
        public virtual event CollectionChangedEventHandler<TKey, TValue> CollectionChanged;

        protected virtual void InvokeCollectionChanged(TKey key, TValue prevValue, TValue newValue)
        {
            if (CollectionChanged == null)
            {
                return;
            }
            var args = CollectionChangedEventArgs<TKey, TValue>.Create(this, key, prevValue, newValue);
            CollectionChanged.Invoke(this, args);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            
            CollectionChanged = null;
            Clear();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
