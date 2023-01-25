using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace MPewsey.Common.Mathematics.Tests
{
    [TestClass]
    public class TestMaths
    {
        [TestMethod]
        public void TestCumSumDouble()
        {
            var values = new double[] { 1, 2, 3 };
            var result = Maths.CumSum(values);
            var expected = new double[] { 1, 3, 6 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestEmptyCumSumDouble()
        {
            var array = Array.Empty<double>();
            CollectionAssert.AreEqual(array, Maths.CumSum(array));
        }

        [TestMethod]
        public void TestCumSumFloat()
        {
            var values = new float[] { 1, 2, 3 };
            var result = Maths.CumSum(values);
            var expected = new double[] { 1, 3, 6 };
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestEmptyCumSumFloat()
        {
            var array = Array.Empty<float>();
            CollectionAssert.AreEqual(array, Maths.CumSum(array));
        }

        [TestMethod]
        public void TestSoftmaxFloat()
        {
            var values = new float[] { 3, 4, 1 };

            var expected = new double[]
            {
                0.25949646034242,
                0.70538451269824,
                0.03511902695934,
            };

            expected = expected.Select(x => Math.Round(x, 10)).ToArray();
            var result = Maths.Softmax(values).Select(x => Math.Round(x, 10)).ToArray();
            Console.WriteLine("Result:");
            Console.WriteLine(string.Join(", ", result));
            CollectionAssert.AreEqual(expected, result);
            CollectionAssert.AreEqual(Array.Empty<float>(), Maths.Softmax(Array.Empty<float>()));
        }

        [TestMethod]
        public void TestSoftmaxDouble()
        {
            var values = new double[] { 3, 4, 1 };

            var expected = new double[]
            {
                0.25949646034242,
                0.70538451269824,
                0.03511902695934,
            };

            expected = expected.Select(x => Math.Round(x, 10)).ToArray();
            var result = Maths.Softmax(values).Select(x => Math.Round(x, 10)).ToArray();
            Console.WriteLine("Result:");
            Console.WriteLine(string.Join(", ", result));
            CollectionAssert.AreEqual(expected, result);
            CollectionAssert.AreEqual(Array.Empty<double>(), Maths.Softmax(Array.Empty<double>()));
        }
    }
}