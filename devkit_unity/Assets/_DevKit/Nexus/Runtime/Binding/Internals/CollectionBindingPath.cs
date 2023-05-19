using System;
using System.Collections;
using System.Collections.Generic;
using DevKit.Core.Observables.API;

namespace DevKit.Nexus.Binding.Internals
{
    public class CollectionBindingPath : BindingPath, ICollectionObserver
    {
        private static readonly Dictionary<CollectionChangedEventAction, Action<IList, CollectionChangedEventArgs>> ListActions = new()
            {
                { CollectionChangedEventAction.Add, Add }
                , { CollectionChangedEventAction.Remove, Remove }
                , { CollectionChangedEventAction.Replace, Replace }
                , { CollectionChangedEventAction.Reset, Reset }
            };

        private static readonly Dictionary<CollectionChangedEventAction, Action<IDictionary, CollectionChangedEventArgs>> DicActions = new()
            {
                { CollectionChangedEventAction.Add, Add }
                , { CollectionChangedEventAction.Remove, Remove }
                , { CollectionChangedEventAction.Replace, Replace }
                , { CollectionChangedEventAction.Reset, Reset }
            };

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

            switch (Source)
            {
                case IList list:
                    ListActions[args.Action].Invoke(list, args);
                    break;

                case IDictionary dic:
                    DicActions[args.Action].Invoke(dic, args);
                    break;
            }
        }

        public void OnError(object sender, CollectionChangedEventArgs args)
        {
            Error = args.Error;
        }

        private static void Add(IList source, CollectionChangedEventArgs args)
        {
            foreach (var item in args.NewItems)
            {
                source.Add(item);
            }
        }

        private static void Add(IDictionary source, CollectionChangedEventArgs args)
        {
            var eItems = args.NewItems.GetEnumerator();
            eItems.Reset();

            var eKeys = args.NewItems.GetEnumerator();
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

        private static void Remove(IList source, CollectionChangedEventArgs args)
        {
            foreach (var item in args.PrevItems)
            {
                source.Remove(item);
            }
        }

        private static void Remove(IDictionary source, CollectionChangedEventArgs args)
        {
            foreach (var key in args.PrevItems)
            {
                source.Remove(key);
            }
        }

        private static void Replace(IList source, CollectionChangedEventArgs args)
        {
            var eItems = args.NewItems.GetEnumerator();
            eItems.Reset();

            var eKeys = args.NewItems.GetEnumerator();
            eKeys.Reset();

            while (eItems.MoveNext() && eKeys.MoveNext())
            {
                if (eKeys.Current == null)
                {
                    continue;
                }
                source.Insert((int)eKeys.Current, eItems.Current);
            }
        }

        private static void Replace(IDictionary source, CollectionChangedEventArgs args)
        {
            var eItems = args.NewItems.GetEnumerator();
            eItems.Reset();

            var eKeys = args.NewItems.GetEnumerator();
            eKeys.Reset();

            while (eItems.MoveNext() && eKeys.MoveNext())
            {
                if (eKeys.Current == null)
                {
                    continue;
                }
                source[eKeys.Current] = eItems.Current;
            }
        }

        private static void Reset(IList source, CollectionChangedEventArgs args)
        {
            source.Clear();
        }

        private static void Reset(IDictionary source, CollectionChangedEventArgs args)
        {
            source.Clear();
        }
    }
}
