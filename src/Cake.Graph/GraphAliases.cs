using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Graph
{
    /// <summary>
    /// Aliases for retrieving the GraphRunner
    /// </summary>
    public static class GraphAliases
    {
        /// <summary>
        /// Alias for retrieving the GraphRunner
        /// </summary>
        /// <param name="context"></param>
        /// <param name="tasks"></param>
        /// <returns></returns>
        /// <example>
        /// <para>Deploy both web files and task nodeset files</para>
        /// <code>
        /// <![CDATA[
        /// Task("Graph")
        ///     .Does(() =>
        /// {
        ///     Graph(Tasks).Deploy(s => s.SetOutputPath("output"));
        /// });
        /// ]]>
        /// </code>
        /// </example>
        /// <example>
        /// <para>Generate the nodesets files for use with deployed web files</para>
        /// <code>
        /// <![CDATA[
        /// Task("Graph")
        ///     .Does(() =>
        /// {
        ///     Graph(Tasks).GenerateNodeSets(s => s.SetOutputPath("output"));
        /// });
        /// ]]>
        /// </code>
        /// </example>
        /// <example>
        /// <para>Deploy the web files needed for Cake.Graph to display the graphs</para>
        /// <code>
        /// <![CDATA[
        /// Task("Graph")
        ///     .Does(() =>
        /// {
        ///     Graph(Tasks).DeployWebFiles(s => s.SetOutputPath("output"));
        /// });
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        public static GraphRunner Graph(this ICakeContext context, IReadOnlyList<CakeTask> tasks) => new GraphRunner(context, tasks);
    }
}