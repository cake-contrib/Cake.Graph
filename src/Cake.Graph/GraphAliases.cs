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
        [CakeMethodAlias]
        public static GraphRunner Graph(this ICakeContext context, IReadOnlyList<ICakeTaskInfo> tasks) => new GraphRunner(context, tasks);
    }
}