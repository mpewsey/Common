using System;
using System.Runtime.Serialization;

namespace MPewsey.Common.Mathematics
{
    /// <summary>
    /// A 3D vector with integer values.
    /// </summary>
    [DataContract(Namespace = Constants.DataContractNamespace)]
    public struct Vector3DInt : IEquatable<Vector3DInt>, IComparable<Vector3DInt>
    {
        /// <summary>
        /// Returns a zero vector.
        /// </summary>
        public static Vector3DInt Zero => new Vector3DInt();

        /// <summary>
        /// Returns a vector of ones.
        /// </summary>
        public static Vector3DInt One => new Vector3DInt(1, 1, 1);

        /// <summary>
        /// The x value.
        /// </summary>
        [DataMember(Order = 0, IsRequired = true)]
        public int X { get; private set; }

        /// <summary>
        /// The y value.
        /// </summary>
        [DataMember(Order = 1, IsRequired = true)]
        public int Y { get; private set; }

        /// <summary>
        /// The z value.
        /// </summary>
        [DataMember(Order = 2, IsRequired = true)]
        public int Z { get; private set; }

        /// <summary>
        /// Initializes a new vector.
        /// </summary>
        /// <param name="x">The x value.</param>
        /// <param name="y">The y value.</param>
        /// <param name="z">The z value.</param>
        public Vector3DInt(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static explicit operator Vector2DInt(Vector3DInt v) => new Vector2DInt(v.X, v.Y);

        public override string ToString()
        {
            return $"Vector3DInt({X}, {Y}, {Z})";
        }

        public override bool Equals(object obj)
        {
            return obj is Vector3DInt vector && Equals(vector);
        }

        public bool Equals(Vector3DInt other)
        {
            return X == other.X &&
                   Y == other.Y &&
                   Z == other.Z;
        }

        public int CompareTo(Vector3DInt other)
        {
            var comparison = X.CompareTo(other.X);

            if (comparison != 0)
                return comparison;

            comparison = Y.CompareTo(other.Y);

            if (comparison != 0)
                return comparison;

            return Z.CompareTo(other.Z);
        }

        public override int GetHashCode()
        {
            int hashCode = -307843816;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Z.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Vector3DInt left, Vector3DInt right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector3DInt left, Vector3DInt right)
        {
            return !(left == right);
        }

        public static Vector3DInt operator +(Vector3DInt left, Vector3DInt right)
        {
            return new Vector3DInt(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Vector3DInt operator -(Vector3DInt left, Vector3DInt right)
        {
            return new Vector3DInt(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public static Vector3DInt operator -(Vector3DInt vector)
        {
            return new Vector3DInt(-vector.X, -vector.Y, -vector.Z);
        }

        /// <summary>
        /// Returns the maximum values of the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        public static Vector3DInt Max(Vector3DInt value1, Vector3DInt value2)
        {
            return new Vector3DInt(Math.Max(value1.X, value2.X), Math.Max(value1.Y, value2.Y), Math.Max(value1.Z, value2.Z));
        }

        /// <summary>
        /// Returns the minimum values of the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        public static Vector3DInt Min(Vector3DInt value1, Vector3DInt value2)
        {
            return new Vector3DInt(Math.Min(value1.X, value2.X), Math.Min(value1.Y, value2.Y), Math.Min(value1.Z, value2.Z));
        }

        /// <summary>
        /// Returns the sign of the vector.
        /// </summary>
        /// <param name="vector">The vector.</param>
        public static Vector3DInt Sign(Vector3DInt vector)
        {
            return new Vector3DInt(Math.Sign(vector.X), Math.Sign(vector.Y), Math.Sign(vector.Z));
        }

        /// <summary>
        /// Returns the dot product of the two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        public static int Dot(Vector3DInt value1, Vector3DInt value2)
        {
            return value1.X * value2.X + value1.Y * value2.Y + value1.Z * value2.Z;
        }
    }
}
