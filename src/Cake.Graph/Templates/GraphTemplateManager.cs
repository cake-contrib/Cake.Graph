using System;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace Cake.Graph.Templates
{
    /// <summary>
    /// Functions for reading embedded resources
    /// </summary>
    public class GraphTemplateManager : IGraphTemplateManager
    {
        private static readonly ITemplateServiceConfiguration razorTemplateServiceConfig = new TemplateServiceConfiguration
        {
            DisableTempFileLocking = true, // loads the files in-memory (gives the templates full-trust permissions)
            CachingProvider = new DefaultCachingProvider(t => { }) //disables the warnings
        };

        static GraphTemplateManager()
        {
            Engine.Razor = RazorEngineService.Create(razorTemplateServiceConfig);
        }

        private readonly IGraphTemplateRepository graphTemplateRepository;

        /// <summary>
        /// GraphTemplateManager
        /// </summary>
        public GraphTemplateManager() : this(new GraphTemplateRepository()) {}

        /// <summary>
        /// GraphTemplateManager
        /// </summary>
        /// <param name="graphTemplateRepository"></param>
        public GraphTemplateManager(IGraphTemplateRepository graphTemplateRepository)
        {
            this.graphTemplateRepository = graphTemplateRepository ?? throw new ArgumentNullException(nameof(graphTemplateRepository));
        }

        /// <summary>
        /// Parses the specified razor template and returns the result as a string
        /// </summary>
        /// <param name="templateTypeKey"></param>
        /// <param name="model"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public string ParseTemplate<T>(TemplateTypes templateTypeKey, T model)
        {
            if (!graphTemplateRepository.TemplateResourcePaths.TryGetValue(templateTypeKey, out var templateResourcePath))
                throw new ArgumentOutOfRangeException(nameof(templateTypeKey));

            var result = ParseRazorTemplateFromResource(templateResourcePath, model);
            return result;
        }

        private string ParseRazorTemplateFromResource<T>(string resourcePath, T model)
        {
            var cached = Engine.Razor.IsTemplateCached(resourcePath, typeof(T));
            if (cached)
                return ParseCachedRazorTemplate(resourcePath, model);

            var razorTemplate = graphTemplateRepository.ReadResourceToString(resourcePath);
            var htmlFileOutput = ParseAndCacheRazorTemplate(resourcePath, razorTemplate, model);
            return htmlFileOutput;
        }

        private string ParseAndCacheRazorTemplate<T>(string templateName, string razorTemplate, T model)
        {
            var htmlFileOutput = Engine.Razor.RunCompile(razorTemplate, templateName, typeof(T), model);
            return htmlFileOutput;
        }

        private string ParseCachedRazorTemplate<T>(string resourcePath, T model)
        {
            return Engine.Razor.Run(resourcePath, typeof(T), model);
        }
    }
}