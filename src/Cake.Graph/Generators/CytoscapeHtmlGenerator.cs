using System.Collections.Generic;
using Cake.Core;
using Cake.Graph.Models;

namespace Cake.Graph.Generators
{
    /// <summary>
    /// Generate html for cyctoscape graphs of tasks
    /// </summary>
    public class CytoscapeHtmlGenerator : ITaskGraphGenerator
    {
        private const string razorTemplatePath = "Cake.Graph.Content.cytoscape.cshtml";
        private readonly CytoscapeGraphGenerator graphGenerator = new CytoscapeGraphGenerator();

        /// <summary>
        /// Url/path for the cytoscape js file
        /// </summary>
        public string CytoscapeJsSource => "https://cdnjs.cloudflare.com/ajax/libs/cytoscape/3.2.8/cytoscape.min.js";

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
            var model = new GraphHtmlModel(task.Name, CytoscapeJsSource, graph);
            var html = ResourceHelpers.ParseRazorTemplateFromResource(razorTemplatePath, model);
            return html;
        }
    }
}