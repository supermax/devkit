using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DevKit.Core.Extensions;
using DevKit.Core.Observables.API;

namespace DevKit.Core.Observables
{
    [Serializable]
    [DataContract]
    public class ObservableCollection<T> :
        Observable
        , IObservableCollection<T>
        , IList
        , ISerializable
    {
        [field: NonSerialized]
        public event CollectionChangedEventHandler CollectionChanged;

        [field: NonSerialized]
        protected readonly List<T> InnerList = new();

        public IObservableCollection Subscribe(ICollectionObserver observer)
        {
            observer.ThrowIfNull(nameof(observer));

            CollectionChanged += observer.OnCollectionChanged;
            return this;
        }

        public IObservableCollection Unsubscribe(ICollectionObserver observer)
        {
            observer.ThrowIfNull(nameof(observer));

            CollectionChanged -= observer.OnCollectionChanged;
            return this;
        }

        public ObservableCollection() { }

        public ObservableCollection(IEnumerable<T> source)
        {
            InnerList = new List<T>(source);
        }

        public ObservableCollection(SerializationInfo info, StreamingContext context)
        {
            InnerList = (List<T>) info.GetValue(nameof(InnerList), typeof(List<T>));
        }

        protected virtual void InvokeCollectionChanged(
            CollectionChangedEventAction actionType
            , IEnumerable<int> prevIndexes
            , IEnumerable<int> newIndexes
            , IEnumerable<T> prevItems
            , IEnumerable<T> newItems)
        {
            if (CollectionChanged == null)
            {
                return;
            }
            var args = new CollectionChangedEventArgs(this, actionType, prevIndexes, newIndexes, prevItems, newItems);
            CollectionChanged.Invoke(this, args);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CollectionChanged = null;
                Clear();
            }
            base.Dispose(disposing);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var e = InnerList.GetEnumerator();
            return e;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var e = InnerList.GetEnumerator();
            return e;
        }

        public int Add(object value)
        {
            return ((IList)InnerList).Add(value);
        }

        public void Clear()
        {
            InnerList.Clear();
            InvokeCollectionChanged(CollectionChangedEventAction.Reset, default, default, default, default);
        }

        public bool Contains(object value)
        {
            return ((IList)InnerList).Contains(value);
        }

        public int IndexOf(object value)
        {
            return ((IList)InnerList).IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            ((IList)InnerList).Insert(index, value);
        }

        public void Remove(object value)
        {
            ((IList)InnerList).Remove(value);
        }

        public bool Contains(T item)
        {
            var contains = InnerList.Contains(item);
            return contains;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (var i = arrayIndex; i < InnerList.Count && i < array.Length; i++)
            {
                array[i] = InnerList[i];
            }
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection)InnerList).CopyTo(array, index);
        }

        public int Count
        {
            get
            {
                var count = InnerList.Count;
                return count;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return ((ICollection)InnerList).IsSynchronized;
            }
        }

        public object SyncRoot
        {
            get
            {
                return ((ICollection)InnerList).SyncRoot;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        object IList.this[int index]
        {
            get
            {
                return ((IList)InnerList)[index];
            }
            set
            {
                ((IList)InnerList)[index] = value;
            }
        }

        public void Add(T item)
        {
            InnerList.Add(item);
            var index = InnerList.IndexOf(item);
            InvokeCollectionChanged(
                CollectionChangedEventAction.Add
                , default
                , new []{index}
                , default
                , new []{item});
        }

        public bool Remove(T item)
        {
            var index = InnerList.IndexOf(item);
            var success = InnerList.Remove(item);
            InvokeCollectionChanged(
                CollectionChangedEventAction.Remove
                , new []{index}
                , default
                , new []{item}
                , default);
            return success;
        }

        public int IndexOf(T item)
        {
            var index = InnerList.IndexOf(item);
            return index;
        }

        public void Insert(int index, T item)
        {
            var prevItem = this[index];
            InnerList.Insert(index, item);
            var indexes = new[] {index};
            var items = new[] {prevItem};
            InvokeCollectionChanged(
                CollectionChangedEventAction.Replace
                , indexes
                , indexes
                , items
                , items);
        }

        public void RemoveAt(int index)
        {
            var prevItem = this[index];
            InnerList.RemoveAt(index);
            InvokeCollectionChanged(
                CollectionChangedEventAction.Remove
                , new []{index}
                , default
                , new []{prevItem}
                , default);
        }

        public bool IsFixedSize
        {
            get
            {
                return ((IList)InnerList).IsFixedSize;
            }
        }

        public T this[int index]
        {
            get
            {
                var res = InnerList[index];
                return res;
            }
            set
            {
                if (InnerList.Contains(value))
                {
                    Insert(index, value);
                    return;
                }
                Add(value);
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(InnerList), InnerList, InnerList.GetType());
        }
    }
}
