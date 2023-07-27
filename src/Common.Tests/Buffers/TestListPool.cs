using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MPewsey.Common.Buffers.Tests
{
    [TestClass]
    public class TestListPool
    {
        [TestMethod]
        public void TestClear()
        {
            ListPool<float>.Clear();
        }

        [TestMethod]
        public void TestRentAndReturn()
        {
            var list = ListPool<float>.Rent();
            var reference = list;
            list.Add(1);
            list.Add(2);
            Assert.AreEqual(2, list.Count);
            ListPool<float>.Return(ref list);
            Assert.IsNull(list);
            Assert.AreEqual(0, reference.Count);

            list = ListPool<float>.Rent();
            Assert.AreEqual(0, list.Count);
            Assert.AreEqual(reference, list);
            ListPool<float>.Return(ref list);
            Assert.IsNull(list);
        }
    }
}