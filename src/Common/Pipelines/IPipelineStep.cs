namespace MPewsey.Common.Pipelines
{
    /// <summary>
    /// An interface for creating a step of a Pipeline.
    /// </summary>
    public interface IPipelineStep
    {
        /// <summary>
        /// Performs the operations for this step.
        /// Artifacts should be written to the results outputs.
        /// </summary>
        /// <param name="results">The pipeline results.</param>
        bool ApplyStep(PipelineResults results);
    }
}
