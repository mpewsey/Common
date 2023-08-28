using System;
using System.Collections.Generic;

namespace MPewsey.Common.Mathematics
{
    /// <summary>
    /// Contains extra math operations.
    /// </summary>
    public static class Maths
    {
        /// <summary>
        /// Returns the cumulative sums of the list.
        /// </summary>
        /// <param name="values">A list of values.</param>
        public static double[] CumSum(IList<double> values)
        {
            if (values.Count == 0)
                return Array.Empty<double>();

            double total = 0;
            var result = new double[values.Count];

            for (int i = 0; i < values.Count; i++)
            {
                total += values[i];
                result[i] = total;
            }

            return result;
        }

        /// <summary>
        /// Adds the cumulative sum of the list to teh specified results list.
        /// </summary>
        /// <param name="values">A list of values.</param>
        /// <param name="result">The results list.</param>
        public static void CumSum(IList<double> values, List<double> result)
        {
            result.Clear();
            double total = 0;

            for (int i = 0; i < values.Count; i++)
            {
                total += values[i];
                result.Add(total);
            }
        }

        /// <summary>
        /// Returns the cumulative sums of the list.
        /// </summary>
        /// <param name="values">A list of values.</param>
        public static double[] CumSum(IList<float> values)
        {
            if (values.Count == 0)
                return Array.Empty<double>();

            double total = 0;
            var result = new double[values.Count];

            for (int i = 0; i < values.Count; i++)
            {
                total += values[i];
                result[i] = total;
            }

            return result;
        }

        /// <summary>
        /// Adds the cumulative sum of the list to teh specified results list.
        /// </summary>
        /// <param name="values">A list of values.</param>
        /// <param name="result">The results list.</param>
        public static void CumSum(IList<float> values, List<double> result)
        {
            result.Clear();
            double total = 0;

            for (int i = 0; i < values.Count; i++)
            {
                total += values[i];
                result.Add(total);
            }
        }

        /// <summary>
        /// Returns the softmax of the collection of values.
        /// </summary>
        /// <param name="values">A list of values.</param>
        public static double[] Softmax(IList<double> values)
        {
            if (values.Count == 0)
                return Array.Empty<double>();

            double total = 0;
            var softmax = new double[values.Count];

            for (int i = 0; i < softmax.Length; i++)
            {
                var value = Math.Exp(values[i]);
                total += value;
                softmax[i] = value;
            }

            if (total > 0)
            {
                for (int i = 0; i < softmax.Length; i++)
                {
                    softmax[i] /= total;
                }
            }

            return softmax;
        }

        /// <summary>
        /// Returns the softmax of the collection of values.
        /// </summary>
        /// <param name="values">A list of values.</param>
        public static double[] Softmax(IList<float> values)
        {
            if (values.Count == 0)
                return Array.Empty<double>();

            double total = 0;
            var softmax = new double[values.Count];

            for (int i = 0; i < softmax.Length; i++)
            {
                var value = Math.Exp(values[i]);
                total += value;
                softmax[i] = value;
            }

            if (total > 0)
            {
                for (int i = 0; i < softmax.Length; i++)
                {
                    softmax[i] /= total;
                }
            }

            return softmax;
        }

        /// <summary>
        /// Returns the index in the collection where the first maximum value occurs.
        /// Returns -1 if the collection is empty.
        /// </summary>
        /// <param name="values">The collection of values.</param>
        public static int MaxIndex(IList<float> values)
        {
            if (values.Count == 0)
                return -1;

            var index = 0;
            var maxValue = values[0];

            for (int i = 1; i < values.Count; i++)
            {
                var value = values[i];

                if (value > maxValue)
                {
                    index = i;
                    maxValue = value;
                }
            }

            return index;
        }

        /// <summary>
        /// Returns the index in the collection where the first maximum value occurs.
        /// Returns -1 if the collection is empty.
        /// </summary>
        /// <param name="values">The collection of values.</param>
        public static int MaxIndex(IList<double> values)
        {
            if (values.Count == 0)
                return -1;

            var index = 0;
            var maxValue = values[0];

            for (int i = 1; i < values.Count; i++)
            {
                var value = values[i];

                if (value > maxValue)
                {
                    index = i;
                    maxValue = value;
                }
            }

            return index;
        }

        /// <summary>
        /// Returns the index in the collection where the first maximum value occurs.
        /// Returns -1 if the collection is empty.
        /// </summary>
        /// <param name="values">The collection of values.</param>
        public static int MaxIndex(IList<int> values)
        {
            if (values.Count == 0)
                return -1;

            var index = 0;
            var maxValue = values[0];

            for (int i = 1; i < values.Count; i++)
            {
                var value = values[i];

                if (value > maxValue)
                {
                    index = i;
                    maxValue = value;
                }
            }

            return index;
        }

        /// <summary>
        /// Returns the index in the collection where the first minimum value occurs.
        /// Returns -1 if the collection is empty.
        /// </summary>
        /// <param name="values">The collection of values.</param>
        public static int MinIndex(IList<float> values)
        {
            if (values.Count == 0)
                return -1;

            var index = 0;
            var minValue = values[0];

            for (int i = 1; i < values.Count; i++)
            {
                var value = values[i];

                if (value < minValue)
                {
                    index = i;
                    minValue = value;
                }
            }

            return index;
        }

        /// <summary>
        /// Returns the index in the collection where the first minimum value occurs.
        /// Returns -1 if the collection is empty.
        /// </summary>
        /// <param name="values">The collection of values.</param>
        public static int MinIndex(IList<double> values)
        {
            if (values.Count == 0)
                return -1;

            var index = 0;
            var minValue = values[0];

            for (int i = 1; i < values.Count; i++)
            {
                var value = values[i];

                if (value < minValue)
                {
                    index = i;
                    minValue = value;
                }
            }

            return index;
        }

        /// <summary>
        /// Returns the index in the collection where the first minimum value occurs.
        /// Returns -1 if the collection is empty.
        /// </summary>
        /// <param name="values">The collection of values.</param>
        public static int MinIndex(IList<int> values)
        {
            if (values.Count == 0)
                return -1;

            var index = 0;
            var minValue = values[0];

            for (int i = 1; i < values.Count; i++)
            {
                var value = values[i];

                if (value < minValue)
                {
                    index = i;
                    minValue = value;
                }
            }

            return index;
        }

        /// <summary>
        /// Clamps the value between 0 and 1.
        /// </summary>
        /// <param name="t">The value.</param>
        public static double Clamp01(double t)
        {
            if (t < 0)
                return 0;
            if (t > 1)
                return 1;
            return t;
        }

        /// <summary>
        /// Linearly interpolates between two points based on a fractional distance parameter.
        /// This method will clamp the parameter to [0, 1].
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <param name="t">The fractional distance parameter, measured from the first point.</param>
        public static double Lerp(double a, double b, double t)
        {
            return LerpUnclamped(a, b, Clamp01(t));
        }

        /// <summary>
        /// Linearly interpolates between two points based on a fractional distance parameter.
        /// </summary>
        /// <param name="a">The first point.</param>
        /// <param name="b">The second point.</param>
        /// <param name="t">The fractional distance parameter, measured from the first point.</param>
        public static double LerpUnclamped(double a, double b, double t)
        {
            return a * (1 - t) + b * t;
        }
    }
}
