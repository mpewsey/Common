﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPewsey.Common.Serialization;
using System.Collections.Generic;
using System.Linq;

namespace MPewsey.Common.Collections.Tests
{
    [TestClass]
    public class TestSet
    {
        [TestMethod]
        public void TestSaveAndLoad()
        {
            var path = "Set.xml";
            var set = new Set<int> { 1, 2, 3 };
            XmlSerialization.SaveXml(path, set);
            var copy = XmlSerialization.LoadXml<Set<int>>(path);
            CollectionAssert.AreEquivalent(set.ToList(), copy.ToList());
        }

        [TestMethod]
        public void TestSaveAndLoadEmpty()
        {
            var path = "EmptySet.xml";
            var set = new Set<int>();
            XmlSerialization.SaveXml(path, set);
            var copy = XmlSerialization.LoadXml<Set<int>>(path);
            CollectionAssert.AreEquivalent(set.ToList(), copy.ToList());
        }

        [TestMethod]
        public void TestDictionaryCast()
        {
            Set<int> set = new HashSet<int>();
            Assert.AreEqual(0, set.Count);
        }

        [TestMethod]
        public void TestCollectionAdd()
        {
            ICollection<int> set = new Set<int>();
            set.Add(1);
            Assert.AreEqual(1, set.Count);
        }

        [TestMethod]
        public void TestStructEnumerator()
        {
            var set = new Set<int> { 1 };

            foreach (var value in set)
            {
                Assert.AreEqual(1, value);
            }
        }

        [TestMethod]
        public void TestEnumerator()
        {
            IEnumerable<int> set = new Set<int> { 1 };

            foreach (var value in set)
            {
                Assert.AreEqual(1, value);
            }
        }

        [TestMethod]
        public void TestGetEnumerator()
        {
            var set = new Set<int> { 1 };
            var enumerator = ((IEnumerable<int>)set).GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(1, enumerator.Current);
        }

        [TestMethod]
        public void TestAdd()
        {
            var set = new Set<int>();
            Assert.AreEqual(0, set.Count);
            set.Add(1);
            Assert.AreEqual(1, set.Count);
        }

        [TestMethod]
        public void TestRemove()
        {
            var set = new Set<int>();
            Assert.IsFalse(set.Remove(1));
            set.Add(1);
            Assert.IsTrue(set.Remove(1));
            Assert.AreEqual(0, set.Count);
        }

        [TestMethod]
        public void TestContains()
        {
            var set = new Set<int>();
            Assert.IsFalse(set.Contains(1));
            set.Add(1);
            Assert.IsTrue(set.Contains(1));
        }

        [TestMethod]
        public void TestCopyTo()
        {
            var expected = new int[] { 1, 2, 3, 4 };
            var set = new Set<int>(expected);
            var result = new int[4];
            set.CopyTo(result, 0);
            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod]
        public void TestAddRange()
        {
            var expected = new int[] { 1, 2, 3, 4 };
            var set = new Set<int>();
            set.UnionWith(expected);
            CollectionAssert.AreEquivalent(expected, set.ToList());
        }

        [TestMethod]
        public void TestClear()
        {
            var set = new Set<int> { 1, 2, 3, 4 };
            Assert.AreEqual(4, set.Count);
            set.Clear();
            Assert.AreEqual(0, set.Count);
        }

        [TestMethod]
        public void TestExceptWith()
        {
            var set1 = new int[] { 1, 2, 3, 4 };
            var set2 = new int[] { 5, 6, 7 };
            var set = new Set<int>();
            set.UnionWith(set1);
            set.UnionWith(set2);
            Assert.AreEqual(set1.Length + set2.Length, set.Count);
            set.ExceptWith(set2);
            CollectionAssert.AreEquivalent(set1, set.ToList());
        }

        [TestMethod]
        public void TestIntersectWith()
        {
            var set1 = new int[] { 1, 2, 3 };
            var set2 = new int[] { 3, 4, 5 };
            var expected = new int[] { 3 };
            var set = new Set<int>(set1);
            set.IntersectWith(set2);
            CollectionAssert.AreEquivalent(expected, set.ToList());
        }

        [TestMethod]
        public void TestIsProperSubsetOf()
        {
            var set1 = new int[] { 1, 2, 3 };
            var set2 = new int[] { 2, 3 };
            var set = new Set<int>(set2);
            Assert.IsTrue(set.IsProperSubsetOf(set1));
        }

        [TestMethod]
        public void TestIsSubsetOf()
        {
            var set1 = new int[] { 1, 2, 3 };
            var set2 = new int[] { 2, 3 };
            var set = new Set<int>(set2);
            Assert.IsTrue(set.IsSubsetOf(set1));
        }

        [TestMethod]
        public void TestIsProperSupersetOf()
        {
            var set1 = new int[] { 1, 2, 3 };
            var set2 = new int[] { 2, 3 };
            var set = new Set<int>(set1);
            Assert.IsTrue(set.IsProperSupersetOf(set2));
        }

        [TestMethod]
        public void TestIsSupersetOf()
        {
            var set1 = new int[] { 1, 2, 3 };
            var set2 = new int[] { 2, 3 };
            var set = new Set<int>(set1);
            Assert.IsTrue(set.IsSupersetOf(set2));
        }

        [TestMethod]
        public void TestIsReadOnly()
        {
            var set = new Set<int>();
            Assert.IsFalse(set.IsReadOnly);
        }

        [TestMethod]
        public void TestOverlaps()
        {
            var set1 = new int[] { 1, 2, 3 };
            var set2 = new int[] { 3, 4, 5 };
            var set = new Set<int>(set1);
            Assert.IsTrue(set.Overlaps(set2));
        }

        [TestMethod]
        public void TestSetEquals()
        {
            var expected = new int[] { 1, 2, 3 };
            var set = new Set<int>(expected);
            Assert.IsTrue(set.SetEquals(expected));
        }

        [TestMethod]
        public void TestSymmetricExceptWith()
        {
            var set1 = new int[] { 1, 2, 3 };
            var set2 = new int[] { 2, 3, 4 };
            var expected = new int[] { 1, 4 };
            var set = new Set<int>(set1);
            set.SymmetricExceptWith(set2);
            CollectionAssert.AreEquivalent(expected, set.ToList());
        }

        [TestMethod]
        public void TestUnionWith()
        {
            var set1 = new int[] { 1, 2, 3 };
            var set2 = new int[] { 2, 3, 4 };
            var expected = new int[] { 1, 2, 3, 4 };
            var set = new Set<int>(set1);
            set.UnionWith(set2);
            CollectionAssert.AreEquivalent(expected, set.ToList());
        }
    }
}