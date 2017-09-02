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
        /// <para>Get GraphRunner for running further commands</para>
        /// <code>
        /// <![CDATA[
        /// Task("Graph")
        ///     .Does(() =>
        /// {
        ///     Graph(Tasks).Deploy(s => s.OutputPath = "/output");
        /// });
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        public static GraphRunner Graph(this ICakeContext context, IReadOnlyList<CakeTask> tasks) => new GraphRunner(context, tasks);
    }
}