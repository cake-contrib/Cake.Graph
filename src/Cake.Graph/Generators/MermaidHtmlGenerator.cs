using System.Collections.Generic;
using Cake.Core;
using Cake.Graph.Models;

namespace Cake.Graph.Generators
{
    /// <summary>
    /// Generate html for mermaid graphs of tasks
    /// </summary>
    public class MermaidHtmlGenerator : ITaskGraphGenerator
    {
        private const string razorTemplatePath = "Cake.Graph.Content.mermaid.cshtml";
        private readonly MermaidGraphGenerator graphGenerator = new MermaidGraphGenerator();

        /// <summary>
        /// Url/path for the mermaid js file
        /// </summary>
        public string MermaidJsSource => "https://unpkg.com/mermaid@7.1.2/dist/mermaid.min.js";

        /// <summary>
        /// The file extension to use for this type of graph file
        /// </summary>
        public string Extension => "html";

        /// <summary>
        /// Converts the task to a graph and outputs the string for displaying
        /// </summary>
        /// <param name="context"></param>
        /// <param name="task"></param>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public string Serialize(ICakeContext context, ICakeTaskInfo task, IReadOnlyList<ICakeTaskInfo> tasks)
        {
            var graph = graphGenerator.Serialize(context, task, tasks);
            var model = new GraphHtmlModel(task.Name, MermaidJsSource, graph);
            var html = ResourceHelpers.ParseRazorTemplateFromResource(razorTemplatePath, model);
            return html;
        }
    }
}