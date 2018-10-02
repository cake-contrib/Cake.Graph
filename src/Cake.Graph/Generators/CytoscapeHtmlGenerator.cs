using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Graph.Models;
using Cake.Graph.Templates;

namespace Cake.Graph.Generators
{
    /// <summary>
    /// Generate html for cyctoscape graphs of tasks
    /// </summary>
    public class CytoscapeHtmlGenerator : ITaskGraphGenerator
    {
        /// <summary>
        /// CytoscapeHtmlGenerator
        /// </summary>
        /// <param name="graphTemplateManager"></param>
        public CytoscapeHtmlGenerator(IGraphTemplateManager graphTemplateManager)
        {
            this.graphTemplateManager = graphTemplateManager ??
                                        throw new ArgumentNullException(nameof(graphTemplateManager));
        }

        /// <summary>
        /// CytoscapeHtmlGenerator
        /// </summary>
        public CytoscapeHtmlGenerator() : this(new GraphTemplateManager()) { }

        private readonly CytoscapeGraphGenerator graphGenerator = new CytoscapeGraphGenerator();
        private readonly IGraphTemplateManager graphTemplateManager;

        /// <summary>
        /// Url/path for the cytoscape js file
        /// </summary>
        public string CytoscapeJsSource => "https://cdnjs.cloudflare.com/ajax/libs/cytoscape/3.2.8/cytoscape.min.js";

        /// <summary>
        /// 
        /// </summary>
        protected string extension = "html";

        /// <inheritdoc />
        public string Extension => extension;

        /// <summary>
        /// Which template to use
        /// </summary>
        protected TemplateTypes templateType = TemplateTypes.Cytoscape;

        /// <inheritdoc />
        public async Task<string> SerializeAsync(ICakeContext context, ICakeTaskInfo task, IReadOnlyList<ICakeTaskInfo> tasks)
        {
            var graph = await graphGenerator.SerializeAsync(context, task, tasks);
            var model = new GraphHtmlModel(task.Name, CytoscapeJsSource, graph);
            var html = await graphTemplateManager.ParseTemplateAsync(templateType, model);
            return html;
        }
    }
}