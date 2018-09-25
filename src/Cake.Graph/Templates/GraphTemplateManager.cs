using System;
using System.Threading.Tasks;
using RazorLight;

namespace Cake.Graph.Templates
{
    /// <summary>
    /// Functions for reading embedded resources
    /// </summary>
    public class GraphTemplateManager : IGraphTemplateManager
    {
        private RazorLightEngine Engine { get; }
        
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

            Engine = new RazorLightEngineBuilder()
                .UseMemoryCachingProvider()
                .Build();
        }

        /// <summary>
        /// Parses the specified razor template and returns the result as a string
        /// </summary>
        /// <param name="templateTypeKey"></param>
        /// <param name="model"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task<string> ParseTemplateAsync<T>(TemplateTypes templateTypeKey, T model)
        {
            if (!graphTemplateRepository.TemplateResourcePaths.TryGetValue(templateTypeKey, out var templateResourcePath))
                throw new ArgumentOutOfRangeException(nameof(templateTypeKey));

            var result = await ParseRazorTemplateFromResourceAsync(templateResourcePath, model);
            return result;
        }

        private async Task<string> ParseRazorTemplateFromResourceAsync<T>(string resourcePath, T model)
        {
            if (Engine.TemplateCache.Contains(resourcePath))
            {
                var template = Engine.TemplateCache.RetrieveTemplate(resourcePath).Template.TemplatePageFactory.Invoke();
                return await Engine.RenderTemplateAsync(template, model);
            }

            var razorTemplate = graphTemplateRepository.ReadResourceToString(resourcePath);
            var htmlFileOutput = await ParseAndCacheRazorTemplateAsync(resourcePath, razorTemplate, model);
            return htmlFileOutput;
        }

        private async Task<string> ParseAndCacheRazorTemplateAsync<T>(string templateName, string razorTemplate, T model)
        {
            var htmlFileOutput = await Engine.CompileRenderAsync(templateName, razorTemplate, model);// RenderTemplateAsync(razorTemplate, model); Razor.RunCompile(razorTemplate, templateName, typeof(T), model);
            return htmlFileOutput;
        }
    }
}