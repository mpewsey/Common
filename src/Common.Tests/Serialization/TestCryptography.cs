using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPewsey.Common.Collections;
using System;

namespace MPewsey.Common.Serialization.Tests
{
    [TestClass]
    public class TestCryptography
    {
        [TestMethod]
        public void TestDecryptTextFile()
        {
            var path = "DecryptTextFile.sav";
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
            var str = Cryptography.DecryptTextFile(path, key);
            Console.WriteLine(str);
            Assert.IsTrue(str.Contains("Rows"));
            Assert.IsTrue(str.Contains("Columns"));
            Assert.IsTrue(str.Contains("Array"));
        }
    }
}