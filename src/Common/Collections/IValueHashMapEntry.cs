namespace MPewsey.Common.Collections
{
    /// <summary>
    /// An interface for values of the ValueHashMap.
    /// </summary>
    public interface IValueHashMapEntry<T>
    {
        /// <summary>
        /// The unique key.
        /// </summary>
        T Key { get; }
    }
}
