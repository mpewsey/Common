﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPewsey.Common.Mathematics;
using MPewsey.Common.Serialization;
using System;

namespace MPewsey.Common.Collections.Tests
{
    [TestClass]
    public class TestBitArray2D
    {
        [TestMethod]
        public void TestSaveAndLoad()
        {
            var path = "BitArray2D.xml";
            var array = new BitArray2D(10, 20);
            array[1, 1] = true;
            array[2, 2] = true;
            array[9, 15] = true;
            XmlSerialization.SaveXml(path, array);
            var copy = XmlSerialization.LoadXml<BitArray2D>(path);
            Assert.AreEqual(array.Rows, copy.Rows);
            Assert.AreEqual(array.Columns, copy.Columns);
            CollectionAssert.AreEqual(array.Array, copy.Array);
        }

        [TestMethod]
        public void TestChunkSize()
        {
            Assert.AreEqual(32, BitArray2D.ChunkSize);
        }

        [TestMethod]
        public void TestEmptyInitializer()
        {
            var array = new BitArray2D();
            Assert.AreEqual(0, array.Rows);
            Assert.AreEqual(0, array.Columns);
            Assert.AreEqual(0, array.Array.Length);
        }

        [TestMethod]
        public void TestIndexSetterAndGetter()
        {
            const int rows = 16;
            const int columns = 2;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var value = 1 << (i * columns + j);
                    var array = new BitArray2D(rows, columns);
                    Assert.AreEqual(rows, array.Rows);
                    Assert.AreEqual(columns, array.Columns);
                    Assert.AreEqual(1, array.Array.Length);
                    Assert.AreEqual(0, array.Array[0]);
                    Assert.IsFalse(array[i, j]);
                    array[i, j] = true;
                    Assert.AreEqual(value, array.Array[0]);
                    Assert.IsTrue(array[i, j]);
                    array[i, j] = false;
                    Assert.AreEqual(0, array.Array[0]);
                    Assert.IsFalse(array[i, j]);
                }
            }
        }

        [TestMethod]
        public void TestToArrayString()
        {
            var values = new int[,]
            {
                { 0, 1, 0, 0, 1, 1, 1, 0 },
                { 1, 0, 1, 1, 0, 0, 0, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1 },
                { 0, 1, 0, 1, 0, 1, 0, 1 },
                { 1, 0, 1, 0, 1, 0, 1, 0 },
            };

            var array = new BitArray2D(values.GetLength(0), values.GetLength(1));
            Assert.AreEqual(2, array.Array.Length);

            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    array[i, j] = values[i, j] == 1;
                }
            }

            var str = array.ToArrayString();
            Console.WriteLine(str);
            var expected = "[[01001110]\n [10110001]\n [11111111]\n [01010101]\n [10101010]]";
            Assert.AreEqual(expected, str);
        }

        [TestMethod]
        public void TestClear()
        {
            var array = new BitArray2D(2, 3);

            for (int i = 0; i < array.Rows; i++)
            {
                for (int j = 0; j < array.Columns; j++)
                {
                    array[i, j] = true;
                }
            }

            array.Clear();

            for (int i = 0; i < array.Rows; i++)
            {
                for (int j = 0; j < array.Columns; j++)
                {
                    Assert.IsFalse(array[i, j]);
                }
            }
        }

        [TestMethod]
        public void TestGetOrDefault()
        {
            var array = new BitArray2D(2, 3);
            array[1, 1] = true;
            Assert.IsTrue(array.GetOrDefault(new Vector2DInt(-1, -1), true));
            Assert.IsTrue(array.GetOrDefault(new Vector2DInt(1, 1)));
        }

        [TestMethod]
        public void TestToString()
        {
            var result = new BitArray2D(1, 2).ToString();
            var expected = $"BitArray2D(Rows = 1, Columns = 2)";
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void TestInitializeNegativeRow()
        {
            Assert.ThrowsException<ArgumentException>(() => new BitArray2D(-1, 1));
        }

        [TestMethod]
        public void TestInitializeNegativeColumn()
        {
            Assert.ThrowsException<ArgumentException>(() => new BitArray2D(1, -1));
        }

        [TestMethod]
        public void TestGetOutOfBoundsIndex()
        {
            Assert.ThrowsException<IndexOutOfRangeException>(() => new BitArray2D()[-1, -1]);
        }

        [TestMethod]
        public void TestSetOutOfBoundsIndex()
        {
            Assert.ThrowsException<IndexOutOfRangeException>(() => new BitArray2D()[-1, -1] = true);
        }

        [TestMethod]
        public void TestCopy()
        {
            var array = new BitArray2D(10, 20);
            array[1, 1] = true;
            array[2, 2] = true;
            array[9, 15] = true;

            var copy = array.Copy();
            Assert.AreEqual(array.Rows, copy.Rows);
            Assert.AreEqual(array.Columns, copy.Columns);
            CollectionAssert.AreEqual(array.Array, copy.Array);
        }

        [TestMethod]
        public void TestVectorIndexer()
        {
            var array = new BitArray2D(10, 10);
            var index = new Vector2DInt(1, 2);
            array[index] = true;
            Assert.IsTrue(array[index]);
            Assert.AreEqual(array[1, 2], array[index]);
        }
    }
}