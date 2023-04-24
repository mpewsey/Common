using MPewsey.Common.Mathematics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;

namespace MPewsey.Common.Collections
{
    /// <summary>
    /// A 2D array that can be serialized.
    /// </summary>
    [DataContract(Name = "Array2D", Namespace = Constants.DataContractNamespace)]
    public class Array2D<T>
    {
        /// <summary>
        /// The number of rows in the array.
        /// </summary>
        [DataMember(Order = 1, IsRequired = true)]
        public int Rows { get; private set; }

        /// <summary>
        /// The number of columns in the array.
        /// </summary>
        [DataMember(Order = 2, IsRequired = true)]
        public int Columns { get; private set; }

        /// <summary>
        /// The underlying flat array.
        /// </summary>
        [DataMember(Order = 3, IsRequired = true)]
        public T[] Array { get; private set; } = System.Array.Empty<T>();

        /// <summary>
        /// Initializes an empty array.
        /// </summary>
        public Array2D()
        {

        }

        /// <summary>
        /// Initializes an array by size.
        /// </summary>
        /// <param name="rows">The number of rows in the array.</param>
        /// <param name="columns">The number of columns in the array.</param>
        /// <exception cref="ArgumentException">Raised if either the input rows or columns are negative.</exception>
        public Array2D(int rows, int columns)
        {
            if (rows < 0)
                throw new ArgumentException($"Rows cannot be negative: {rows}.");
            if (columns < 0)
                throw new ArgumentException($"Columns cannot be negative: {columns}.");

            if (rows > 0 && columns > 0)
            {
                Rows = rows;
                Columns = columns;
                Array = new T[rows * columns];
            }
        }

        /// <summary>
        /// Initializes a copy of an array.
        /// </summary>
        /// <param name="other">The original array.</param>
        public Array2D(Array2D<T> other)
        {
            if (other.Rows > 0 && other.Columns > 0)
            {
                Rows = other.Rows;
                Columns = other.Columns;
                Array = new T[Rows * Columns];
                other.Array.CopyTo(Array, 0);
            }
        }

        /// <summary>
        /// Initializes an array from a built-in 2D array.
        /// </summary>
        /// <param name="array">The built-in 2D array.</param>
        public Array2D(T[,] array)
        {
            if (array.Length > 0)
            {
                Rows = array.GetLength(0);
                Columns = array.GetLength(1);
                Array = FlattenArray(array);
            }
        }

        /// <summary>
        /// Accesses the array by 2D index.
        /// </summary>
        /// <param name="row">The row index.</param>
        /// <param name="column">The column index.</param>
        /// <exception cref="IndexOutOfRangeException">Raised if the index is out of range.</exception>
        public T this[int row, int column]
        {
            get
            {
                if (!IndexExists(row, column))
                    throw new IndexOutOfRangeException($"Index out of range: ({row}, {column}).");
                return Array[Index(row, column)];
            }
            set
            {
                if (!IndexExists(row, column))
                    throw new IndexOutOfRangeException($"Index out of range: ({row}, {column}).");
                Array[Index(row, column)] = value;
            }
        }

        /// <summary>
        /// Accesses the array by 2D index.
        /// </summary>
        /// <param name="index">The index vector, where X is the row and Y is the column.</param>
        public T this[Vector2DInt index]
        {
            get => this[index.X, index.Y];
            set => this[index.X, index.Y] = value;
        }

        /// <summary>
        /// Implicitly casts a built-in 2D array to an Array2D.
        /// </summary>
        /// <param name="array">The built-in 2D array.</param>
        public static implicit operator Array2D<T>(T[,] array) => new Array2D<T>(array);

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"Array2D<{typeof(T)}>(Rows = {Rows}, Columns = {Columns})";
        }

        /// <summary>
        /// Returns a shallow copy of the array.
        /// </summary>
        public Array2D<T> Copy()
        {
            return new Array2D<T>(this);
        }

        /// <summary>
        /// Returns a string of all array elements.
        /// </summary>
        public string ToArrayString()
        {
            var size = 2 + 2 * Array.Length + 4 * Rows;
            var builder = new StringBuilder(size);
            builder.Append('[');

            for (int i = 0; i < Rows; i++)
            {
                builder.Append('[');

                for (int j = 0; j < Columns; j++)
                {
                    builder.Append(this[i, j]);

                    if (j < Columns - 1)
                        builder.Append(", ");
                }

                builder.Append(']');

                if (i < Rows - 1)
                    builder.Append("\n ");
            }

            builder.Append(']');
            return builder.ToString();
        }

        /// <summary>
        /// Sets the contents of the array to the default value.
        /// </summary>
        public void Clear()
        {
            System.Array.Clear(Array, 0, Array.Length);
        }

        /// <summary>
        /// Sets all elements of the array to the value.
        /// </summary>
        /// <param name="value">The fill value.</param>
        public void Fill(T value)
        {
            for (int i = 0; i < Array.Length; i++)
            {
                Array[i] = value;
            }
        }

        /// <summary>
        /// Returns true if the values in the arrays of equal based on the default comparer.
        /// </summary>
        /// <param name="x">The first array.</param>
        /// <param name="y">The second array.</param>
        public static bool ValuesAreEqual(Array2D<T> x, Array2D<T> y)
        {
            return ValuesAreEqual(x, y, EqualityComparer<T>.Default.Equals);
        }

        /// <summary>
        /// Returns true if the values in the arrays are equal.
        /// </summary>
        /// <param name="x">The first array.</param>
        /// <param name="y">The second array.</param>
        /// <param name="comparer">The equality comparer.</param>
        public static bool ValuesAreEqual(Array2D<T> x, Array2D<T> y, Func<T, T, bool> comparer)
        {
            if (x == y)
                return true;

            if (x == null || y == null)
                return false;

            return x.ValuesAreEqual(y, comparer);
        }

        /// <summary>
        /// Returns true if the values in the arrays are equal based on the default comparer.
        /// </summary>
        /// <param name="other">The other array.</param>
        public bool ValuesAreEqual(Array2D<T> other)
        {
            return ValuesAreEqual(other, EqualityComparer<T>.Default.Equals);
        }

        /// <summary>
        /// Returns true if the values in the arrays are equal.
        /// </summary>
        /// <param name="other">The other array.</param>
        /// <param name="comparer">The element equality comparer.</param>
        public bool ValuesAreEqual(Array2D<T> other, Func<T, T, bool> comparer)
        {
            if (Array.Length != other.Array.Length || Rows != other.Rows || Columns != other.Columns)
                return false;

            for (int i = 0; i < Array.Length; i++)
            {
                if (!comparer.Invoke(Array[i], other.Array[i]))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns true if the index exists.
        /// </summary>
        /// <param name="row">The row index.</param>
        /// <param name="column">The column index.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IndexExists(int row, int column)
        {
            return (uint)row < (uint)Rows && (uint)column < (uint)Columns;
        }

        /// <summary>
        /// Returns the flat array index corresponding to the specified 2D index.
        /// </summary>
        /// <param name="row">The row index.</param>
        /// <param name="column">The column index.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int Index(int row, int column)
        {
            return row * Columns + column;
        }

        /// <summary>
        /// Returns the 2D index corresponding to the specified flat index.
        /// </summary>
        /// <param name="index">The flat index.</param>
        /// <exception cref="IndexOutOfRangeException">Raised if the index is outside the bounds of the array.</exception>
        public Vector2DInt InverseIndex(int index)
        {
            if ((uint)index < (uint)Array.Length)
            {
                var row = index / Columns;
                var column = index - row * Columns;
                return new Vector2DInt(row, column);
            }

            throw new IndexOutOfRangeException($"Index out of range: {index}.");
        }

        /// <summary>
        /// Returns the first 2D index where the specified predicate is true.
        /// Returns a -1 vector if no index is found.
        /// </summary>
        /// <param name="predicate">The predicate, taking each element.</param>
        public Vector2DInt FindIndex(Func<T, bool> predicate)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (predicate.Invoke(Array[Index(i, j)]))
                        return new Vector2DInt(i, j);
                }
            }

            return new Vector2DInt(-1, -1);
        }

        /// <summary>
        /// Returns a list of indexes where the specified predicate is true.
        /// </summary>
        /// <param name="predicate">The predicate, taking each element.</param>
        public List<Vector2DInt> FindIndexes(Func<T, bool> predicate)
        {
            var result = new List<Vector2DInt>();

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (predicate.Invoke(Array[Index(i, j)]))
                        result.Add(new Vector2DInt(i, j));
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a new flattened array from a built-in 2D array.
        /// </summary>
        /// <param name="array">The built-in 2D array.</param>
        public static T[] FlattenArray(T[,] array)
        {
            var result = new T[array.Length];
            int k = 0;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    result[k++] = array[i, j];
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the value at the specified index if it exists. If not,
        /// returns the fallback value.
        /// </summary>
        /// <param name="row">The row index.</param>
        /// <param name="column">The column index.</param>
        /// <param name="fallback">The fallback value.</param>
        public T GetOrDefault(int row, int column, T fallback = default)
        {
            if (IndexExists(row, column))
                return Array[Index(row, column)];
            return fallback;
        }

        /// <summary>
        /// Returns the value at the specified index if it exists. If not,
        /// returns the fallback value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="fallback">The fallback value.</param>
        public T GetOrDefault(Vector2DInt index, T fallback = default)
        {
            return GetOrDefault(index.X, index.Y, fallback);
        }

        /// <summary>
        /// Returns the index corresponding to when the array is rotated clockwise 90 degrees.
        /// </summary>
        /// <param name="index">The index vector.</param>
        public Vector2DInt IndexRotated90(Vector2DInt index)
        {
            return IndexRotated90(index.X, index.Y);
        }

        /// <summary>
        /// Returns the index corresponding to when the array is rotated clockwise 90 degrees.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        public Vector2DInt IndexRotated90(int row, int column)
        {
            return new Vector2DInt(column, Rows - 1 - row);
        }

        /// <summary>
        /// Returns a new array rotated clockwise 90 degrees.
        /// </summary>
        public Array2D<T> Rotated90()
        {
            var rotation = new Array2D<T>(Columns, Rows);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    rotation[IndexRotated90(i, j)] = this[i, j];
                }
            }

            return rotation;
        }

        /// <summary>
        /// Returns the index corresponding to when the array is rotated 180 degrees.
        /// </summary>
        /// <param name="index">The index vector.</param>
        public Vector2DInt IndexRotated180(Vector2DInt index)
        {
            return IndexRotated180(index.X, index.Y);
        }

        /// <summary>
        /// Returns the index corresponding to when the array is rotated 180 degrees.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        public Vector2DInt IndexRotated180(int row, int column)
        {
            return new Vector2DInt(Rows - 1 - row, Columns - 1 - column);
        }

        /// <summary>
        /// Returns a new array rotated 180 degrees.
        /// </summary>
        public Array2D<T> Rotated180()
        {
            var rotation = new Array2D<T>(Rows, Columns);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    rotation[IndexRotated180(i, j)] = this[i, j];
                }
            }

            return rotation;
        }

        /// <summary>
        /// Returns the index corresponding to when the array is rotated clockwise 270 degrees.
        /// </summary>
        /// <param name="index">The index vector.</param>
        public Vector2DInt IndexRotated270(Vector2DInt index)
        {
            return IndexRotated270(index.X, index.Y);
        }

        /// <summary>
        /// Returns the index corresponding to when the array is rotated clockwise 270 degrees.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        public Vector2DInt IndexRotated270(int row, int column)
        {
            return new Vector2DInt(Columns - 1 - column, row);
        }

        /// <summary>
        /// Returns a new array rotated clockwise 270 degrees.
        /// </summary>
        public Array2D<T> Rotated270()
        {
            var rotation = new Array2D<T>(Columns, Rows);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    rotation[IndexRotated270(i, j)] = this[i, j];
                }
            }

            return rotation;
        }

        /// <summary>
        /// Returns the index corresponding to when the array is mirrored horizontally.
        /// </summary>
        /// <param name="index">The index vector.</param>
        public Vector2DInt IndexMirroredHorizontally(Vector2DInt index)
        {
            return IndexMirroredHorizontally(index.X, index.Y);
        }

        /// <summary>
        /// Returns the index corresponding to when the array is mirrored horizontally.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        public Vector2DInt IndexMirroredHorizontally(int row, int column)
        {
            return new Vector2DInt(row, Columns - 1 - column);
        }

        /// <summary>
        /// Returns a new array mirrored horizontally, i.e. about the vertical axis.
        /// </summary>
        public Array2D<T> MirroredHorizontally()
        {
            var mirror = new Array2D<T>(Rows, Columns);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    mirror[IndexMirroredHorizontally(i, j)] = this[i, j];
                }
            }

            return mirror;
        }

        /// <summary>
        /// Returns the index corresponding to when the array is mirrored vertically.
        /// </summary>
        /// <param name="index">The index vector.</param>
        public Vector2DInt IndexMirroredVertically(Vector2DInt index)
        {
            return IndexMirroredVertically(index.X, index.Y);
        }

        /// <summary>
        /// Returns the index corresponding to when the array is mirrored vertically.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        public Vector2DInt IndexMirroredVertically(int row, int column)
        {
            return new Vector2DInt(Rows - 1 - row, column);
        }

        /// <summary>
        /// Returns a new array mirrored vertically, i.e. about the horizontal axis.
        /// </summary>
        public Array2D<T> MirroredVertically()
        {
            var mirror = new Array2D<T>(Rows, Columns);

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    mirror[IndexMirroredVertically(i, j)] = this[i, j];
                }
            }

            return mirror;
        }
    }
}
