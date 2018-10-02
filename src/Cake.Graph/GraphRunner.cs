using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.Diagnostics;
using LogLevel = Cake.Core.Diagnostics.LogLevel;
using Verbosity = Cake.Core.Diagnostics.Verbosity;

namespace Cake.Graph
{
    /// <summary>
    /// Runs the commands to generate the graph node sets and deploy the html files for displaying graphs
    /// </summary>
    public class GraphRunner
    {
        private readonly ICakeContext context;
        private readonly IReadOnlyList<ICakeTaskInfo> tasks;

        /// <summary>
        /// Runs the commands to generate the graph node sets and deploy the html files for displaying graphs
        /// </summary>
        public GraphRunner(ICakeContext context, IReadOnlyList<ICakeTaskInfo> tasks)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.tasks = tasks ?? throw new ArgumentNullException(nameof(tasks));
        }

        /// <summary>
        /// Generate the node set files and deploy the web content files
        /// </summary>
        /// <param name="configure"></param>
        public async Task<GraphRunner> DeployAsync(Action<GraphSettings> configure = null)
        {
            var settings = new GraphSettings();
            configure?.Invoke(settings);

            return await DeployAsync(settings);
        }

        /// <summary>
        /// Generate the node set files and deploy the web content files
        /// </summary>
        /// <param name="configure"></param>
        public GraphRunner Deploy(Action<GraphSettings> configure = null) =>
            DeployAsync(configure).GetAwaiter().GetResult();
        
        /// <summary>
        /// Generate the node set files and deploy the web content files
        /// </summary>
        /// <param name="settings"></param>
        public async Task<GraphRunner> DeployAsync(GraphSettings settings)
        {
            if (settings.Generator == null)
            {
                context.Log.Information("No generator set. Using mermaid generator");
                settings.WithMermaidGenerator();
            }

            context.Log.Write(Verbosity.Normal, LogLevel.Information, "Writing files");
            context.Log.Write(Verbosity.Diagnostic, LogLevel.Information, $"Ensuring node sets directory at {settings.OutputPath}");
            
            if (!string.IsNullOrWhiteSpace(settings.OutputPath))
                Directory.CreateDirectory(settings.OutputPath);
            
            foreach (var task in tasks)
            {
                var value = await settings.Generator.SerializeAsync(context, task, tasks);
                
                var filePath = Path.Combine(settings.OutputPath, $"{task.Name}.{settings.Generator.Extension}");
                using (var file = File.CreateText(filePath))
                    file.Write(value);
            }

            return this;
        }

        /// <summary>
        /// Generate the node set files and deploy the web content files
        /// </summary>
        /// <param name="settings"></param>
        public GraphRunner Deploy(GraphSettings settings) => DeployAsync(settings).GetAwaiter().GetResult();
    }
}