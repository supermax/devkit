using System;
using System.Collections.Generic;

namespace DevKit.Core.Observables.API
{
    public interface IObservableCollection<TKey, TValue> : IObservableCollection, IDictionary<TKey, TValue>
    {
        event CollectionChangedEventHandler<TKey, TValue> CollectionChanged;
    }

    public interface IObservableCollection : IDisposable
    {

    }
}
