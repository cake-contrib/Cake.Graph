using Cake.Graph.Templates;

namespace Cake.Graph.Generators
{
    /// <inheritdoc />
    public class CytoscapeWyamGenerator : CytoscapeHtmlGenerator
    {
        /// <inheritdoc />
        public CytoscapeWyamGenerator(IGraphTemplateManager graphTemplateManager) : base(graphTemplateManager)
        {
            templateType = TemplateTypes.CytoscapeWyam;
            extension = "cshtml";
        }

        /// <inheritdoc />
        public CytoscapeWyamGenerator() : this(new GraphTemplateManager()) { }
    }
}