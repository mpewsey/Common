using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MPewsey.Common.Collections
{
    /// <summary>
    /// A dictionary whose values are data contract serializable.
    /// </summary>
    [DataContract(Name = "ValuHashMap", Namespace = Constants.DataContractNamespace)]
    public class ValueHashMap<TKey, TValue> : HashMapBase<TKey, TValue> where TValue : IValueHashMapEntry<TKey>
    {
        /// <summary>
        /// An array of dictionary values.
        /// </summary>
        [DataMember(Order = 1, Name = "Values")]
        public TValue[] ValuesArray { get => GetValuesArray(); set => SetDictionary(value); }

        /// <summary>
        /// Initializes a new empty dictionary.
        /// </summary>
        public ValueHashMap()
        {

        }

        /// <summary>
        /// Initializes a new dictionary with the specified capacity.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public ValueHashMap(int capacity)
        {
            Dictionary = new Dictionary<TKey, TValue>(capacity);
        }

        /// <summary>
        /// Initializes a copy of the specified dictionary.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        public ValueHashMap(ValueHashMap<TKey, TValue> dict)
        {
            Dictionary = new Dictionary<TKey, TValue>(dict.Dictionary);
        }

        /// <summary>
        /// Creates a new data contract dictionary instance with the specified dictionary assigned.
        /// </summary>
        /// <param name="dict">The dictionary.</param>
        public static implicit operator ValueHashMap<TKey, TValue>(Dictionary<TKey, TValue> dict)
        {
            return dict == null ? null : new ValueHashMap<TKey, TValue> { Dictionary = dict };
        }

        /// <summary>
        /// Returns a new array of dictionary values.
        /// </summary>
        private TValue[] GetValuesArray()
        {
            if (Dictionary.Count == 0)
                return System.Array.Empty<TValue>();

            var array = new TValue[Dictionary.Count];
            Dictionary.Values.CopyTo(array, 0);
            return array;
        }

        /// <summary>
        /// Sets the dictionary from an array of dictionary values.
        /// </summary>
        /// <param name="array">An array of dictionary values.</param>
        private void SetDictionary(TValue[] array)
        {
            Dictionary = new Dictionary<TKey, TValue>(array.Length);

            foreach (var value in array)
            {
                Dictionary.Add(value.Key, value);
            }
        }

        /// <summary>
        /// Adds a value to the dictionary.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Add(TValue value)
        {
            Add(value.Key, value);
        }

        /// <summary>
        /// Sets a value to the dictionary.
        /// </summary>
        /// <param name="value">The value.</param>
        public void SetValue(TValue value)
        {
            this[value.Key] = value;
        }
    }
}
