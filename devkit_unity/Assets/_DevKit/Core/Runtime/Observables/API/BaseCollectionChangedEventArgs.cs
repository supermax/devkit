namespace DevKit.Core.Observables.API
{
    public abstract class BaseCollectionChangedEventArgs<TSource, TValue> : BaseEventArgs
    {
        public virtual TSource Source { get; protected set; }

        public virtual CollectionChangedEventAction Action { get; protected set; }

        public virtual TValue OldValue { get; protected set; }

        public virtual TValue NewValue { get; protected set; }

        protected BaseCollectionChangedEventArgs(TSource source, CollectionChangedEventAction actionType, TValue oldValue, TValue newValue)
        {
            Source = source;
            Action = actionType;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
