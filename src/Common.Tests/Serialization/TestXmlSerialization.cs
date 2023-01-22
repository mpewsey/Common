using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPewsey.Common.Collections;

namespace MPewsey.Common.Serialization.Tests
{
    [TestClass]
    public class TestXmlSerialization
    {
        [TestMethod]
        public void TestSaveAndLoad()
        {
            var path = "Array2D.xml";
            Array2D<int> array = new int[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 },
                { 10, 11, 12 },
            };

            XmlSerialization.SaveXml(path, array);
            var copy = XmlSerialization.LoadXml<Array2D<int>>(path);
            Assert.AreEqual(array.Rows, copy.Rows);
            Assert.AreEqual(array.Columns, copy.Columns);
            CollectionAssert.AreEqual(array.Array, copy.Array);
        }

        [TestMethod]
        public void TestEncryptedSaveAndLoad()
        {
            var path = "Array2DXml.sav";
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

            XmlSerialization.SaveEncryptedXml(path, array, key);
            var copy = XmlSerialization.LoadEncryptedXml<Array2D<int>>(path, key);
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

            var str = XmlSerialization.GetXmlString(array);
            var copy = XmlSerialization.LoadXmlString<Array2D<int>>(str);
            Assert.AreEqual(array.Rows, copy.Rows);
            Assert.AreEqual(array.Columns, copy.Columns);
            CollectionAssert.AreEqual(array.Array, copy.Array);
        }
    }
}