using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
        public void TestCumSumDoubleNonAlloc()
        {
            var values = new double[] { 1, 2, 3 };
            var result = new List<double>();
            Maths.CumSum(values, result);
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
        public void TestCumSumFloatNonAlloc()
        {
            var values = new float[] { 1, 2, 3 };
            var result = new List<double>();
            Maths.CumSum(values, result);
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

        [TestMethod]
        public void TestFloatMaxIndex()
        {
            var values = new float[] { 1, 10, 5 };
            Assert.AreEqual(1, Maths.MaxIndex(values));
            Assert.AreEqual(-1, Maths.MaxIndex(Array.Empty<float>()));
        }

        [TestMethod]
        public void TestDoubleMaxIndex()
        {
            var values = new double[] { 1, 10, 5 };
            Assert.AreEqual(1, Maths.MaxIndex(values));
            Assert.AreEqual(-1, Maths.MaxIndex(Array.Empty<double>()));
        }

        [TestMethod]
        public void TestIntMaxIndex()
        {
            var values = new int[] { 1, 10, 5 };
            Assert.AreEqual(1, Maths.MaxIndex(values));
            Assert.AreEqual(-1, Maths.MaxIndex(Array.Empty<int>()));
        }

        [TestMethod]
        public void TestFloatMinIndex()
        {
            var values = new float[] { 10, 1, 5 };
            Assert.AreEqual(1, Maths.MinIndex(values));
            Assert.AreEqual(-1, Maths.MinIndex(Array.Empty<float>()));
        }

        [TestMethod]
        public void TestDoubleMinIndex()
        {
            var values = new double[] { 10, 1, 5 };
            Assert.AreEqual(1, Maths.MinIndex(values));
            Assert.AreEqual(-1, Maths.MinIndex(Array.Empty<double>()));
        }

        [TestMethod]
        public void TestIntMinIndex()
        {
            var values = new int[] { 10, 1, 5 };
            Assert.AreEqual(1, Maths.MinIndex(values));
            Assert.AreEqual(-1, Maths.MinIndex(Array.Empty<int>()));
        }

        [TestMethod]
        public void TestClamp01()
        {
            Assert.AreEqual(0, Maths.Clamp01(-1));
            Assert.AreEqual(1, Maths.Clamp01(2));
            Assert.AreEqual(0.5, Maths.Clamp01(0.5));
        }

        [TestMethod]
        public void TestLerp()
        {
            Assert.AreEqual(10.0, Maths.Lerp(10, 30, 0));
            Assert.AreEqual(10.0, Maths.Lerp(10, 30, -1));
            Assert.AreEqual(30.0, Maths.Lerp(10, 30, 1));
            Assert.AreEqual(30.0, Maths.Lerp(10, 30, 2));
            Assert.AreEqual(20.0, Maths.Lerp(10, 30, 0.5));
            Assert.AreEqual(15.0, Maths.Lerp(10, 30, 0.25));
            Assert.AreEqual(25.0, Maths.Lerp(10, 30, 0.75));
        }

        [TestMethod]
        public void TestLerpUnclamped()
        {
            Assert.AreEqual(10.0, Maths.LerpUnclamped(10, 30, 0));
            Assert.AreEqual(-10.0, Maths.LerpUnclamped(10, 30, -1));
            Assert.AreEqual(30.0, Maths.LerpUnclamped(10, 30, 1));
            Assert.AreEqual(50.0, Maths.LerpUnclamped(10, 30, 2));
            Assert.AreEqual(20.0, Maths.LerpUnclamped(10, 30, 0.5));
            Assert.AreEqual(15.0, Maths.LerpUnclamped(10, 30, 0.25));
            Assert.AreEqual(25.0, Maths.LerpUnclamped(10, 30, 0.75));
        }
    }
}