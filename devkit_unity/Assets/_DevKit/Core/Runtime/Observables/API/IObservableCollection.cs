using System.Collections.Generic;

namespace DevKit.Core.Observables.API
{
    public interface IObservableCollection<T>
        : IObservableCollection, ICollection<T>
            , IObservableObject<IObservableCollection<T>>
    {
        event CollectionChangedEventHandler<T> CollectionChanged;
    }

    public interface IObservableCollection<TKey, TValue>
        : IObservableCollection, IDictionary<TKey, TValue>
            , IObservableObject<IObservableCollection<TKey, TValue>>
    {
        event CollectionChangedEventHandler<TKey, TValue> CollectionChanged;
    }

    public interface IObservableCollection : IObservableObject
    {

    }
}
