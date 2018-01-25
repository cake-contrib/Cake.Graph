using System.IO;
using Shouldly;
using Xunit;

namespace Cake.Graph.Tests
{
    public class GraphRunnerTests
    {
        private const string outputPath = "TestData";

        [Fact]
        public void Deploy_Creates_Files_For_Each_Task()
        {
            if (Directory.Exists(outputPath))
                Directory.Delete(outputPath, true);

            var cakeContext = TestHelpers.GetMockCakeContext();
            var tasks = TestHelpers.CreateTasksWithDependencies();

            var emptyGenerator = TestHelpers.GetEmptyTaskGraphGenerator();
            cakeContext.Object.Graph(tasks).Deploy(s => s
                .WithCustomGenerator(emptyGenerator)
                .SetOutputPath(outputPath)
            );

            var files = Directory.GetFiles(outputPath, $"*.{emptyGenerator.Extension}");
            files.Length.ShouldBe(tasks.Count);
        }
    }
}
