using System.Collections;

namespace DevKit.Core.Observables.API
{
    public class CollectionChangedEventArgs : BaseEventArgs
    {
        public IEnumerable PrevKeys { get; }

        public IEnumerable NewKeys { get; }

        public object Source { get; }

        public CollectionChangedEventAction Action { get; }

        public IEnumerable PrevItems { get; }

        public IEnumerable NewItems { get; }

        public CollectionChangedEventArgs(
            object source
            , CollectionChangedEventAction actionType
            , IEnumerable prevKeys
            , IEnumerable newKeys
            , IEnumerable prevItems
            , IEnumerable newItems)
        {
            PrevKeys = prevKeys;
            NewKeys = newKeys;

            Source = source;
            Action = actionType;
            PrevItems = prevItems;
            NewItems = newItems;
        }
    }

}
