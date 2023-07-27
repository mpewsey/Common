using System.Collections.Generic;

namespace MPewsey.Common.Buffers
{
    /// <summary>
    /// A pool of lists.
    /// </summary>
    public static class ListPool<T>
    {
        /// <summary>
        /// The list pool.
        /// </summary>
        private static Stack<List<T>> Pool { get; } = new Stack<List<T>>();

        /// <summary>
        /// Clears all lists from the pool.
        /// </summary>
        public static void Clear()
        {
            Pool.Clear();
        }

        /// <summary>
        /// Returns a list from the pool or a new list if the pool is empty.
        /// </summary>
        public static List<T> Rent()
        {
            if (Pool.Count > 0)
            {
                var list = Pool.Pop();
                list.Clear();
                return list;
            }

            return new List<T>();
        }

        /// <summary>
        /// Returns the specified list to the pool and sets the reference to null.
        /// </summary>
        /// <param name="list">The list.</param>
        public static void Return(ref List<T> list)
        {
            list.Clear();
            Pool.Push(list);
            list = null;
        }
    }
}
