namespace Cake.Graph.Models
{
    /// <summary>
    /// Model for use in generating HTML pages
    /// </summary>
    public class GraphHtmlModel
    {
        /// <summary>
        /// Constructor for GraphHtmlModel
        /// </summary>
        /// <param name="title"></param>
        /// <param name="jsSource"></param>
        /// <param name="graph"></param>
        public GraphHtmlModel(string title, string jsSource, string graph)
        {
            Title = title;
            JsSource = jsSource;
            Graph = graph;
        }

        /// <summary>
        /// CDN Url for mermaid.js
        /// </summary>
        public string JsSource { get; }
        /// <summary>
        /// Mermaid graph div html
        /// </summary>
        public string Graph { get; }

        /// <summary>
        /// Title for the graph
        /// </summary>
        public string Title { get; set; }
    }
}