using System;
using System.Runtime.Serialization;

namespace MPewsey.Common.Mathematics
{
    /// <summary>
    /// A 2D vector with integer values.
    /// </summary>
    [DataContract(Namespace = Constants.DataContractNamespace)]
    public struct Vector2DInt : IEquatable<Vector2DInt>, IComparable<Vector2DInt>
    {
        /// <summary>
        /// Returns a zero vector.
        /// </summary>
        public static Vector2DInt Zero => new Vector2DInt();

        /// <summary>
        /// Returns a vector of ones.
        /// </summary>
        public static Vector2DInt One => new Vector2DInt(1, 1);

        /// <summary>
        /// The X value.
        /// </summary>
        [DataMember(Order = 0, IsRequired = true)]
        public int X { get; private set; }

        /// <summary>
        /// The Y value.
        /// </summary>
        [DataMember(Order = 1, IsRequired = true)]
        public int Y { get; private set; }

        /// <summary>
        /// Initializes a new vector.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        public Vector2DInt(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static explicit operator Vector3DInt(Vector2DInt v) => new Vector3DInt(v.X, v.Y, 0);

        public override string ToString()
        {
            return $"Vector2DInt({X}, {Y})";
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2DInt vector && Equals(vector);
        }

        public bool Equals(Vector2DInt other)
        {
            return X == other.X &&
                   Y == other.Y;
        }

        public int CompareTo(Vector2DInt other)
        {
            var comparison = X.CompareTo(other.X);

            if (comparison != 0)
                return comparison;

            return Y.CompareTo(other.Y);
        }

        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Vector2DInt left, Vector2DInt right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector2DInt left, Vector2DInt right)
        {
            return !(left == right);
        }

        public static Vector2DInt operator +(Vector2DInt left, Vector2DInt right)
        {
            return new Vector2DInt(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2DInt operator -(Vector2DInt left, Vector2DInt right)
        {
            return new Vector2DInt(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2DInt operator -(Vector2DInt vector)
        {
            return new Vector2DInt(-vector.X, -vector.Y);
        }

        /// <summary>
        /// Returns the maximum values of the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        public static Vector2DInt Max(Vector2DInt value1, Vector2DInt value2)
        {
            return new Vector2DInt(Math.Max(value1.X, value2.X), Math.Max(value1.Y, value2.Y));
        }

        /// <summary>
        /// Returns the minimum values of the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        public static Vector2DInt Min(Vector2DInt value1, Vector2DInt value2)
        {
            return new Vector2DInt(Math.Min(value1.X, value2.X), Math.Min(value1.Y, value2.Y));
        }

        /// <summary>
        /// Returns the sign of the vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        public static Vector2DInt Sign(Vector2DInt vector)
        {
            return new Vector2DInt(Math.Sign(vector.X), Math.Sign(vector.Y));
        }

        /// <summary>
        /// Returns the dot product of the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        public static int Dot(Vector2DInt value1, Vector2DInt value2)
        {
            return value1.X * value2.X + value1.Y * value2.Y;
        }
    }
}
