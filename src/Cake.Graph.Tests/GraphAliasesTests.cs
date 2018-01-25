using Shouldly;
using Xunit;

namespace Cake.Graph.Tests
{
    public class GraphAliasesTests
    {
        [Fact]
        public void Graph_Returns_GraphRunner()
        {
            var context = TestHelpers.GetMockCakeContext();
            var tasks = TestHelpers.CreateTasksWithDependencies();

            var result = context.Object.Graph(tasks);

            result.ShouldBeOfType(typeof(GraphRunner));
        }
    }
}