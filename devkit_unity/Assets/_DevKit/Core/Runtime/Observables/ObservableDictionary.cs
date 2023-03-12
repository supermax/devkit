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
        Observable<IObservableCollection<TKey, TValue>>
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

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            var e = InnerDictionary.GetEnumerator();
            return e;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            var e = InnerDictionary.GetEnumerator();
            return e;
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            InnerDictionary.Add(item.Key, item.Value);
            InvokeCollectionChanged(CollectionChangedEventAction.Add, default, item.Key, default, item.Value);
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

        public int Count
        {
            get
            {
                var count = InnerDictionary.Count;
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
