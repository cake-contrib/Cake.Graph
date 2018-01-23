using Cake.Graph.Generators;

namespace Cake.Graph
{
    /// <summary>
    /// Graph settings extensions
    /// </summary>
    public static class GraphSettingsExtensions
    {
        /// <summary>
        /// Root path for deploying all files
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="outputPath"></param>
        /// <returns></returns>
        public static GraphSettings SetOutputPath(this GraphSettings settings, string outputPath)
        {
            settings.OutputPath = outputPath;
            return settings;
        }

        /// <summary>
        /// Use a generator which outputs in mermaid syntax
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static GraphSettings WithMermaidGenerator(this GraphSettings settings)
        {
            settings.Generator = new MermaidGraphGenerator();
            return settings;
        }

        /// <summary>
        /// Use a generator which outputs separate files with mermaid graphs
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static GraphSettings WithMermaidHtmlGenerator(this GraphSettings settings)
        {
            settings.Generator = new MermaidHtmlGenerator();
            return settings;
        }

        /// <summary>
        /// Use a generator which outputs in cytoscape syntax
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static GraphSettings WithCytoscapeGenerator(this GraphSettings settings)
        {
            settings.Generator = new CytoscapeGraphGenerator();
            return settings;
        }

        /// <summary>
        /// Use a generator which outputs separate files with mermaid graphs
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static GraphSettings WithCytoscapeHtmlGenerator(this GraphSettings settings)
        {
            settings.Generator = new CytoscapeHtmlGenerator();
            return settings;
        }

        /// <summary>
        /// Use a custom generator
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="generator"></param>
        /// <returns></returns>
        public static GraphSettings WithCustomGenerator(this GraphSettings settings, ITaskGraphGenerator generator)
        {
            settings.Generator = generator;
            return settings;
        }
    }
}