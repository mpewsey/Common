using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MPewsey.Common.Pipelines
{
    /// <summary>
    /// A class for chaining multiple IPipelineStep together.
    /// </summary>
    public class Pipeline
    {
        /// <summary>
        /// A list of pipeline steps.
        /// </summary>
        public List<IPipelineStep> Steps { get; private set; } = new List<IPipelineStep>();

        /// <summary>
        /// Initializes a new pipeline.
        /// </summary>
        /// <param name="steps">The pipeline steps.</param>
        public Pipeline(params IPipelineStep[] steps)
        {
            Steps = new List<IPipelineStep>(steps);
        }

        /// <summary>
        /// Invokes all steps of the pipeline and returns the results.
        /// </summary>
        /// <param name="inputs">A dictionary of pipeline inputs.</param>
        /// <param name="logger">A logging action. Ignored if null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public PipelineResults Run(Dictionary<string, object> inputs, Action<string> logger = null, CancellationToken cancellationToken = default)
        {
            logger?.Invoke("[Pipeline] Running pipeline...");
            var results = new PipelineResults(inputs);

            foreach (var step in Steps)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    logger?.Invoke("[Pipeline] Cancelled process.");
                    return results;
                }

                if (!step.ApplyStep(results, logger, cancellationToken))
                {
                    logger?.Invoke("[Pipeline] Pipeline failed.");
                    return results;
                }
            }

            results.Complete();
            logger?.Invoke("[Pipeline] Pipeline complete.");
            return results;
        }

        /// <summary>
        /// Runs the pipeline asynchonously.
        /// </summary>
        /// <param name="inputs">A dictionary of pipeline inputs.</param>
        /// <param name="logger">A logging action. Ignored if null.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public Task<PipelineResults> RunAsync(Dictionary<string, object> inputs, Action<string> logger = null, CancellationToken cancellationToken = default)
        {
            return Task.Run(() => Run(inputs, logger, cancellationToken));
        }
    }
}
