using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Diagnostics;

namespace Cake.Graph.Generators
{
    /// <summary>
    /// Helper methods for use in generators
    /// </summary>
    public static class TaskGraphGeneratorHelpers
    {
        /// <summary>
        /// Looks up the dependency in the dictionary and throws an exception if unable to find it
        /// </summary>
        /// <param name="context"></param>
        /// <param name="taskDictionary"></param>
        /// <param name="dependencyName"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public static ICakeTaskInfo GetTaskDependency(ICakeContext context, IDictionary<string, ICakeTaskInfo> taskDictionary, string dependencyName)
        {
            if (!taskDictionary.TryGetValue(dependencyName, out var dependencyTask))
                throw new KeyNotFoundException($"Unable to find '{dependencyName}' dependency '{dependencyName}' in tasks collection");

            context.Log.Write(Verbosity.Diagnostic, LogLevel.Debug, $"Found dependent task {dependencyTask.Name} with {dependencyTask.Dependencies.Count} dependencies");
            return dependencyTask;
        }

        /// <summary>
        /// Ensure parameters are valid
        /// </summary>
        /// <param name="context"></param>
        /// <param name="task"></param>
        /// <param name="tasks"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ValidateParameters(ICakeContext context, ICakeTaskInfo task, IReadOnlyList<ICakeTaskInfo> tasks)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (task == null)
                throw new ArgumentNullException(nameof(task));
            if (tasks == null)
                throw new ArgumentNullException(nameof(tasks));
        }
    }
}