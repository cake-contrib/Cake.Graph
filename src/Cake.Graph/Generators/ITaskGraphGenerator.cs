using System.Collections.Generic;
using System.Threading.Tasks;
using Cake.Core;

namespace Cake.Graph.Generators
{
    /// <summary>
    /// Interface for generating the file contents for graph files
    /// </summary>
    public interface ITaskGraphGenerator
    {
        /// <summary>
        /// Converts the task to a graph and outputs the string for displaying
        /// </summary>
        /// <param name="context"></param>
        /// <param name="task"></param>
        /// <param name="tasks"></param>
        /// <returns></returns>
        Task<string> SerializeAsync(ICakeContext context, ICakeTaskInfo task, IReadOnlyList<ICakeTaskInfo> tasks);
        /// <summary>
        /// The file extension to use for this type of graph file
        /// </summary>
        string Extension { get; }
    }
}