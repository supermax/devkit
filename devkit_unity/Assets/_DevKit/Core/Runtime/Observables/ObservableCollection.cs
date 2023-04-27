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
        public event CollectionChangedEventHandler<T> CollectionChanged;

        [NonSerialized]
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

        protected virtual void InvokeCollectionChanged(CollectionChangedEventAction actionType, int oldIndex, int newIndex, T prevValue, T newValue)
        {
            if (CollectionChanged == null)
            {
                return;
            }
            var args = CollectionChangedEventArgs<T>.Create(this, actionType, oldIndex, newIndex, prevValue, newValue);
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

        int IList.Add(object value)
        {
            var item = (T)value;
            Add(item);
            var index = InnerList.IndexOf(item);
            return index;
        }

        public void Clear()
        {
            InnerList.Clear();
            InvokeCollectionChanged(CollectionChangedEventAction.Reset, default, default, default, default);
        }

        bool IList.Contains(object value)
        {
            return Contains((T)value);
        }

        int IList.IndexOf(object value)
        {
            return IndexOf((T)value);
        }

        void IList.Insert(int index, object value)
        {
            Insert(index, (T)value);
        }

        void IList.Remove(object value)
        {
            Remove((T)value);
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

        void ICollection.CopyTo(Array array, int index)
        {
            for (var i = index; i < InnerList.Count && i < array.Length; i++)
            {
                array.SetValue(InnerList[i], i);
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

        bool ICollection.IsSynchronized
        {
            get
            {
                return true;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                return InnerList;
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
                return this[index];
            }
            set
            {
                this[index] = (T)value;
            }
        }

        public void Add(T item)
        {
            InnerList.Add(item);
            var index = InnerList.IndexOf(item);
            InvokeCollectionChanged(CollectionChangedEventAction.Add, default, index, default, item);
        }

        public bool Remove(T item)
        {
            var index = InnerList.IndexOf(item);
            var success = InnerList.Remove(item);
            InvokeCollectionChanged(CollectionChangedEventAction.Remove, index, default, item, default);
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
            InvokeCollectionChanged(CollectionChangedEventAction.Replace, index, index, prevItem, item);
        }

        public void RemoveAt(int index)
        {
            var prevItem = this[index];
            InnerList.RemoveAt(index);
            InvokeCollectionChanged(CollectionChangedEventAction.Remove, index, index, prevItem, default);
        }

        bool IList.IsFixedSize
        {
            get
            {
                return false;
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
                    var oldValue = InnerList[index];
                    InnerList[index] = value;
                    InvokeCollectionChanged(CollectionChangedEventAction.Replace, index, index, oldValue, value);
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
