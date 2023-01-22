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
        /// <param name="values">An list of values.</param>
        public static double[] CumSum(IList<double> values)
        {
            if (values.Count == 0)
                return Array.Empty<double>();

            double total = 0;
            var totals = new double[values.Count];

            for (int i = 0; i < totals.Length; i++)
            {
                total += values[i];
                totals[i] = total;
            }

            return totals;
        }

        /// <summary>
        /// Returns the cumulative sums of the list.
        /// </summary>
        /// <param name="values">An list of values.</param>
        public static double[] CumSum(IList<float> values)
        {
            if (values.Count == 0)
                return Array.Empty<double>();

            double total = 0;
            var totals = new double[values.Count];

            for (int i = 0; i < totals.Length; i++)
            {
                total += values[i];
                totals[i] = total;
            }

            return totals;
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
    }
}
