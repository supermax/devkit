using System.Collections;
using System.Collections.Generic;

namespace DevKit.Core.Observables.API
{
    public class CollectionChangedEventArgs
    {
        public IEnumerable PrevKeys { get; }

        public IEnumerable NewKeys { get; }

        public virtual IObservableCollection Source { get; protected set; }

        public virtual CollectionChangedEventAction Action { get; protected set; }

        public virtual IEnumerable PrevItems { get; protected set; }

        public virtual IEnumerable NewItems { get; protected set; }

        public CollectionChangedEventArgs(
            IObservableCollection source
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
