using System;
using System.Collections.Generic;

namespace MPewsey.Common.Collections
{
    /// <summary>
    /// A class for calculating distances between cells.
    /// </summary>
    public class Array2DDistanceSearch<T>
    {
        /// <summary>
        /// The base array.
        /// </summary>
        private Array2D<T> Array { get; set; }

        /// <summary>
        /// The array of distances.
        /// </summary>
        private Array2D<int> Distances { get; set; }

        /// <summary>
        /// The empty cell predicate. The function should return true when an empty cell is encountered.
        /// </summary>
        private Func<T, bool> Predicate { get; }

        /// <summary>
        /// Initializes a new search.
        /// </summary>
        /// <param name="predicate">The empty cell predicate. If null, an empty cell will be assumed to be the default value for the type.</param>
        public Array2DDistanceSearch(Func<T, bool> predicate = null)
        {
            Predicate = predicate ?? DefaultPredicate;
        }

        /// <summary>
        /// The default empty cell predicate. Returns true when the value is equal to the default value for the type.
        /// </summary>
        /// <param name="value">The cell value.</param>
        private static bool DefaultPredicate(T value)
        {
            return EqualityComparer<T>.Default.Equals(value, default);
        }

        /// <summary>
        /// Initializes the search's buffers.
        /// </summary>
        /// <param name="array">The base array.</param>
        private void Initialize(Array2D<T> array)
        {
            Array = array;
            Distances = new Array2D<int>(array.Rows, array.Columns);
            Distances.Fill(-1);
        }

        /// <summary>
        /// Returns an array of distances from the specified index to each cell.
        /// Values of -1 indicate that the index does not exist.
        /// </summary>
        /// <param name="array">The base array.</param>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        public Array2D<int> FindDistances(Array2D<T> array, int row, int column)
        {
            Initialize(array);
            SearchDistances(row, column, 0);
            return Distances;
        }

        /// <summary>
        /// Performs a recursive crawl of the template cells to determine the distance to an index.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        /// <param name="distance">The distance to the current index.</param>
        private void SearchDistances(int row, int column, int distance)
        {
            if (!Array.IndexExists(row, column) || Predicate.Invoke(Array[row, column]) || (uint)Distances[row, column] <= distance)
                return;

            Distances[row, column] = distance++;
            SearchDistances(row, column - 1, distance);
            SearchDistances(row, column + 1, distance);
            SearchDistances(row - 1, column, distance);
            SearchDistances(row + 1, column, distance);
        }
    }
}
