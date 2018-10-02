using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Cake.Graph.Templates;
using Shouldly;
using Xunit;

namespace Cake.Graph.Tests
{
    public class GraphTemplateManagerTests
    {
        private readonly IGraphTemplateRepository graphTemplateRepository = new GraphTemplateRepository(
            Assembly.GetExecutingAssembly(),
            new Dictionary<TemplateTypes, string>
            {
                { TemplateTypes.Cytoscape, "Cake.Graph.Tests.Data.index.cshtml" }
            });
            
        [Fact]
        public async Task Manager_Parses_Razor_Template()
        {
            const string test = "test";
            var graphTemplateManager = new GraphTemplateManager(graphTemplateRepository);
            var result = await graphTemplateManager.ParseTemplateAsync(TemplateTypes.Cytoscape, test);
            result = await graphTemplateManager.ParseTemplateAsync(TemplateTypes.Cytoscape, test);

            result.ShouldBe(GraphTemplateRepositoryTests.TestFileContent.Replace("@Model", test));
        }
    }
}