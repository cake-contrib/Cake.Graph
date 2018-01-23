using Cake.Graph.Generators;
using Moq;
using Ploeh.AutoFixture;
using Shouldly;
using Xunit;

namespace Cake.Graph.Tests
{
    public class GraphSettingsTests
    {
        private readonly IFixture autofixture = new Fixture();

        [Fact]
        public void SetOutputPath_Sets_OutputPath_Property()
        {
            var settings = new GraphSettings();
            var randomString = autofixture.Create<string>();
            settings.SetOutputPath(randomString);
            settings.OutputPath.ShouldBe(randomString);
        }

        [Fact]
        public void WithMermaidGenerator_Sets_Generator_Property()
        {
            var settings = new GraphSettings();
            settings.WithMermaidGenerator();
            settings.Generator.ShouldBeOfType<MermaidGraphGenerator>();
        }

        [Fact]
        public void WithMermaidHtmlGenerator_Sets_Generator_Property()
        {
            var settings = new GraphSettings();
            settings.WithMermaidHtmlGenerator();
            settings.Generator.ShouldBeOfType<MermaidHtmlGenerator>();
        }

        [Fact]
        public void WithCytoscapeGenerator_Sets_Generator_Property()
        {
            var settings = new GraphSettings();
            settings.WithCytoscapeGenerator();
            settings.Generator.ShouldBeOfType<CytoscapeGraphGenerator>();
        }

        [Fact]
        public void WithCytoscapeHtmlGenerator_Sets_Generator_Property()
        {
            var settings = new GraphSettings();
            settings.WithCytoscapeHtmlGenerator();
            settings.Generator.ShouldBeOfType<CytoscapeHtmlGenerator>();
        }

        [Fact]
        public void WithCustomGenerator_Sets_Generator_Property()
        {
            var settings = new GraphSettings();
            var generator = new Mock<ITaskGraphGenerator>();
            settings.WithCustomGenerator(generator.Object);
            settings.Generator.ShouldBe(generator.Object);
        }
    }
}
