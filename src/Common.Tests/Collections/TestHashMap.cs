using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPewsey.Common.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MPewsey.Common.Collections.Tests
{
    [TestClass]
    public class TestHashMap
    {
        [TestMethod]
        public void TestSaveAndLoad()
        {
            var path = "HashMap.xml";
            var dict = new HashMap<int, int>
            {
                { 1, 2 },
                { 3, 4 },
            };

            XmlSerialization.SaveXml(path, dict);
            var copy = XmlSerialization.LoadXml<HashMap<int, int>>(path);
            CollectionAssert.AreEquivalent(dict.Keys.ToList(), copy.Keys.ToList());
            CollectionAssert.AreEquivalent(dict.Values.ToList(), copy.Values.ToList());
        }

        [TestMethod]
        public void TestSaveAndLoadEmptyDictionary()
        {
            var path = "EmptyHashMap.xml";
            var dict = new HashMap<int, int>();
            XmlSerialization.SaveXml(path, dict);
            var copy = XmlSerialization.LoadXml<HashMap<int, int>>(path);
            CollectionAssert.AreEquivalent(dict.Keys.ToList(), copy.Keys.ToList());
            CollectionAssert.AreEquivalent(dict.Values.ToList(), copy.Values.ToList());
        }

        [TestMethod]
        public void TestInitializer()
        {
            var dict = new HashMap<int, int>(10);
            Assert.AreEqual(0, dict.Count);
        }

        [TestMethod]
        public void TestIsReadOnly()
        {
            var dict = new HashMap<int, int>();
            Assert.IsFalse(dict.IsReadOnly);
        }

        [TestMethod]
        public void TestStructKeys()
        {
            var dict = new HashMap<int, int>
            {
                { 1, 2 },
                { 3, 4 },
            };

            var expected = new int[] { 1, 3 };
            CollectionAssert.AreEquivalent(expected, dict.Keys.ToList());
        }

        [TestMethod]
        public void TestStructValues()
        {
            var dict = new HashMap<int, int>
            {
                { 1, 2 },
                { 3, 4 },
            };

            var expected = new int[] { 2, 4 };
            CollectionAssert.AreEquivalent(expected, dict.Values.ToList());
        }

        [TestMethod]
        public void TestKeys()
        {
            IDictionary<int, int> dict = new HashMap<int, int>
            {
                { 1, 2 },
                { 3, 4 },
            };

            var expected = new int[] { 1, 3 };
            CollectionAssert.AreEquivalent(expected, dict.Keys.ToList());
        }

        [TestMethod]
        public void TestValues()
        {
            IDictionary<int, int> dict = new HashMap<int, int>
            {
                { 1, 2 },
                { 3, 4 },
            };

            var expected = new int[] { 2, 4 };
            CollectionAssert.AreEquivalent(expected, dict.Values.ToList());
        }

        [TestMethod]
        public void TestAdd()
        {
            var dict = new HashMap<int, int>();
            dict.Add(1, 2);
            Assert.AreEqual(2, dict[1]);
        }

        [TestMethod]
        public void TestAddKeyValue()
        {
            var dict = new HashMap<int, int>();
            dict.Add(new KeyValuePair<int, int>(1, 2));
            Assert.AreEqual(2, dict[1]);
        }

        [TestMethod]
        public void TestClear()
        {
            var dict = new HashMap<int, int>
            {
                { 1, 2 },
                { 3, 4 },
            };

            Assert.AreEqual(2, dict.Count);
            dict.Clear();
            Assert.AreEqual(0, dict.Count);
        }

        [TestMethod]
        public void TestContainsKey()
        {
            var dict = new HashMap<int, int>
            {
                { 1, 2 },
                { 3, 4 },
            };

            Assert.IsTrue(dict.ContainsKey(1));
        }

        [TestMethod]
        public void TestContains()
        {
            var dict = new HashMap<int, int>
            {
                { 1, 2 },
                { 3, 4 },
            };

            Assert.IsTrue(dict.Contains(new KeyValuePair<int, int>(1, 2)));
        }

        [TestMethod]
        public void TestRemove()
        {
            var dict = new HashMap<int, int>
            {
                { 1, 2 },
                { 3, 4 },
            };

            Assert.IsTrue(dict.Remove(1));
            Assert.AreEqual(1, dict.Count);
        }

        [TestMethod]
        public void TestEnumerator()
        {
            var dict = new HashMap<int, int>
            {
                { 1, 2 },
                { 3, 4 },
            };

            foreach (var pair in dict)
            {
                Assert.IsTrue(dict.Contains(pair));
            }
        }

        [TestMethod]
        public void TestIsSynchronized()
        {
            var dict = new HashMap<int, int>();
            Assert.IsFalse(dict.IsSynchronized);
        }

        [TestMethod]
        public void TestSyncRoot()
        {
            var dict = new HashMap<int, int>();
            Assert.IsNotNull(dict.SyncRoot);
        }

        [TestMethod]
        public void TestReadOnlyEnumerable()
        {
            IReadOnlyDictionary<int, int> dict = new HashMap<int, int>
            {
                { 1, 2 },
            };

            foreach (var pair in dict)
            {
                Assert.AreEqual(1, pair.Key);
                Assert.AreEqual(2, pair.Value);
            }
        }

        [TestMethod]
        public void TestReadOnlyKeys()
        {
            IReadOnlyDictionary<int, int> dict = new HashMap<int, int>
            {
                { 1, 2 },
            };

            foreach (var value in dict.Keys)
            {
                Assert.AreEqual(1, value);
            }
        }

        [TestMethod]
        public void TestReadOnlyValues()
        {
            IReadOnlyDictionary<int, int> dict = new HashMap<int, int>
            {
                { 1, 2 },
            };

            foreach (var value in dict.Values)
            {
                Assert.AreEqual(2, value);
            }
        }

        [TestMethod]
        public void TestCollectionCopyTo()
        {
            var dict = new HashMap<int, int>
            {
                { 1, 2 },
            };

            Array array = new KeyValuePair<int, int>[10];
            dict.CopyTo(array, 0);
        }

        [TestMethod]
        public void TestRemoveKeyValuePair()
        {
            var dict = new HashMap<int, int>
            {
                { 1, 2 },
            };

            Assert.IsTrue(dict.Remove(new KeyValuePair<int, int>(1, 2)));
        }

        [TestMethod]
        public void TestGetEnumerator()
        {
            IEnumerable dict = new HashMap<int, int>
            {
                { 1, 2 },
            };

            foreach (var value in dict)
            {
                Assert.IsNotNull(value);
            }
        }

        [TestMethod]
        public void TestCopyInitializer()
        {
            var dict = new HashMap<int, int>
            {
                { 1, 2 },
                { 3, 4 },
            };

            var copy = new HashMap<int, int>(dict);
            CollectionAssert.AreEquivalent(dict.Keys, copy.Keys);
            CollectionAssert.AreEquivalent(dict.Values, copy.Values);
        }

        [TestMethod]
        public void TestDictionaryCast()
        {
            var dict = new Dictionary<int, int>
            {
                { 1, 2 },
                { 3, 4 },
            };

            var cast = (HashMap<int, int>)dict;
            Assert.AreEqual(dict, cast.Dictionary);
        }

        [TestMethod]
        public void TestTryGetValue()
        {
            var dict = new HashMap<int, int>
            {
                { 1, 2 },
                { 3, 4 },
            };

            Assert.IsTrue(dict.TryGetValue(1, out int value));
            Assert.AreEqual(2, value);
        }

        [TestMethod]
        public void TestCopyTo()
        {
            var dict = new HashMap<int, int>
            {
                { 1, 2 },
                { 3, 4 },
            };

            var expected = dict.ToArray();
            var result = new KeyValuePair<int, int>[dict.Count];
            dict.CopyTo(result, 0);
            CollectionAssert.AreEquivalent(expected, result);
        }
    }
}