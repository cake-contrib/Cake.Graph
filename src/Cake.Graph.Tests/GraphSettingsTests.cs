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
        public void SetNodeSetsPath_Sets_NodeSetsPathh_Property()
        {
            var settings = new GraphSettings();
            var randomString = autofixture.Create<string>();
            settings.SetNodeSetsPath(randomString);
            settings.NodeSetsPath.ShouldBe(randomString);
        }
    }
}
