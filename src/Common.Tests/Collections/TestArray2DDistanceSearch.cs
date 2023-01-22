using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MPewsey.Common.Collections.Tests
{
    [TestClass]
    public class TestArray2DDistanceSearch
    {
        public class Cell
        {

        }

        [TestMethod]
        public void TestFindDistances()
        {
            Cell x = null;
            Cell o = new Cell();

            var cells = new Cell[,]
            {
                { o, o, o, o },
                { o, x, o, o },
                { o, o, x, o },
                { o, o, x, o },
                { o, o, o, o },
            };

            var search = new Array2DDistanceSearch<Cell>();
            var distances = search.FindDistances(cells, 0, 0);

            var expected = new int[,]
            {
                { 0,  1,  2, 3 },
                { 1, -1,  3, 4 },
                { 2,  3, -1, 5 },
                { 3,  4, -1, 6 },
                { 4,  5,  6, 7 },
            };

            var array = new Array2D<int>(expected);
            CollectionAssert.AreEqual(array.Array, distances.Array);
        }
    }
}