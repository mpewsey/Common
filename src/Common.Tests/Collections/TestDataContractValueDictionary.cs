using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPewsey.Common.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace MPewsey.Common.Collections.Tests
{
    [TestClass]
    public class TestDataContractValueDictionary
    {
        [DataContract]
        public class TestEntry : IDataContractValueDictionaryValue<int>
        {
            [DataMember(Order = 1)]
            public int Id { get; set; }
            public int Key => Id;

            public TestEntry(int id)
            {
                Id = id;
            }
        }

        [TestMethod]
        public void TestSaveAndLoad()
        {
            var path = "DataContractValueDictionary.xml";
            var dict = new DataContractValueDictionary<int, TestEntry>
            {
                { 1, new TestEntry(1) },
                { 3, new TestEntry(3) },
            };

            XmlSerialization.SaveXml(path, dict);
            var copy = XmlSerialization.LoadXml<DataContractValueDictionary<int, TestEntry>>(path);
            CollectionAssert.AreEquivalent(dict.Keys.ToList(), copy.Keys.ToList());
            CollectionAssert.AreEquivalent(dict.Values.Select(x => x.Id).ToList(), copy.Values.Select(x => x.Id).ToList());
        }

        [TestMethod]
        public void TestSaveAndLoadEmpty()
        {
            var path = "EmptyDataContractValueDictionary.xml";
            var dict = new DataContractValueDictionary<int, TestEntry>();

            XmlSerialization.SaveXml(path, dict);
            var copy = XmlSerialization.LoadXml<DataContractValueDictionary<int, TestEntry>>(path);
            CollectionAssert.AreEquivalent(dict.Keys.ToList(), copy.Keys.ToList());
            CollectionAssert.AreEquivalent(dict.Values.Select(x => x.Id).ToList(), copy.Values.Select(x => x.Id).ToList());
        }

        [TestMethod]
        public void TestInitializers()
        {
            var dict = new DataContractValueDictionary<int, TestEntry>(10);
            dict = new DataContractValueDictionary<int, TestEntry>(dict);
            Assert.AreEqual(0, dict.Count);
        }

        [TestMethod]
        public void TestDictionaryCast()
        {
            var dict = new Dictionary<int, TestEntry>();
            var cast = (DataContractValueDictionary<int, TestEntry>)dict;
            Assert.AreEqual(dict, cast.Dictionary);
        }
    }
}