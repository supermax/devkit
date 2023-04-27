using System.Collections;
using System.Collections.Generic;

namespace DevKit.Core.Observables.API
{
    public interface IObservableCollection<T>
        : IObservableCollection
            , IList<T>
            , IList
    {
        event CollectionChangedEventHandler<T> CollectionChanged;
    }

    public interface IObservableCollection<TKey, TValue>
        : IObservableCollection
            , IDictionary<TKey, TValue>
            , IDictionary
    {
        event CollectionChangedEventHandler<TKey, TValue> CollectionChanged;
    }

    public interface IObservableCollection : IObservableObject
    {

    }
}
