using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cake.Core;
using Cake.Core.Diagnostics;

namespace Cake.Graph.Generators
{
    /// <summary>
    /// Generate graph outputs in mermaid syntax
    /// </summary>
    public class MermaidGraphGenerator : ITaskGraphGenerator
    {
        /// <summary>
        /// Extension to use for the files by default
        /// </summary>
        public string Extension => "md";

        /// <summary>
        /// Convert the task and dependencies to mermaid syntax string
        /// </summary>
        /// <param name="context"></param>
        /// <param name="task"></param>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public string Serialize(ICakeContext context, ICakeTaskInfo task, IReadOnlyList<ICakeTaskInfo> tasks)
        {
            TaskGraphGeneratorHelpers.ValidateParameters(context, task, tasks);

            var edges = GetEdges(context, task, tasks);
            return ConvertToString(task, edges);
        }

        private static string ConvertToString(ICakeTaskInfo task, IReadOnlyCollection<string> edges)
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(task.Description))
                sb.Append($"<p>{task.Description}</p>");
            sb.AppendLine("<div class=\"mermaid\">");
            sb.AppendLine("graph TD;");

            if (edges.Count == 0)
                sb.AppendLine($"{task.Name};");

            foreach (var edge in edges)
                sb.AppendLine(edge);

            sb.AppendLine("</div>");

            return sb.ToString();
        }

        private static IReadOnlyCollection<string> GetEdges(ICakeContext context, ICakeTaskInfo task, IReadOnlyList<ICakeTaskInfo> tasks)
        {
            var taskDictionary = tasks.ToDictionary();

            var stack = new Stack<ICakeTaskInfo>();
            stack.Push(task);
            var edges = new List<string>();
            var visited = new HashSet<string>();
            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                context.Log.Write(Verbosity.Diagnostic, LogLevel.Debug,
                    $"Creating Node for {currentNode.Name} which has {currentNode.Dependencies.Count} dependencies");

                foreach (var dependencyName in currentNode.Dependencies)
                {
                    var dependencyTask = TaskGraphGeneratorHelpers.GetTaskDependency(context, taskDictionary, dependencyName.Name);

                    if (visited.Contains(dependencyTask.Name, StringComparer.InvariantCultureIgnoreCase))
                        continue;

                    visited.Add(dependencyTask.Name);
                    stack.Push(dependencyTask);

                    context.Log.Write(Verbosity.Diagnostic, LogLevel.Debug,
                        $"Creating Edge from {currentNode.Name} to {dependencyTask.Name}");
                    var edge = $"{currentNode.Name}-->{dependencyTask.Name};";
                    edges.Add(edge);
                }
            }

            return edges;
        }
    }
}