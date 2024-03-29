﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            public bool ApplyStep(PipelineResults results, Action<string> logger, CancellationToken cancellationToken) => true;
        }

        public class FailingPipelineStep : IPipelineStep
        {
            public bool ApplyStep(PipelineResults results, Action<string> logger, CancellationToken cancellationToken) => false;
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
            var token = new CancellationTokenSource(0).Token;
            var pipeline = new Pipeline(new SucceedingPipelineStep());
            var inputs = new Dictionary<string, object>();
            var results = pipeline.Run(inputs, Console.WriteLine, token);
            Assert.IsFalse(results.Success);
        }

        [TestMethod]
        public async Task TestRunAsyncCancellationToken()
        {
            var token = new CancellationTokenSource(0).Token;
            var pipeline = new Pipeline(new SucceedingPipelineStep());
            var inputs = new Dictionary<string, object>();
            var results = await pipeline.RunAsync(inputs, Console.WriteLine, token);
            Assert.IsFalse(results.Success);
        }

        [TestMethod]
        public async Task TestRunAsync()
        {
            var pipeline = new Pipeline(new SucceedingPipelineStep());
            var inputs = new Dictionary<string, object>();
            var results = await pipeline.RunAsync(inputs);
            Assert.IsTrue(results.Success);
        }
    }
}