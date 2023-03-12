namespace DevKit.Core.Observables.API
{
    public delegate void CollectionChangedEventHandler<T>(
        IObservableCollection<T> sender
        , CollectionChangedEventArgs<T> args);

    public delegate void CollectionChangedEventHandler<TKey, TValue>(
        IObservableCollection<TKey, TValue> sender
        , CollectionChangedEventArgs<TKey, TValue> args);
}
