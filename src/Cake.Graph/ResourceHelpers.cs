using System;
using System.IO;
using System.Reflection;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace Cake.Graph
{
    /// <summary>
    /// Functions for reading embedded resources
    /// </summary>
    public static class ResourceHelpers
    {
        private static readonly ITemplateServiceConfiguration razorTemplateServiceConfig = new TemplateServiceConfiguration
        {
            DisableTempFileLocking = true, // loads the files in-memory (gives the templates full-trust permissions)
            CachingProvider = new DefaultCachingProvider(t => { }) //disables the warnings
        };
        static ResourceHelpers()
        {
            Engine.Razor = RazorEngineService.Create(razorTemplateServiceConfig);
        }

        /// <summary>
        /// Read the specified resource into a string
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public static string ReadResourceToString(string resource)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var readStream = assembly.GetManifestResourceStream(resource))
            using (var reader = new StreamReader(readStream ?? throw new ArgumentNullException(nameof(readStream))))
                return reader.ReadToEnd();
        }

        /// <summary>
        /// Parse the given razorTemplate with the given model and returns the output as a string
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="razorTemplate"></param>
        /// <param name="model"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ParseRazorTemplate<T>(string templateName, string razorTemplate, T model)
        {
            var htmlFileOutput = Engine.Razor.RunCompile(razorTemplate, templateName, typeof(T), model);
            return htmlFileOutput;
        }

        /// <summary>
        /// Reads an embedded resource and parses it as a razor template with the given model
        /// </summary>
        /// <param name="resourcePath"></param>
        /// <param name="model"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string ParseRazorTemplateFromResource<T>(string resourcePath, T model)
        {
            var razorTemplate = ReadResourceToString(resourcePath);
            var htmlFileOutput = ParseRazorTemplate(resourcePath, razorTemplate, model);
            return htmlFileOutput;
        }
    }
}