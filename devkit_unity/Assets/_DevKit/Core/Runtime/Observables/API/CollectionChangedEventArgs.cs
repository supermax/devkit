namespace DevKit.Core.Observables.API
{
    public class CollectionChangedEventArgs<T> : BaseCollectionChangedEventArgs<IObservableCollection<T>, T>
    {
        public int NewIndex { get; }

        public int OldIndex { get; }

        public CollectionChangedEventArgs(IObservableCollection<T> source, CollectionChangedEventAction actionType, int oldIndex, int newIndex, T oldValue, T newValue) : base(source, actionType, oldValue, newValue)
        {
            OldIndex = oldIndex;
            NewIndex = newIndex;
        }

        public static CollectionChangedEventArgs<T> Create(IObservableCollection<T> source, CollectionChangedEventAction actionType, int oldIndex, int newIndex, T oldValue, T newValue)
        {
            var args = new CollectionChangedEventArgs<T>(source, actionType, oldIndex, newIndex, oldValue, newValue);
            return args;
        }
    }

    public class CollectionChangedEventArgs<TKey, TValue> : BaseCollectionChangedEventArgs<IObservableCollection<TKey, TValue>, TValue>
    {
        public TKey OldKey { get; }

        public TKey NewKey { get; }

        public CollectionChangedEventArgs(IObservableCollection<TKey, TValue> source, CollectionChangedEventAction actionType, TKey oldKey, TKey newKey, TValue oldValue, TValue newValue) : base(source, actionType, oldValue, newValue)
        {
            OldKey = oldKey;
            NewKey = newKey;
        }

        public static CollectionChangedEventArgs<TKey, TValue> Create(IObservableCollection<TKey, TValue> source, CollectionChangedEventAction actionType, TKey oldKey, TKey newKey, TValue prevValue, TValue newValue)
        {
            var args = new CollectionChangedEventArgs<TKey, TValue>(source, actionType, oldKey, newKey, prevValue, newValue);
            return args;
        }
    }

}
