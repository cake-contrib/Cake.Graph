using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;

namespace Cake.Graph.Generators
{
    /// <summary>
    /// CakeTask Extension methods
    /// </summary>
    public static class CakeTaskExtensions
    {
        /// <summary>
        /// Convert to a dictionary keyed on task name
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public static IDictionary<string, ICakeTaskInfo> ToDictionary(this IReadOnlyList<ICakeTaskInfo> tasks)
        {
            return tasks?.ToDictionary(x => x.Name, x => x, StringComparer.InvariantCultureIgnoreCase)
                   ?? throw new ArgumentNullException(nameof(tasks));
        }
    }
}