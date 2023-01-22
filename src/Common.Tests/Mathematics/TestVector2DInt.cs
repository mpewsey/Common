using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPewsey.Common.Random;
using MPewsey.Common.Serialization;
using System.Collections.Generic;

namespace MPewsey.Common.Mathematics.Tests
{
    [TestClass]
    public class TestVector2DInt
    {
        [TestMethod]
        public void TestToString()
        {
            var v = new Vector2DInt(1, 2);
            Assert.AreEqual("Vector2DInt(1, 2)", v.ToString());
        }

        [TestMethod]
        public void TestSaveAndLoad()
        {
            var path = "Vector2DInt.xml";
            var v = new Vector2DInt(1, 2);
            XmlSerialization.SaveXml(path, v);
            var copy = XmlSerialization.LoadXml<Vector2DInt>(path);
            Assert.AreEqual(v, copy);
        }

        [TestMethod]
        public void TestEqualsOperator()
        {
            var x = new Vector2DInt(1, 2);
            var y = new Vector2DInt(1, 2);
            Assert.IsTrue(x == y);
        }

        [TestMethod]
        public void TestDoesNotEqualOperator()
        {
            var x = new Vector2DInt(1, 2);
            var y = new Vector2DInt(1, 4);
            Assert.IsTrue(x != y);
        }

        [TestMethod]
        public void TestEquals()
        {
            var x = new Vector2DInt(1, 2);
            var y = new Vector2DInt(1, 2);
            Assert.IsTrue(x.Equals(y));
        }

        [TestMethod]
        public void TestInitializers()
        {
            var x = new Vector2DInt(1, 2);
            var y = new Vector2DInt(1, 2);
            Assert.AreEqual(x.GetHashCode(), y.GetHashCode());
        }

        [TestMethod]
        public void TestAddOperator()
        {
            var x = new Vector2DInt(1, 2);
            var y = new Vector2DInt(9, 4);
            var expected = new Vector2DInt(10, 6);
            Assert.AreEqual(expected, x + y);
        }

        [TestMethod]
        public void TestSubtractOperator()
        {
            var x = new Vector2DInt(1, 2);
            var y = new Vector2DInt(9, 4);
            var expected = new Vector2DInt(-8, -2);
            Assert.AreEqual(expected, x - y);
        }

        [TestMethod]
        public void TestSign()
        {
            Assert.AreEqual(new Vector2DInt(1, -1), Vector2DInt.Sign(new Vector2DInt(100, -200)));
            Assert.AreEqual(new Vector2DInt(-1, 1), Vector2DInt.Sign(new Vector2DInt(-100, 200)));
            Assert.AreEqual(Vector2DInt.Zero, Vector2DInt.Sign(Vector2DInt.Zero));
        }

        [TestMethod]
        public void TestMax()
        {
            var v1 = new Vector2DInt(200, -100);
            var v2 = new Vector2DInt(-40, 10);
            Assert.AreEqual(new Vector2DInt(200, 10), Vector2DInt.Max(v1, v2));
        }

        [TestMethod]
        public void TestMin()
        {
            var v1 = new Vector2DInt(200, -100);
            var v2 = new Vector2DInt(-40, 10);
            Assert.AreEqual(new Vector2DInt(-40, -100), Vector2DInt.Min(v1, v2));
        }

        [TestMethod]
        public void TestNegation()
        {
            var v = new Vector2DInt(200, 300);
            Assert.AreEqual(new Vector2DInt(-200, -300), -v);
        }

        [TestMethod]
        public void TestZero()
        {
            Assert.AreEqual(new Vector2DInt(0, 0), Vector2DInt.Zero);
        }

        [TestMethod]
        public void TestOne()
        {
            Assert.AreEqual(new Vector2DInt(1, 1), Vector2DInt.One);
        }

        [TestMethod]
        public void TestVector3DIntCast()
        {
            var v = new Vector2DInt(1, 2);
            Assert.AreEqual(new Vector3DInt(1, 2, 0), (Vector3DInt)v);
        }

        [TestMethod]
        public void TestDot()
        {
            var v1 = new Vector2DInt(10, 2);
            var v2 = new Vector2DInt(3, 40);
            Assert.AreEqual(110, Vector2DInt.Dot(v1, v2));
        }

        [TestMethod]
        public void TestCompareTo()
        {
            var random = new RandomSeed(12345);
            var count = 100;
            var expected = new List<Vector2DInt>();

            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    expected.Add(new Vector2DInt(i, j));
                }
            }

            var result = random.Shuffled(expected);
            result.Sort();
            CollectionAssert.AreEqual(expected, result);
        }
    }
}