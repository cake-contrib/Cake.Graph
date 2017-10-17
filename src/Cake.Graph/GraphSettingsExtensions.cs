using System;
using Cake.Graph.Generators;

namespace Cake.Graph
{
    /// <summary>
    /// Graph settings extensions
    /// </summary>
    public static class GraphSettingsExtensions
    {
        /// <summary>
        /// Set whether to deploy an html page or a wyam ready cshtml page
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="useWyam"></param>
        /// <returns></returns>
        public static GraphSettings UseWyam(this GraphSettings settings, bool useWyam = true)
        {
            settings.IsWyam = useWyam;
            return settings;
        }
        /// <summary>
        /// Root path for deploying all other files
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="outputPath"></param>
        /// <returns></returns>
        public static GraphSettings SetOutputPath(this GraphSettings settings, string outputPath)
        {
            settings.OutputPath = outputPath;
            return settings;
        }
        /// <summary>
        /// Set the directory to put the .json files which describe each task. OutputPath will be prepended
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="nodeSetsPath"></param>
        /// <returns></returns>
        public static GraphSettings SetNodeSetsPath(this GraphSettings settings, string nodeSetsPath)
        {
            settings.NodeSetsPath = nodeSetsPath;
            return settings;
        }
        /// <summary>
        /// Set the name of the html/cshtml file when deployed
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="mainPageName"></param>
        /// <returns></returns>
        public static GraphSettings SetMainPageName(this GraphSettings settings, string mainPageName)
        {
            settings.MainPageName = mainPageName;
            return settings;
        }
        /// <summary>
        /// Set the file name that the css file will be deployed as. Prepended by OutputPath
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="cssPath"></param>
        /// <returns></returns>
        public static GraphSettings SetCssPath(this GraphSettings settings, string cssPath)
        {
            settings.CssPath = cssPath;
            return settings;
        }
        /// <summary>
        /// Set the file name that the cytoscape javascript file will be deployed as. Prepended by OutputPath
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="cytoscapeJsPath"></param>
        /// <returns></returns>
        public static GraphSettings SetCytoscapeJsPath(this GraphSettings settings, string cytoscapeJsPath)
        {
            settings.CytoscapeJsPath = cytoscapeJsPath;
            return settings;
        }
        /// <summary>
        /// Set the file name that the graph js file will be deployed as. Prepended by OutputPath
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="jsPath"></param>
        /// <returns></returns>
        public static GraphSettings SetJsPath(this GraphSettings settings, string jsPath)
        {
            settings.JsPath = jsPath;
            return settings;
        }
        /// <summary>
        /// Set the file name that the jquery file will be deployed as. Prepended by OutputPath.
        /// Not used with Wyam deployment
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="jQueryPath"></param>
        /// <returns></returns>
        public static GraphSettings SetJQueryPath(this GraphSettings settings, string jQueryPath)
        {
            settings.JQueryPath = jQueryPath;
            return settings;
        }
        /// <summary>
        /// Set the file name that the json file which lists all the tasks be deployed as. Prepended by OutputPath
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="taskListFileName"></param>
        /// <returns></returns>
        public static GraphSettings SetTaskListFileName(this GraphSettings settings, string taskListFileName)
        {
            settings.TaskListFileName = taskListFileName;
            return settings;
        }

        /// <summary>
        /// Use a generator which outputs in mermaid syntax
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static GraphSettings WithMermaidGenerator(this GraphSettings settings)
        {
            settings.Generator = new MermaidGraphGenerator();
            return settings;
        }

        /// <summary>
        /// Use a generator which outputs in cytoscape syntax
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static GraphSettings WithCytoscapeGenerator(this GraphSettings settings)
        {
            settings.Generator = new CytoscapeGraphGenerator();
            return settings;
        }

        /// <summary>
        /// Use a custom generator
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="generator"></param>
        /// <returns></returns>
        public static GraphSettings WithCustomGenerator(this GraphSettings settings, ITaskGraphGenerator generator)
        {
            settings.Generator = generator;
            return settings;
        }
    }
}