using Newtonsoft.Json;

namespace Cake.Graph
{
    /// <summary>
    /// Represents a node or edge for in a cytoscape graph
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Set this object as a node
        /// </summary>
        /// <param name="id"></param>
        public Node(string id)
        {
            Data = new NodeData(id);
        }

        /// <summary>
        /// Set this object as an edge connecting the source and target nodes
        /// </summary>
        /// <param name="id"></param>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public Node(string id, string source, string target)
        {
            Data = new NodeData(id, source, target);
        }

        /// <summary>
        /// The node data
        /// </summary>
        [JsonProperty("data")]
        public NodeData Data { get; }

        /// <summary>
        /// Node data class which contains the properties
        /// </summary>
        public class NodeData
        {
            /// <summary>
            /// Sets this object as a node
            /// </summary>
            /// <param name="id"></param>
            public NodeData(string id)
            {
                this.Id = id;
            }

            /// <summary>
            /// Sets this object as an edge
            /// </summary>
            /// <param name="id"></param>
            /// <param name="source"></param>
            /// <param name="target"></param>
            public NodeData(string id, string source, string target)
            {
                Id = id;
                Source = source;
                Target = target;
            }

            /// <summary>
            /// Node/Edge identifier
            /// </summary>
            [JsonProperty("id")]
            public string Id { get; }
            /// <summary>
            /// Source node
            /// </summary>
            [JsonProperty("source")]
            public string Source { get; }
            /// <summary>
            /// Target node
            /// </summary>
            [JsonProperty("target")]
            public string Target { get; }
        }
    }
}
