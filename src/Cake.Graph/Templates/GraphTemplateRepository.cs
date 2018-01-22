using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Cake.Graph.Templates
{
    /// <summary>
    /// Retrieves templates from embedded resources
    /// </summary>
    public class GraphTemplateRepository : IGraphTemplateRepository
    {
        private readonly Assembly assembly;

        /// <summary>
        /// GraphTemplateRepository
        /// </summary>
        public GraphTemplateRepository() : this(Assembly.GetExecutingAssembly(), defaultTemplateResourcePaths) {}

        /// <summary>
        /// GraphTemplateRepository
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="templateResourcePaths"></param>
        public GraphTemplateRepository(Assembly assembly, IDictionary<TemplateTypes, string> templateResourcePaths)
        {
            this.assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));
            this.TemplateResourcePaths = templateResourcePaths ??
                                         throw new ArgumentNullException(nameof(templateResourcePaths));
        }

        /// <summary>
        /// Provides the paths to the resources
        /// </summary>
        public IDictionary<TemplateTypes, string> TemplateResourcePaths { get; set; }

        private static IDictionary<TemplateTypes, string> defaultTemplateResourcePaths { get; } = new Dictionary<TemplateTypes, string>
        {
            { TemplateTypes.Cytoscape, "Cake.Graph.Content.cytoscape.html" },
            { TemplateTypes.Mermaid, "Cake.Graph.Content.mermaid.html" }
        };

        /// <summary>
        /// Reads the resource to a string if it exists
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public string ReadResourceToString(string resource)
        {
            using (var readStream = assembly.GetManifestResourceStream(resource))
            using (var reader = new StreamReader(readStream ?? throw new ArgumentNullException(nameof(readStream))))
                return reader.ReadToEnd();
        }
    }
}