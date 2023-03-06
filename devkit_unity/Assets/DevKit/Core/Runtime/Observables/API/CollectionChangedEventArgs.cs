namespace DevKit.Core.Observables.API
{
    public class CollectionChangedEventArgs<TKey, TValue> : BaseEventArgs
    {
        public IObservableCollection<TKey, TValue> Source { get; }

        public TKey Key { get; }

        public TValue PrevValue { get; }

        public TValue NewValue { get; }

        public CollectionChangedEventArgs(IObservableCollection<TKey, TValue> source, TKey key, TValue prevValue, TValue newValue)
        {
            Source = source;
            Key = key;
            PrevValue = prevValue;
            NewValue = newValue;
        }

        public static CollectionChangedEventArgs<TKey, TValue> Create(IObservableCollection<TKey, TValue> source, TKey key, TValue prevValue, TValue newValue)
        {
            var args = new CollectionChangedEventArgs<TKey, TValue>(source, key, prevValue, newValue);
            return args;
        }
    }
}
