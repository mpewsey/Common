using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPewsey.Common.Collections;

namespace MPewsey.Common.Serialization.Tests
{
    [TestClass]
    public class TestJsonSerialization
    {
        [TestMethod]
        public void TestSaveAndLoad()
        {
            var path = "Array2D.json";
            Array2D<int> array = new int[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 },
                { 10, 11, 12 },
            };

            JsonSerialization.SaveJson(path, array);
            var copy = JsonSerialization.LoadJson<Array2D<int>>(path);
            Assert.AreEqual(array.Rows, copy.Rows);
            Assert.AreEqual(array.Columns, copy.Columns);
            CollectionAssert.AreEqual(array.Array, copy.Array);
        }

        [TestMethod]
        public void TestEncryptedSaveAndLoad()
        {
            var path = "Array2DJson.sav";
            Array2D<int> array = new int[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 },
                { 10, 11, 12 },
            };

            var key = new byte[32];

            for (int i = 0; i < key.Length; i++)
            {
                key[i] = (byte)i;
            }

            JsonSerialization.SaveEncryptedJson(path, array, key);
            var copy = JsonSerialization.LoadEncryptedJson<Array2D<int>>(path, key);
            Assert.AreEqual(array.Rows, copy.Rows);
            Assert.AreEqual(array.Columns, copy.Columns);
            CollectionAssert.AreEqual(array.Array, copy.Array);
        }

        [TestMethod]
        public void TestSaveAndLoadString()
        {
            Array2D<int> array = new int[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 },
                { 10, 11, 12 },
            };

            var str = JsonSerialization.GetJsonString(array);
            var copy = JsonSerialization.LoadJsonString<Array2D<int>>(str);
            Assert.AreEqual(array.Rows, copy.Rows);
            Assert.AreEqual(array.Columns, copy.Columns);
            CollectionAssert.AreEqual(array.Array, copy.Array);
        }
    }
}