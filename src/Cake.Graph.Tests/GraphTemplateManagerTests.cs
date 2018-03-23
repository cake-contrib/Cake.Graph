using System.Collections.Generic;
using System.Reflection;
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

        /* GraphTemplateManagerTests.cs(26,29): error CS0103: The name 'GraphTemplateRepositoryTests' does not exist in the current context [C:\SourceCode\Cake.Graph\src\Cake.Graph.Tests\Cake.Graph.Tests.csproj]
        [Fact]
        public void Manager_Parses_Razor_Template()
        {
            const string test = "test";
            var graphTemplateManager = new GraphTemplateManager(graphTemplateRepository);
            var result = graphTemplateManager.ParseTemplate(TemplateTypes.Cytoscape, test);
            result = graphTemplateManager.ParseTemplate(TemplateTypes.Cytoscape, test);

            result.ShouldBe(GraphTemplateRepositoryTests.TestFileContent.Replace("@Model", test));
        }*/
    }
}