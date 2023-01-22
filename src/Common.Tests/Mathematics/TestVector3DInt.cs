using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPewsey.Common.Random;
using MPewsey.Common.Serialization;
using System;
using System.Collections.Generic;

namespace MPewsey.Common.Mathematics.Tests
{
    [TestClass]
    public class TestVector3DInt
    {
        [TestMethod]
        public void TestToString()
        {
            var v = new Vector3DInt(1, 2, 3);
            Assert.AreEqual("Vector3DInt(1, 2, 3)", v.ToString());
        }

        [TestMethod]
        public void TestSaveAndLoad()
        {
            var path = "Vector3DInt.xml";
            var v = new Vector3DInt(1, 2, 3);
            XmlSerialization.SaveXml(path, v);
            var copy = XmlSerialization.LoadXml<Vector3DInt>(path);
            Assert.AreEqual(v, copy);
        }

        [TestMethod]
        public void TestGetJsonString()
        {
            var v = new Vector3DInt(1, 2, 3);
            var str = JsonSerialization.GetJsonString(v);
            Console.WriteLine(str);
            var copy = JsonSerialization.LoadJsonString<Vector3DInt>(str);
            Assert.AreEqual(v, copy);
        }

        [TestMethod]
        public void TestEqualsOperator()
        {
            var x = new Vector3DInt(1, 2, 3);
            var y = new Vector3DInt(1, 2, 3);
            Assert.IsTrue(x == y);
        }

        [TestMethod]
        public void TestDoesNotEqualOperator()
        {
            var x = new Vector3DInt(1, 2, 3);
            var y = new Vector3DInt(1, 4, 3);
            Assert.IsTrue(x != y);
        }

        [TestMethod]
        public void TestEquals()
        {
            var x = new Vector3DInt(1, 2, 3);
            var y = new Vector3DInt(1, 2, 3);
            Assert.IsTrue(x.Equals(y));
        }

        [TestMethod]
        public void TestInitializers()
        {
            var x = new Vector3DInt(1, 2, 3);
            var y = new Vector3DInt(1, 2, 3);
            Assert.AreEqual(x.GetHashCode(), y.GetHashCode());
        }

        [TestMethod]
        public void TestAddOperator()
        {
            var x = new Vector3DInt(1, 2, 3);
            var y = new Vector3DInt(9, 4, 2);
            var expected = new Vector3DInt(10, 6, 5);
            Assert.AreEqual(expected, x + y);
        }

        [TestMethod]
        public void TestSubtractOperator()
        {
            var x = new Vector3DInt(1, 2, 3);
            var y = new Vector3DInt(9, 4, 2);
            var expected = new Vector3DInt(-8, -2, 1);
            Assert.AreEqual(expected, x - y);
        }

        [TestMethod]
        public void TestNegation()
        {
            var v = new Vector3DInt(100, 200, 300);
            Assert.AreEqual(new Vector3DInt(-100, -200, -300), -v);
        }

        [TestMethod]
        public void TestZero()
        {
            Assert.AreEqual(new Vector3DInt(0, 0, 0), Vector3DInt.Zero);
        }

        [TestMethod]
        public void TestOne()
        {
            Assert.AreEqual(new Vector3DInt(1, 1, 1), Vector3DInt.One);
        }

        [TestMethod]
        public void TestVector2DIntCast()
        {
            var v = new Vector3DInt(1, 2, 3);
            Assert.AreEqual(new Vector2DInt(1, 2), (Vector2DInt)v);
        }

        [TestMethod]
        public void TestSign()
        {
            Assert.AreEqual(new Vector3DInt(1, -1, 1), Vector3DInt.Sign(new Vector3DInt(100, -200, 300)));
            Assert.AreEqual(new Vector3DInt(-1, 1, -1), Vector3DInt.Sign(new Vector3DInt(-100, 200, -300)));
            Assert.AreEqual(Vector3DInt.Zero, Vector3DInt.Sign(Vector3DInt.Zero));
        }

        [TestMethod]
        public void TestMax()
        {
            var v1 = new Vector3DInt(200, -100, 30);
            var v2 = new Vector3DInt(-40, 10, 20);
            Assert.AreEqual(new Vector3DInt(200, 10, 30), Vector3DInt.Max(v1, v2));
        }

        [TestMethod]
        public void TestMin()
        {
            var v1 = new Vector3DInt(200, -100, 30);
            var v2 = new Vector3DInt(-40, 10, 20);
            Assert.AreEqual(new Vector3DInt(-40, -100, 20), Vector3DInt.Min(v1, v2));
        }

        [TestMethod]
        public void TestDot()
        {
            var v1 = new Vector3DInt(10, 2, 50);
            var v2 = new Vector3DInt(3, 40, 7);
            Assert.AreEqual(460, Vector3DInt.Dot(v1, v2));
        }

        [TestMethod]
        public void TestCompareTo()
        {
            var random = new RandomSeed(12345);
            var count = 100;
            var expected = new List<Vector3DInt>();

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    for (int k = 0; k < count; k++)
                    {
                        expected.Add(new Vector3DInt(i, j, k));
                    }
                }
            }

            var result = random.Shuffled(expected);
            result.Sort();
            CollectionAssert.AreEqual(expected, result);
        }
    }
}