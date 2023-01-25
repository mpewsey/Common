using MPewsey.Common.Logging;
using System.Collections.Generic;

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
        /// Invokes all generators of the pipeline and returns the results.
        /// </summary>
        /// <param name="inputs">A dictionary of generator inputs.</param>
        public PipelineResults Generate(Dictionary<string, object> inputs)
        {
            Logger.Log("Running pipeline...");
            var results = new PipelineResults(inputs);

            foreach (var step in Steps)
            {
                if (!step.ApplyStep(results))
                {
                    Logger.Log("Pipeline failed.");
                    return results;
                }
            }

            results.Complete();
            Logger.Log("Pipeline complete.");
            return results;
        }
    }
}
