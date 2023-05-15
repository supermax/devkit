using System.Collections.Generic;

namespace DevKit.Core.Observables.API
{
    public interface IObservableCollection : IObservableObject
    {
        event CollectionChangedEventHandler CollectionChanged;

        IObservableCollection Subscribe(ICollectionObserver observer);

        IObservableCollection Unsubscribe(ICollectionObserver observer);
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
