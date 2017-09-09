using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Cake.Core;
using Cake.Core.Diagnostics;
using Newtonsoft.Json;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
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
        private readonly IReadOnlyList<CakeTask> tasks;
        private readonly ITemplateServiceConfiguration razorTemplateServiceConfig = new TemplateServiceConfiguration
        {
            DisableTempFileLocking = true, // loads the files in-memory (gives the templates full-trust permissions)
            CachingProvider = new DefaultCachingProvider(t => { }) //disables the warnings
        };

        /// <summary>
        /// Runs the commands to generate the graph node sets and deploy the html files for displaying graphs
        /// </summary>
        public GraphRunner(ICakeContext context, IReadOnlyList<CakeTask> tasks)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.tasks = tasks ?? throw new ArgumentNullException(nameof(tasks));
        }

        /// <summary>
        /// Generate the node set files and deploy the web content files
        /// </summary>
        /// <param name="configure"></param>
        public GraphRunner Deploy(Action<GraphSettings> configure = null)
        {
            var settings = new GraphSettings();
            configure?.Invoke(settings);

            return Deploy(settings);
        }

        /// <summary>
        /// Generate the node set files and deploy the web content files
        /// </summary>
        /// <param name="settings"></param>
        public GraphRunner Deploy(GraphSettings settings)
        {
            GenerateNodeSets(settings);
            DeployWebFiles(settings);

            return this;
        }

        /// <summary>
        /// Create the node set files that describe each task and it's dependencies
        /// </summary>
        /// <param name="configure"></param>
        public GraphRunner GenerateNodeSets(Action<GraphSettings> configure = null)
        {
            var settings = new GraphSettings();
            configure?.Invoke(settings);

            return GenerateNodeSets(settings);
        }

        /// <summary>
        /// Create the node set files that describe each task and it's dependencies
        /// </summary>
        /// <param name="settings"></param>
        public GraphRunner GenerateNodeSets(GraphSettings settings)
        {
            context.Log.Write(Verbosity.Normal, LogLevel.Information, "Writing graph node set files");
            var nodeSetsFolder = Path.Combine(settings.OutputPath, settings.NodeSetsPath);
            context.Log.Write(Verbosity.Diagnostic, LogLevel.Information, $"Ensuring node sets directory at {nodeSetsFolder}");
            if (!string.IsNullOrWhiteSpace(nodeSetsFolder))
                Directory.CreateDirectory(nodeSetsFolder);

            foreach (var task in tasks)
                CreateNodeSetFile(task, settings);

            CreateTaskListFile(settings);

            return this;
        }

        /// <summary>
        /// Deploy the css, js, and html files needed to be able to view the graphs for the tasks
        /// </summary>
        /// <param name="configure"></param>
        public GraphRunner DeployWebFiles(Action<GraphSettings> configure = null)
        {
            var settings = new GraphSettings();
            configure?.Invoke(settings);

            return DeployWebFiles(settings);
        }

        /// <summary>
        /// Deploy the css, js, and html files needed to be able to view the graphs for the tasks
        /// </summary>
        /// <param name="settings"></param>
        public GraphRunner DeployWebFiles(GraphSettings settings)
        {
            context.Log.Write(Verbosity.Normal, LogLevel.Information, "Deploying graph web files");

            var jQueryFilePath = Path.Combine(settings.OutputPath, settings.JQueryPath);
            var jsFilePath = Path.Combine(settings.OutputPath, settings.JsPath);
            var cytoscapeJsFilePath = Path.Combine(settings.OutputPath, settings.CytoscapeJsPath);
            var cssFilePath = Path.Combine(settings.OutputPath, settings.CssPath);
            var htmlOutputPath = Path.Combine(settings.OutputPath, $"{settings.MainPageName}.{(settings.IsWyam ? "cshtml" : "html")}");

            var htmlResource = $"Cake.Graph.Content.{(settings.IsWyam ? "Wyam." : "")}tasks.cshtml";

            if (!settings.IsWyam)
                WriteResourceToFile(GraphConstants.JQueryResource, jQueryFilePath);
            WriteResourceToFile(GraphConstants.CytoscapeJsResource, cytoscapeJsFilePath);
            WriteResourceToFile(GraphConstants.CssResource, cssFilePath);

            ParseRazorAndWriteToFile(settings, GraphConstants.JsResource, jsFilePath);
            ParseRazorAndWriteToFile(settings, htmlResource, htmlOutputPath);

            return this;
        }

        private void ParseRazorAndWriteToFile(GraphSettings settings, string razorResource, string outputPath)
        {
            var razorTemplate = ReadResourceToString(razorResource);
            Engine.Razor = RazorEngineService.Create(razorTemplateServiceConfig);
            var htmlFileOutput = Engine.Razor.RunCompile(razorTemplate, razorResource, typeof(GraphSettings), settings);
            // Clean file
            File.Delete(outputPath);
            File.WriteAllText(outputPath, htmlFileOutput);
        }

        private void WriteResourceToFile(string resource, string filePath)
        {
            context.Log.Write(Verbosity.Diagnostic, LogLevel.Information, $"Writing {resource} to {filePath}");
            Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? throw new ArgumentNullException(nameof(filePath)));
            var assembly = Assembly.GetExecutingAssembly();

            // Clean file
            File.Delete(filePath);
            using (var readStream = assembly.GetManifestResourceStream(resource))
            using (var writeStream = File.OpenWrite(filePath))
            {
                if (readStream == null)
                    throw new ArgumentNullException();
                readStream.CopyTo(writeStream);
            }
        }

        private string ReadResourceToString(string resource)
        {
            context.Log.Write(Verbosity.Diagnostic, LogLevel.Information, $"Reading {resource}");
            var assembly = Assembly.GetExecutingAssembly();
            using (var readStream = assembly.GetManifestResourceStream(resource))
            using (var reader = new StreamReader(readStream ?? throw new ArgumentNullException(nameof(readStream))))
                return reader.ReadToEnd();
        }

        private void CreateNodeSetFile(CakeTask task, GraphSettings settings)
        {
            context.Log.Write(Verbosity.Normal, LogLevel.Information, $"Creating node set for {task.Name} dependencies");
            var nodes = GetTaskGraphNodes(task);

            foreach (var node in nodes)
                context.Log.Debug(Verbosity.Diagnostic, $"Node: {node.Data.Id}, Source: {node.Data.Source}, Target: {node.Data.Target}");

            var filePath = Path.Combine(settings.OutputPath, settings.NodeSetsPath, $"{task.Name}.json");
            context.Log.Write(Verbosity.Diagnostic, LogLevel.Information, $"Writing node set file for {task.Name} dependencies to {filePath}");

            using (var file = File.CreateText(filePath))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, nodes);
            }
        }

        private void CreateTaskListFile(GraphSettings settings)
        {
            context.Log.Write(Verbosity.Normal, LogLevel.Information, "Writing task list file");
            var filePath = Path.Combine(settings.OutputPath, settings.NodeSetsPath, settings.TaskListFileName);
            using (var file = File.CreateText(filePath))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, tasks.Select(x => x.Name));
            }
        }

        private IReadOnlyList<Node> GetTaskGraphNodes(CakeTask task)
        {
            var nodes = new List<Node>();

            var stack = new Stack<CakeTask>();
            stack.Push(task);
            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                context.Log.Write(Verbosity.Diagnostic, LogLevel.Debug, $"Creating Node for {currentNode.Name} which has {currentNode.Dependencies.Count} dependencies");
                nodes.Add(new Node(currentNode.Name));

                foreach (var dependencyName in currentNode.Dependencies)
                {
                    var dependencyTask = GetTask(dependencyName);
                    context.Log.Write(Verbosity.Diagnostic, LogLevel.Debug, $"Found dependent task {dependencyTask.Name} with {dependencyTask.Dependencies.Count} dependencies");
                    stack.Push(dependencyTask);

                    context.Log.Write(Verbosity.Diagnostic, LogLevel.Debug, $"Creating Edge from {currentNode.Name} to {dependencyTask.Name}");
                    nodes.Add(new Node(Guid.NewGuid().ToString(), currentNode.Name, dependencyTask.Name));
                }
            }

            return nodes;
        }

        private CakeTask GetTask(string taskName)
        {
            var task = tasks.FirstOrDefault(x => string.Equals(x.Name, taskName, StringComparison.InvariantCultureIgnoreCase));
            if (task == null)
                throw new ArgumentException($"Unknown task: {taskName}");

            return task;
        }
    }
}