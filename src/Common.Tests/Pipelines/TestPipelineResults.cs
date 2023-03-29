using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MPewsey.Common.Pipelines.Tests
{
    [TestClass]
    public class TestPipelineResults
    {
        [TestMethod]
        public void TestGetInput()
        {
            var inputs = new Dictionary<string, object>
            {
                { "Key", 1 }
            };

            var results = new PipelineResults(inputs);
            Assert.AreEqual(1, results.GetInput<int>("Key"));
        }

        [TestMethod]
        public void TestGetInputThrowsInvalidCastException()
        {
            var inputs = new Dictionary<string, object>
            {
                { "Key", 1 }
            };

            var results = new PipelineResults(inputs);
            Assert.ThrowsException<System.InvalidCastException>(() => results.GetInput<float>("Key"));
        }

        [TestMethod]
        public void TestGetOutput()
        {
            var inputs = new Dictionary<string, object>();
            var results = new PipelineResults(inputs);
            results.Outputs.Add("Key", 1);
            Assert.AreEqual(1, results.GetOutput<int>("Key"));
        }

        [TestMethod]
        public void TestGetOutputThrowsInvalidCastException()
        {
            var inputs = new Dictionary<string, object>();
            var results = new PipelineResults(inputs);
            results.Outputs.Add("Key", 1);
            Assert.ThrowsException<System.InvalidCastException>(() => results.GetOutput<float>("Key"));
        }

        [TestMethod]
        public void TestGetArgument()
        {
            var inputs = new Dictionary<string, object>
            {
                { "Key1", 1 }
            };

            var results = new PipelineResults(inputs);
            results.Outputs.Add("Key2", 2);
            Assert.AreEqual(1, results.GetArgument<int>("Key1"));
            Assert.AreEqual(2, results.GetArgument<int>("Key2"));
        }

        [TestMethod]
        public void TestGetArgumentThrowsInvalidCastExceptionForInputs()
        {
            var inputs = new Dictionary<string, object>
            {
                { "Key", 1 }
            };

            var results = new PipelineResults(inputs);
            Assert.ThrowsException<System.InvalidCastException>(() => results.GetArgument<float>("Key"));
        }

        [TestMethod]
        public void TestGetArgumentThrowsInvalidCastExceptionForOutputs()
        {
            var inputs = new Dictionary<string, object>();
            var results = new PipelineResults(inputs);
            results.Outputs.Add("Key", 1);
            Assert.ThrowsException<System.InvalidCastException>(() => results.GetArgument<float>("Key"));
        }

        [TestMethod]
        public void TestComplete()
        {
            var inputs = new Dictionary<string, object>();
            var results = new PipelineResults(inputs);
            Assert.IsFalse(results.Success);
            results.Complete();
            Assert.IsTrue(results.Success);
        }

        [TestMethod]
        public void TestSetOutput()
        {
            var inputs = new Dictionary<string, object>();
            var results = new PipelineResults(inputs);
            results.SetOutput("Key", 1);
            Assert.AreEqual(1, results.GetOutput<int>("Key"));
        }

        [TestMethod]
        public void TestAddOutput()
        {
            var inputs = new Dictionary<string, object>();
            var results = new PipelineResults(inputs);
            results.AddOutput("Key", 1);
            Assert.AreEqual(1, results.GetOutput<int>("Key"));
        }
    }
}