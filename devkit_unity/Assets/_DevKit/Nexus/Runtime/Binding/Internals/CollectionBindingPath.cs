using System;
using System.Collections;
using System.Collections.Generic;
using DevKit.Core.Observables.API;

namespace DevKit.Nexus.Binding.Internals
{
    public class CollectionBindingPath : BindingPath, ICollectionObserver
    {
        internal CollectionBindingPath(object source) : base(source) { }

        public override string ToString()
        {
            return $"{nameof(Source)}: {Source}";
        }

        internal override object GetValue()
        {
            return Source;
        }

        internal override void SetValue(object value)
        {
            Source = value;
        }

        public void OnCollectionChanged(object sender, CollectionChangedEventArgs args)
        {
            if (args.IsHandled || args.Error != null)
            {
                return;
            }

            switch (args.Action)
            {
                case CollectionChangedEventAction.Add:
                    Add(Source as IList, args.NewItems);
                    Add(Source as IDictionary, args.NewItems, args.NewKeys);
                    break;
                
                case CollectionChangedEventAction.Remove:
                    break;

                case CollectionChangedEventAction.Replace:
                    break;

                case CollectionChangedEventAction.Move:
                    break;

                case CollectionChangedEventAction.Reset:
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(args), $"Unsupported {nameof(args.Action)} type: {args.Action}");
            }
        }

        public void OnError(object sender, CollectionChangedEventArgs args)
        {
            Error = args.Error;
        }

        private static void Add(IList source, IEnumerable items)
        {
            if (source == null)
            {
                return;
            }

            foreach (var item in items)
            {
                source.Add(item);
            }
        }

        private static void Add(IDictionary source, IEnumerable items, IEnumerable keys)
        {
            if (source == null)
            {
                return;
            }

            var eItems = items.GetEnumerator();
            eItems.Reset();

            var eKeys = keys.GetEnumerator();
            eKeys.Reset();

            while (eItems.MoveNext() && eKeys.MoveNext())
            {
                if (eKeys.Current == null)
                {
                    continue;
                }
                source.Add(eKeys.Current, eItems.Current);
            }
        }

        private static void Remove(IList source, IEnumerable items)
        {
            if (source == null)
            {
                return;
            }
        }

        private static void Remove(IDictionary source, IEnumerable items)
        {
            if (source == null)
            {
                return;
            }
        }

        private static void Replace(IList source, IEnumerable items)
        {
            if (source == null)
            {
                return;
            }
        }

        private static void Replace(IDictionary source, IEnumerable items)
        {
            if (source == null)
            {
                return;
            }
        }

        private static void Move(IList source, IEnumerable items)
        {
            if (source == null)
            {
                return;
            }
        }

        private static void Move(IDictionary source, IEnumerable items)
        {
            if (source == null)
            {
                return;
            }
        }

        private static void Reset(IList source)
        {
            if (source == null)
            {
                return;
            }
        }

        private static void Reset(IDictionary source)
        {
            if (source == null)
            {
                return;
            }
        }
    }
}
