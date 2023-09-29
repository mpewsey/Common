using MPewsey.Common.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MPewsey.Common.Random
{
    /// <summary>
    /// A class for performing pseudo-random number generation.
    /// 
    /// References
    /// ----------
    /// * [1] Rossetta Code. Subtractive generator. Retrieved April 12, 2022, from https://rosettacode.org/wiki/Subtractive_generator.
    /// * [2] Microsoft Corporation. Reference Source .NET Framework 4.8. Retrieved April 12, 2022, from https://referencesource.microsoft.com/#mscorlib/system/random.cs,bb77e610694e64ca.
    /// </summary>
    [DataContract(Namespace = Constants.DataContractNamespace)]
    public class RandomSeed
    {
        /// <summary>
        /// The current global random number generator.
        /// </summary>
        public static RandomSeed Current { get; } = new RandomSeed();

        /// <summary>
        /// The random seed.
        /// </summary>
        [DataMember(Order = 1)]
        public int Seed { get; private set; }

        /// <summary>
        /// The first position of the randomizer.
        /// </summary>
        [DataMember(Order = 2)]
        private int Position1 { get; set; }

        /// <summary>
        /// The second position of the randomizer.
        /// </summary>
        [DataMember(Order = 3)]
        private int Position2 { get; set; }

        /// <summary>
        /// An array of previous seeds.
        /// </summary>
        [DataMember(Order = 4)]
        private int[] Seeds { get; set; } = new int[56];

        /// <summary>
        /// Initializes a new random seed based on the current system ticks.
        /// </summary>
        public RandomSeed()
        {
            SetSeed(Environment.TickCount);
        }

        /// <summary>
        /// Initializes a new random seed.
        /// </summary>
        /// <param name="seed">The random seed.</param>
        public RandomSeed(int seed)
        {
            SetSeed(seed);
        }

        /// <summary>
        /// Initializes a copy of a random seed.
        /// </summary>
        /// <param name="other">The random seed to be copied.</param>
        private RandomSeed(RandomSeed other)
        {
            Seed = other.Seed;
            Position1 = other.Position1;
            Position2 = other.Position2;
            Seeds = new int[other.Seeds.Length];
            Array.Copy(other.Seeds, Seeds, other.Seeds.Length);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"RandomSeed(Seed = {Seed})";
        }

        /// <summary>
        /// Returns a copy of the object.
        /// </summary>
        public RandomSeed Copy()
        {
            return new RandomSeed(this);
        }

        /// <summary>
        /// Returns the positive modulo of a value with respect to int.MaxValue.
        /// </summary>
        /// <param name="value">The value.</param>
        private static int Mod(int value)
        {
            if (value < 0)
                return value + int.MaxValue;
            return value;
        }

        /// <summary>
        /// Wraps an index if it exceeds the top array bounds.
        /// </summary>
        /// <param name="value">The index.</param>
        private static int WrapIndex(int value)
        {
            if (value > 55)
                return 1;
            return value;
        }

        /// <summary>
        /// Sets the random seed and initializes the randomizer.
        /// </summary>
        /// <param name="seed">The random seed.</param>
        public void SetSeed(int seed)
        {
            Seed = seed;
            Position1 = 0;
            Position2 = 21;
            var mj = seed == int.MinValue ? int.MaxValue : Math.Abs(seed);
            mj = 161803398 - mj;
            Seeds[55] = mj;
            var mk = 1;

            // Populate random seed array.
            for (int i = 1; i < 55; i++)
            {
                var index = 21 * i % 55;
                Seeds[index] = mk;
                mk = Mod(mj - mk);
                mj = Seeds[index];
            }

            // Generate random numbers to reduce seed bias.
            for (int k = 1; k < 5; k++)
            {
                for (int i = 1; i < 56; i++)
                {
                    Seeds[i] = Mod(Seeds[i] - Seeds[1 + (i + 30) % 55]);
                }
            }
        }

        /// <summary>
        /// Returns a random integer on the interval [0, int.MaxValue).
        /// </summary>
        /// <returns></returns>
        public int Next()
        {
            var i = WrapIndex(Position1 + 1);
            var j = WrapIndex(Position2 + 1);
            var next = Mod(Seeds[i] - Seeds[j]);

            if (next == int.MaxValue)
                next--;

            Seeds[i] = next;
            Position1 = i;
            Position2 = j;
            return next;
        }

        /// <summary>
        /// Returns a random value on the interval [0, maxValue).
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        public int Next(int maxValue)
        {
            return (int)(NextDouble() * maxValue);
        }

        /// <summary>
        /// Returns a random value on the interval [minValue, maxValue).
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        public int Next(int minValue, int maxValue)
        {
            var delta = maxValue - (long)minValue;
            var t = delta <= int.MaxValue ? NextDouble() : NextLargeDouble();
            return (int)((long)(t * delta) + minValue);
        }

        /// <summary>
        /// Returns a random float on the interval [0, 1).
        /// </summary>
        public float NextFloat()
        {
            return (float)NextDouble();
        }

        /// <summary>
        /// Returns a random float on the interval [0, maxValue).
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        public float NextFloat(float maxValue)
        {
            return (float)NextDouble(maxValue);
        }

        /// <summary>
        /// Returns a random float on the interval [minValue, maxValue).
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        public float NextFloat(float minValue, float maxValue)
        {
            return (float)NextDouble(minValue, maxValue);
        }

        /// <summary>
        /// Returns a random double on the interval [0, 1).
        /// </summary>
        public double NextDouble()
        {
            return Next() / (double)int.MaxValue;
        }

        /// <summary>
        /// Returns a random double on the interval [0, maxValue).
        /// </summary>
        /// <param name="maxValue">The maximum value.</param>
        public double NextDouble(double maxValue)
        {
            return NextDouble() * maxValue;
        }

        /// <summary>
        /// Returns a random double on the interval [minValue, maxValue).
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        public double NextDouble(double minValue, double maxValue)
        {
            var x = NextDouble();
            return x * maxValue + (1 - x) * minValue;
        }

        /// <summary>
        /// Returns a random floating precision point inside the unit circle.
        /// </summary>
        public (float x, float y) FloatInsideUnitCircle()
        {
            var (x, y) = DoubleInsideUnitCircle();
            return ((float)x, (float)y);
        }

        /// <summary>
        /// Returns a random double precision point inside the unit circle.
        /// </summary>
        public (double x, double y) DoubleInsideUnitCircle()
        {
            var radius = Math.Sqrt(NextDouble());
            var angle = NextDouble(2 * Math.PI);
            var x = radius * Math.Cos(angle);
            var y = radius * Math.Sin(angle);
            return (x, y);
        }

        /// <summary>
        /// Returns a random high resolution double on the interval [0, 1).
        /// </summary>
        private double NextLargeDouble()
        {
            var next = Next();

            if (Next() % 2 == 0)
                next = -next;

            double result = next;
            result += int.MaxValue - 1;
            result /= 2.0 * int.MaxValue - 1;
            return result;
        }

        /// <summary>
        /// Shuffles the specified list in place.
        /// </summary>
        /// <param name="list">The list to shuffle.</param>
        public void Shuffle<T>(IList<T> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                var j = Next(i, list.Count);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }

        /// <summary>
        /// Returns a new shuffled copy of the collection.
        /// </summary>
        /// <param name="collection">The collection</param>
        public List<T> Shuffled<T>(IEnumerable<T> collection)
        {
            var copy = new List<T>(collection);
            Shuffle(copy);
            return copy;
        }

        /// <summary>
        /// Draws an index from the list of cumulative weights.
        /// </summary>
        /// <param name="totals">A list of cumulative weights.</param>
        private int DrawIndex(IList<double> totals)
        {
            if (totals.Count > 0)
            {
                var value = NextDouble(totals[totals.Count - 1]);

                for (int i = 0; i < totals.Count; i++)
                {
                    if (value <= totals[i] && totals[i] > 0)
                        return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Draws a random weighted index from a list.
        /// </summary>
        /// <param name="weights">A list of weights.</param>
        public int DrawWeightedIndex(IList<double> weights)
        {
            if (weights.Count > 0)
            {
                var totals = new List<double>(weights.Count);
                return DrawWeightedIndex(weights, totals);
            }

            return -1;
        }

        /// <summary>
        /// Draws a random weighted index from a list.
        /// </summary>
        /// <param name="weights">A list of weights.</param>
        public int DrawWeightedIndex(IList<float> weights)
        {
            if (weights.Count > 0)
            {
                var totals = new List<double>(weights.Count);
                return DrawWeightedIndex(weights, totals);
            }

            return -1;
        }

        /// <summary>
        /// Draws a random weighted index from a list.
        /// The specified totals buffer allows for minimal garbage generation.
        /// </summary>
        /// <param name="weights">A list of weights.</param>
        /// <param name="totals">The totals buffer.</param>
        public int DrawWeightedIndex(IList<double> weights, List<double> totals)
        {
            Maths.CumSum(weights, totals);
            return DrawIndex(totals);
        }

        /// <summary>
        /// Draws a random weighted index from a list.
        /// The specified totals buffer allows for minimal garbage generation.
        /// </summary>
        /// <param name="weights">A list of weights.</param>
        /// <param name="totals">The totals buffer.</param>
        public int DrawWeightedIndex(IList<float> weights, List<double> totals)
        {
            Maths.CumSum(weights, totals);
            return DrawIndex(totals);
        }

        /// <summary>
        /// Draws a quantity of random weighted indexes from a list.
        /// Based on the draw weights and whether indexes are drawn with replacement, the resulting
        /// list may be less than the specified count.
        /// </summary>
        /// <param name="weights">A list of index draw weights.</param>
        /// <param name="count">The desired number of drawn indexes.</param>
        /// <param name="withReplacement">If false, the indexes will be drawn without replacement.</param>
        public List<int> DrawWeightedIndexes(IList<double> weights, int count, bool withReplacement)
        {
            if (withReplacement)
                return DrawWeightedIndexesWithReplacement(weights, count);

            return DrawWeightedIndexesWithoutReplacement(weights, count);
        }

        /// <summary>
        /// Draws a quantity of random weighted indexes from a list with replacement.
        /// </summary>
        /// <param name="weights">A list of index draw weights.</param>
        /// <param name="count">The desired number of drawn indexes.</param>
        private List<int> DrawWeightedIndexesWithReplacement(IList<double> weights, int count)
        {
            var result = new List<int>(count);

            if (weights.Count > 0)
            {
                var totals = Maths.CumSum(weights);

                for (int i = 0; i < count; i++)
                {
                    var index = DrawIndex(totals);

                    if ((uint)index < (uint)weights.Count)
                        result.Add(index);
                }
            }

            return result;
        }

        /// <summary>
        /// Draws a quantity of random weighted indexes from a list without replacement.
        /// </summary>
        /// <param name="weights">A list of index draw weights.</param>
        /// <param name="count">The desired number of drawn indexes.</param>
        private List<int> DrawWeightedIndexesWithoutReplacement(IList<double> weights, int count)
        {
            count = Math.Min(count, weights.Count);
            var result = new List<int>(count);

            if (weights.Count > 0)
            {
                var weightsCopy = weights.ToArray();
                var totals = new List<double>(weightsCopy.Length);

                for (int i = 0; i < count; i++)
                {
                    var index = DrawWeightedIndex(weightsCopy, totals);

                    if ((uint)index < (uint)weightsCopy.Length)
                    {
                        weightsCopy[index] = 0;
                        result.Add(index);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Draws a quantity of random weighted indexes from a list.
        /// Based on the draw weights and whether indexes are drawn with replacement, the resulting
        /// list may be less than the specified count.
        /// </summary>
        /// <param name="weights">A list of index draw weights.</param>
        /// <param name="count">The desired number of drawn indexes.</param>
        /// <param name="withReplacement">If false, the indexes will be drawn without replacement.</param>
        public List<int> DrawWeightedIndexes(IList<float> weights, int count, bool withReplacement)
        {
            if (withReplacement)
                return DrawWeightedIndexesWithReplacement(weights, count);

            return DrawWeightedIndexesWithoutReplacement(weights, count);
        }

        /// <summary>
        /// Draws a quantity of random weighted indexes from a list with replacement.
        /// </summary>
        /// <param name="weights">A list of index draw weights.</param>
        /// <param name="count">The desired number of drawn indexes.</param>
        private List<int> DrawWeightedIndexesWithReplacement(IList<float> weights, int count)
        {
            var result = new List<int>(count);

            if (weights.Count > 0)
            {
                var totals = Maths.CumSum(weights);

                for (int i = 0; i < count; i++)
                {
                    var index = DrawIndex(totals);

                    if ((uint)index < (uint)weights.Count)
                        result.Add(index);
                }
            }

            return result;
        }

        /// <summary>
        /// Draws a quantity of random weighted indexes from a list without replacement.
        /// </summary>
        /// <param name="weights">A list of index draw weights.</param>
        /// <param name="count">The desired number of drawn indexes.</param>
        private List<int> DrawWeightedIndexesWithoutReplacement(IList<float> weights, int count)
        {
            count = Math.Min(count, weights.Count);
            var result = new List<int>(count);

            if (weights.Count > 0)
            {
                var weightsCopy = weights.ToArray();
                var totals = new List<double>(weightsCopy.Length);

                for (int i = 0; i < count; i++)
                {
                    var index = DrawWeightedIndex(weightsCopy, totals);

                    if ((uint)index < (uint)weightsCopy.Length)
                    {
                        weightsCopy[index] = 0;
                        result.Add(index);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Draws a random number and returns true if it satisfies the specified probability.
        /// </summary>
        /// <param name="chance">The probability, on the interval [0, 1].</param>
        public bool ChanceSatisfied(double chance)
        {
            return (NextDouble() <= chance && chance > 0) || chance >= 1;
        }
    }
}
