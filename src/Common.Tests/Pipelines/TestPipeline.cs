using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MPewsey.Common.Pipelines.Tests
{
    [TestClass]
    public class TestPipeline
    {
        public class SucceedingPipelineStep : IPipelineStep
        {
            public bool ApplyStep(PipelineResults results, CancellationToken cancellationToken) => true;
        }

        public class FailingPipelineStep : IPipelineStep
        {
            public bool ApplyStep(PipelineResults results, CancellationToken cancellationToken) => false;
        }

        public class LongPipelineStep : IPipelineStep
        {
            public bool ApplyStep(PipelineResults results, CancellationToken cancellationToken)
            {
                Thread.Sleep(100);
                return true;
            }
        }

        [TestMethod]
        public void TestEmptyPipeline()
        {
            var pipeline = new Pipeline();
            var inputs = new Dictionary<string, object>();
            var results = pipeline.Run(inputs);
            Assert.IsTrue(results.Success);
        }

        [TestMethod]
        public void TestSuccessfulPipeline()
        {
            var pipeline = new Pipeline(new SucceedingPipelineStep());
            var inputs = new Dictionary<string, object>();
            var results = pipeline.Run(inputs);
            Assert.IsTrue(results.Success);
        }

        [TestMethod]
        public void TestFailedPipeline()
        {
            var pipeline = new Pipeline(new FailingPipelineStep());
            var inputs = new Dictionary<string, object>();
            var results = pipeline.Run(inputs);
            Assert.IsFalse(results.Success);
        }

        [TestMethod]
        public void TestCancellationToken()
        {
            var token = new CancellationTokenSource(50).Token;
            var pipeline = new Pipeline(new LongPipelineStep(), new LongPipelineStep());
            var inputs = new Dictionary<string, object>();
            var results = pipeline.Run(inputs, token);
            Assert.IsFalse(results.Success);
        }

        [TestMethod]
        public async Task TestRunAsync()
        {
            var token = new CancellationTokenSource(50).Token;
            var pipeline = new Pipeline(new LongPipelineStep(), new LongPipelineStep());
            var inputs = new Dictionary<string, object>();
            var results = await pipeline.RunAsync(inputs, token);
            Assert.IsFalse(results.Success);
        }
    }
}