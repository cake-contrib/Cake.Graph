using System.Collections.Generic;
using System.Reflection;
using Cake.Graph.Templates;
using Shouldly;
using Xunit;

namespace Cake.Graph.Tests
{
    public class GraphTemplateRepositoryTests
    {
        public static string TestFileHeader = @"@using Cake.Graph
@inherits GraphTemplate<string>
";
        public static string TestFileContent = @"This is a test template with a string - @Model";

        [Fact]
        public void Can_Read_Embedded_Resource_To_String()
        {
            var graphTemplateRepository = new GraphTemplateRepository(
                Assembly.GetExecutingAssembly(),
                new Dictionary<TemplateTypes, string>
                {
                    { TemplateTypes.Cytoscape, "Cake.Graph.Tests.Data.index.cshtml" }
                });

            var fileContents = graphTemplateRepository.ReadResourceToString("Cake.Graph.Tests.Data.index.cshtml");
            fileContents.ShouldBe(TestFileHeader + TestFileContent);
        }
    }

    public class GraphTemplateManagerTests
    {
        private readonly IGraphTemplateRepository graphTemplateRepository = new GraphTemplateRepository(
            Assembly.GetExecutingAssembly(),
            new Dictionary<TemplateTypes, string>
            {
                { TemplateTypes.Cytoscape, "Cake.Graph.Tests.Data.index.cshtml" }
            });

        [Fact]
        public void A()
        {
            const string test = "test";
            var graphTemplateManager = new GraphTemplateManager(graphTemplateRepository);
            var result = graphTemplateManager.ParseTemplate(TemplateTypes.Cytoscape, test);
            result = graphTemplateManager.ParseTemplate(TemplateTypes.Cytoscape, test);

            result.ShouldBe(GraphTemplateRepositoryTests.TestFileContent.Replace("@Model", test));
        }
    }
}