using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MPewsey.Common.Logging.Tests
{
    [TestClass]
    public class TestLogger
    {
        [TestMethod]
        public void TestLog()
        {
            Logger.RemoveAllListeners();
            var messages = new List<string>();
            var expected = new List<string> { "Message1", "Message2", "Message2" };

            // Add the listener.
            Logger.AddListener(messages.Add);

            foreach (var message in expected)
            {
                Logger.Log(message);
            }

            CollectionAssert.AreEqual(expected, messages);

            // Remove the listener.
            Logger.RemoveListener(messages.Add);

            foreach (var message in expected)
            {
                Logger.Log(message);
            }

            CollectionAssert.AreEqual(expected, messages);
        }
    }
}