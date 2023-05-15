using System.Collections.Generic;

namespace DevKit.Core.Observables.API
{
    public interface IObservableCollection
    {
        event CollectionChangedEventHandler CollectionChanged;
    }

    public interface IObservableCollection<T>
            : IObservableCollection
            , IList<T>
    {

    }

    public interface IObservableCollection<TKey, TValue>
            : IObservableCollection
            , IDictionary<TKey, TValue>
    {

    }
}
