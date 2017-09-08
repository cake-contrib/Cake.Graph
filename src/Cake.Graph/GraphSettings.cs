namespace Cake.Graph
{
    /// <summary>
    /// Settings for creating the graph files
    /// </summary>
    public class GraphSettings
    {
        /// <summary>
        /// Skips deployment of jQuery and deploys a cshtml file instead of html file
        /// </summary>
        public bool IsWyam { get; set; }
        /// <summary>
        /// Root folder to deploy content to
        /// </summary>
        public string OutputPath { get; set; } = "docs/input";
        /// <summary>
        /// Path to deploy the node set files to
        /// </summary>
        public string NodeSetsPath { get; set; } = "tasks";
        /// <summary>
        /// Name of the html/cshtml file that will be used to display the graphs
        /// </summary>
        public string MainPageName { get; set; } = "graph";
        /// <summary>
        /// Path to the css file that styles the graph container
        /// </summary>
        public string CssPath { get; set; } = "assets/css/tasks.css";
        /// <summary>
        /// Path to the cytoscape js file
        /// </summary>
        public string CytoscapeJsPath { get; set; } = "assets/js/cytoscape.js";
        /// <summary>
        /// Path to the js file which loads the node sets and renders the graph
        /// </summary>
        public string JsPath { get; set; } = "assets/js/task-graph.js";
        /// <summary>
        /// Path to the jquery file (ignored when using wyam)
        /// </summary>
        public string JQueryPath { get; set; } = "assets/js/jquery-3.1.1.min.js";
        /// <summary>
        /// Name of the file to store the list of all the tasks in
        /// </summary>
        public string TaskListFileName { get; set; } = "tasklist.json";
    }
}