using System.Collections.Generic;

namespace MPewsey.Common.Pipelines
{
    /// <summary>
    /// A container for holding pipeline results.
    /// </summary>
    public class PipelineResults
    {
        /// <summary>
        /// A dictionary of pipeline inputs.
        /// </summary>
        public Dictionary<string, object> Inputs { get; } = new Dictionary<string, object>();

        /// <summary>
        /// A dictionary of pipeline output results.
        /// </summary>
        public Dictionary<string, object> Outputs { get; } = new Dictionary<string, object>();

        /// <summary>
        /// True if the pipeline steps are successful.
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// Initializes a new result.
        /// </summary>
        /// <param name="inputs">The input dictionary.</param>
        public PipelineResults(Dictionary<string, object> inputs)
        {
            Inputs = new Dictionary<string, object>(inputs);
        }

        /// <summary>
        /// Searches the output and input dictionaries for the key and returns it.
        /// </summary>
        /// <typeparam name="T">The object type.</typeparam>
        /// <param name="key">The dictionary key.</param>
        public T GetArgument<T>(string key)
        {
            if (Outputs.TryGetValue(key, out var value))
            {
                if (value is T outputValue)
                    return outputValue;
                throw new System.InvalidCastException($"Invalid cast for argument: {key}. Attempted to cast {value.GetType()} to {typeof(T)}.");
            }

            if (Inputs[key] is T inputValue)
                return inputValue;
            throw new System.InvalidCastException($"Invalid cast for argument: {key}. Attempted to cast {Inputs[key].GetType()} to {typeof(T)}.");
        }

        /// <summary>
        /// Returns the input for the specified key.
        /// </summary>
        /// <param name="key">The input key.</param>
        public T GetInput<T>(string key)
        {
            if (Inputs[key] is T value)
                return value;
            throw new System.InvalidCastException($"Invalid cast for argument: {key}. Attempted to cast {Inputs[key].GetType()} to {typeof(T)}.");
        }

        /// <summary>
        /// Returns the output for the specified key.
        /// </summary>
        /// <param name="key">The output key.</param>
        public T GetOutput<T>(string key)
        {
            if (Outputs[key] is T value)
                return value;
            throw new System.InvalidCastException($"Invalid cast for argument: {key}. Attempted to cast {Outputs[key].GetType()} to {typeof(T)}.");
        }

        /// <summary>
        /// Sets the value to the outputs dictionary.
        /// </summary>
        /// <param name="key">The output key.</param>
        /// <param name="value">The output value.</param>
        public void SetOutput<T>(string key, T value)
        {
            Outputs[key] = value;
        }

        /// <summary>
        /// Adds the value to the outputs dictionary. Raises an exception if the key already exists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The output key.</param>
        /// <param name="value">The output value.</param>
        public void AddOutput<T>(string key, T value)
        {
            Outputs.Add(key, value);
        }

        /// <summary>
        /// Marks the pipeline as successful.
        /// </summary>
        public void Complete()
        {
            Success = true;
        }
    }
}
