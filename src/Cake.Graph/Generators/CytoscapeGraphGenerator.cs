using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Diagnostics;
using Newtonsoft.Json;

namespace Cake.Graph.Generators
{
    /// <summary>
    /// Generator which outputs cytoscape syntax
    /// </summary>
    public class CytoscapeGraphGenerator : ITaskGraphGenerator
    {
        /// <summary>
        /// Converts the task to a graph and outputs the string for displaying
        /// </summary>
        /// <param name="context"></param>
        /// <param name="task"></param>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public string Serialize(ICakeContext context, ICakeTaskInfo task, IReadOnlyList<ICakeTaskInfo> tasks)
        {
            TaskGraphGeneratorHelpers.ValidateParameters(context, task, tasks);

            var nodes = GetTaskGraphNodes(context, task, tasks);
            return JsonConvert.SerializeObject(nodes);
        }

        /// <summary>
        /// The file extension to use for this type of graph file
        /// </summary>
        public string Extension => "json";

        private IReadOnlyList<Node> GetTaskGraphNodes(ICakeContext context, ICakeTaskInfo task, IReadOnlyList<ICakeTaskInfo> tasks)
        {
            var taskDictionary = tasks.ToDictionary();

            var nodes = new List<Node>();

            var stack = new Stack<ICakeTaskInfo>();
            stack.Push(task);
            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                context.Log.Write(Verbosity.Diagnostic, LogLevel.Debug, $"Creating Node for {currentNode.Name} which has {currentNode.Dependencies.Count} dependencies");
                nodes.Add(new Node(currentNode.Name));

                foreach (var dependencyName in currentNode.Dependencies)
                {
                    var dependencyTask = TaskGraphGeneratorHelpers.GetTaskDependency(context, taskDictionary, dependencyName.Name);
                    stack.Push(dependencyTask);
                    context.Log.Write(Verbosity.Diagnostic, LogLevel.Debug, $"Creating Edge from {currentNode.Name} to {dependencyTask.Name}");
                    nodes.Add(new Node(Guid.NewGuid().ToString(), currentNode.Name, dependencyTask.Name));
                }
            }

            return nodes;
        }
    }
}
