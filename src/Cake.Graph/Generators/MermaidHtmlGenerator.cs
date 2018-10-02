using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Graph.Models;
using Cake.Graph.Templates;

namespace Cake.Graph.Generators
{
    /// <summary>
    /// Generate html for mermaid graphs of tasks
    /// </summary>
    public class MermaidHtmlGenerator : ITaskGraphGenerator
    {
        /// <summary>
        /// MermaidHtmlGenerator
        /// </summary>
        /// <param name="graphTemplateManager"></param>
        public MermaidHtmlGenerator(IGraphTemplateManager graphTemplateManager)
        {
            this.graphTemplateManager = graphTemplateManager ??
                                        throw new ArgumentNullException(nameof(graphTemplateManager));
        }

        /// <summary>
        /// MermaidHtmlGenerator
        /// </summary>
        public MermaidHtmlGenerator() : this(new GraphTemplateManager()){}

        private readonly MermaidGraphGenerator graphGenerator = new MermaidGraphGenerator();
        private readonly IGraphTemplateManager graphTemplateManager;
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
        public async Task<string> SerializeAsync(ICakeContext context, ICakeTaskInfo task, IReadOnlyList<ICakeTaskInfo> tasks)
        {
            var graph = await graphGenerator.SerializeAsync(context, task, tasks);
            var model = new GraphHtmlModel(task.Name, MermaidJsSource, graph);
            var html = await graphTemplateManager.ParseTemplateAsync(TemplateTypes.Mermaid, model);
            return html;
        }
    }
}