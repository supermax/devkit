using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DevKit.Core.Observables.API;

namespace DevKit.Core.Observables
{
    [Serializable]
    [DataContract]
    public class ObservableCollection<T> :
        Observable
        , IObservableCollection<T>
        , ISerializable
    {
        [field: NonSerialized]
        public event CollectionChangedEventHandler CollectionChanged;

        [field: NonSerialized]
        protected readonly List<T> InnerList;

        public ObservableCollection()
        {
            InnerList = new List<T>();
        }

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

        public void Clear()
        {
            InnerList.Clear();
            InvokeCollectionChanged(CollectionChangedEventAction.Reset, default, default, default, default);
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

        public int Count
        {
            get
            {
                var count = InnerList.Count;
                return count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
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
