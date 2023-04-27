using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DevKit.Core.Observables.API;

namespace DevKit.Core.Observables
{
    [Serializable]
    [DataContract]
    public class ObservableDictionary<TKey, TValue> :
        Observable
        , IObservableCollection<TKey, TValue>
        , ISerializable
    {
        [field: NonSerialized]
        public event CollectionChangedEventHandler<TKey, TValue> CollectionChanged;

        [NonSerialized]
        protected readonly Dictionary<TKey, TValue> InnerDictionary;

        public ObservableDictionary()
        {
            InnerDictionary = new Dictionary<TKey, TValue>();
        }

        public ObservableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            InnerDictionary = new Dictionary<TKey, TValue>(source);
        }

        public ObservableDictionary(SerializationInfo info, StreamingContext context)
        {
            InnerDictionary = (Dictionary<TKey, TValue>) info.GetValue(nameof(InnerDictionary), typeof(Dictionary<TKey, TValue>));
        }

        protected virtual void InvokeCollectionChanged(CollectionChangedEventAction actionType, TKey oldKey, TKey newKey, TValue prevValue, TValue newValue)
        {
            if (CollectionChanged == null)
            {
                return;
            }
            var args = CollectionChangedEventArgs<TKey, TValue>.Create(this, actionType, oldKey, newKey, prevValue, newValue);
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

        bool IDictionary.Contains(object key)
        {
            return InnerDictionary.ContainsKey((TKey)key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return InnerDictionary.GetEnumerator();
        }

        void IDictionary.Remove(object key)
        {
            InnerDictionary.Remove((TKey)key);
        }

        bool IDictionary.IsFixedSize
        {
            get
            {
                return false;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            var e = InnerDictionary.GetEnumerator();
            return e;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var e = (IDictionaryEnumerator)InnerDictionary.GetEnumerator();
            return e;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            InnerDictionary.Add(item.Key, item.Value);
            InvokeCollectionChanged(CollectionChangedEventAction.Add, default, item.Key, default, item.Value);
        }

        void IDictionary.Add(object key, object value)
        {
            InnerDictionary.Add((TKey)key, (TValue)value);
        }

        public void Clear()
        {
            InnerDictionary.Clear();
            InvokeCollectionChanged(CollectionChangedEventAction.Reset, default, default, default, default);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            var contains = InnerDictionary.ContainsKey(item.Key);
            return contains;
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            using var e = InnerDictionary.GetEnumerator();
            var i = 0;
            while (i < array.Length && e.MoveNext())
            {
                if (i >= arrayIndex)
                {
                    array[i++] = e.Current;
                }
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            var success = InnerDictionary.Remove(item.Key);
            InvokeCollectionChanged(CollectionChangedEventAction.Remove, item.Key, default, item.Value, default);
            return success;
        }

        void ICollection.CopyTo(Array array, int index)
        {
            using var e = InnerDictionary.GetEnumerator();
            var i = 0;
            while (i < array.Length && e.MoveNext())
            {
                if (i >= index)
                {
                    array.SetValue(e.Current, i++);
                }
            }
        }

        public int Count
        {
            get
            {
                var count = InnerDictionary.Count;
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
                return InnerDictionary;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        object IDictionary.this[object key]
        {
            get
            {
                return this[(TKey)key];
            }
            set
            {
                this[(TKey)key] = (TValue)value;
            }
        }

        public void Add(TKey key, TValue value)
        {
            InnerDictionary.Add(key, value);
            InvokeCollectionChanged(CollectionChangedEventAction.Add, default, key, default, value);
        }

        public bool ContainsKey(TKey key)
        {
            var contains = InnerDictionary.ContainsKey(key);
            return contains;
        }

        public bool Remove(TKey key)
        {
            var success = InnerDictionary.Remove(key);
            InvokeCollectionChanged(CollectionChangedEventAction.Remove, key, default, default, default);
            return success;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var success = InnerDictionary.TryGetValue(key, out value);
            return success;
        }

        public TValue this[TKey key]
        {
            get
            {
                var res = InnerDictionary[key];
                return res;
            }
            set
            {
                if (InnerDictionary.ContainsKey(key))
                {
                    var oldValue = InnerDictionary[key];
                    InnerDictionary[key] = value;
                    InvokeCollectionChanged(CollectionChangedEventAction.Replace, key, key, oldValue, value);
                    return;
                }
                Add(key, value);
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                var keys = InnerDictionary.Keys;
                return keys;
            }
        }

        ICollection IDictionary.Values
        {
            get
            {
                return InnerDictionary.Values;
            }
        }

        ICollection IDictionary.Keys
        {
            get
            {
                return InnerDictionary.Keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                var values = InnerDictionary.Values;
                return values;
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(InnerDictionary), InnerDictionary, InnerDictionary.GetType());
        }
    }
}
