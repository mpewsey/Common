using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MPewsey.Common.Collections
{
    /// <summary>
    /// The base class for dictionaries with custom data contract serialization.
    /// </summary>
    [DataContract(Name = "BaseDataContractDictionary", Namespace = Constants.DataContractNamespace)]
    public abstract class BaseDataContractDictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IReadOnlyDictionary<TKey, TValue>, ICollection
    {
        /// <summary>
        /// The underlying dictionary.
        /// </summary>
        public Dictionary<TKey, TValue> Dictionary { get; protected set; } = new Dictionary<TKey, TValue>();

        /// <inheritdoc/>
        public TValue this[TKey key] { get => Dictionary[key]; set => Dictionary[key] = value; }

        /// <inheritdoc/>
        public int Count => Dictionary.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).IsReadOnly;

        /// <inheritdoc/>
        public Dictionary<TKey, TValue>.KeyCollection Keys => Dictionary.Keys;

        /// <inheritdoc/>
        ICollection<TKey> IDictionary<TKey, TValue>.Keys => Dictionary.Keys;

        /// <inheritdoc/>
        public Dictionary<TKey, TValue>.ValueCollection Values => Dictionary.Values;

        /// <inheritdoc/>
        ICollection<TValue> IDictionary<TKey, TValue>.Values => Dictionary.Values;

        /// <inheritdoc/>
        public bool IsSynchronized => ((ICollection)Dictionary).IsSynchronized;

        /// <inheritdoc/>
        public object SyncRoot => ((ICollection)Dictionary).SyncRoot;

        /// <inheritdoc/>
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => ((IReadOnlyDictionary<TKey, TValue>)Dictionary).Keys;

        /// <inheritdoc/>
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => ((IReadOnlyDictionary<TKey, TValue>)Dictionary).Values;

        /// <inheritdoc/>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).Add(item);
        }

        /// <inheritdoc/>
        public void Add(TKey key, TValue value)
        {
            Dictionary.Add(key, value);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            Dictionary.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).Contains(item);
        }

        /// <inheritdoc/>
        public bool ContainsKey(TKey key)
        {
            return Dictionary.ContainsKey(key);
        }

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).CopyTo(array, arrayIndex);
        }

        /// <inheritdoc/>
        public void CopyTo(Array array, int index)
        {
            ((ICollection)Dictionary).CopyTo(array, index);
        }

        /// <inheritdoc/>
        public Dictionary<TKey, TValue>.Enumerator GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return Dictionary.GetEnumerator();
        }

        /// <inheritdoc/>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)Dictionary).Remove(item);
        }

        /// <inheritdoc/>
        public bool Remove(TKey key)
        {
            return Dictionary.Remove(key);
        }

        /// <inheritdoc/>
        public bool TryGetValue(TKey key, out TValue value)
        {
            return Dictionary.TryGetValue(key, out value);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Dictionary).GetEnumerator();
        }
    }
}
