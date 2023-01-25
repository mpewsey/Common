using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MPewsey.Common.Pipelines.Tests
{
    [TestClass]
    public class TestPipeline
    {
        public class SucceedingPipelineStep : IPipelineStep
        {
            public bool ApplyStep(PipelineResults results) => true;
        }

        public class FailingPipelineStep : IPipelineStep
        {
            public bool ApplyStep(PipelineResults results) => false;
        }

        [TestMethod]
        public void TestEmptyPipeline()
        {
            var pipeline = new Pipeline(new SucceedingPipelineStep());
            var inputs = new Dictionary<string, object>();
            var results = pipeline.Generate(inputs);
            Assert.IsTrue(results.Success);
        }

        [TestMethod]
        public void TestFailedPipeline()
        {
            var pipeline = new Pipeline(new FailingPipelineStep());
            var inputs = new Dictionary<string, object>();
            var results = pipeline.Generate(inputs);
            Assert.IsFalse(results.Success);
        }
    }
}