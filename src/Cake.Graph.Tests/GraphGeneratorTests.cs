using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Graph.Generators;
using Shouldly;
using Xunit;

namespace Cake.Graph.Tests
{
    public class GraphGeneratorTests
    {
        private readonly ICakeTaskInfo taskC;
        private readonly IReadOnlyList<ICakeTaskInfo> tasks;

        public GraphGeneratorTests()
        {
            tasks = TestHelpers.CreateTasksWithDependencies();
            taskC = tasks.First(x => string.Equals(x.Name, "C"));
        }

        [Theory]
        [InlineData(typeof(MermaidGraphGenerator), TestHelpers.TaskCMermaidPattern)]
        [InlineData(typeof(CytoscapeGraphGenerator), TestHelpers.TaskCCytoscapePattern)]
        [InlineData(typeof(MermaidHtmlGenerator), TestHelpers.TaskCMermaidPattern)]
        [InlineData(typeof(CytoscapeHtmlGenerator), TestHelpers.TaskCCytoscapePattern)]
        [InlineData(typeof(CytoscapeWyamGenerator), TestHelpers.TaskCCytoscapePattern)]
        public void Serializes_Tasks_With_Dependencies_Correctly(Type generatorType, string expectedResult)
        {
            var mockContext = TestHelpers.GetMockCakeContext();
            var graphGenerator = (ITaskGraphGenerator)Activator.CreateInstance(generatorType);
            var result = graphGenerator.Serialize(mockContext.Object, taskC, tasks);
            result.ShouldMatch(expectedResult);
        }

        [Theory]
        [InlineData(typeof(MermaidGraphGenerator), "md")]
        [InlineData(typeof(CytoscapeGraphGenerator), "json")]
        [InlineData(typeof(MermaidHtmlGenerator), "html")]
        [InlineData(typeof(CytoscapeHtmlGenerator), "html")]
        [InlineData(typeof(CytoscapeWyamGenerator), "cshtml")]
        public void Check_Generator_Extensions(Type generatorType, string expectedExtension)
        {
            var graphGenerator = (ITaskGraphGenerator)Activator.CreateInstance(generatorType);
            graphGenerator.Extension.ShouldBe(expectedExtension);
        }
    }
}