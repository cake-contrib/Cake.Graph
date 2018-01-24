using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Graph.Generators;
using Moq;

namespace Cake.Graph.Tests
{
    public static class TestHelpers
    {
        public static Mock<ICakeContext> GetMockCakeContext()
        {
            var mockContext = new Mock<ICakeContext>();
            mockContext
                .Setup(x => x.Log)
                .Returns(new Mock<ICakeLog>().Object);

            return mockContext;
        }

        public static ITaskGraphGenerator GetEmptyTaskGraphGenerator()
        {
            var mockTaskGraphGenerator = new Mock<ITaskGraphGenerator>();
            mockTaskGraphGenerator
                .Setup(x => x.Extension)
                .Returns("test");
            mockTaskGraphGenerator.Setup(x => x.Serialize(It.IsAny<ICakeContext>(), It.IsAny<ICakeTaskInfo>(),
                    It.IsAny<IReadOnlyList<ICakeTaskInfo>>()))
                .Returns((ICakeContext context, ICakeTaskInfo task, IReadOnlyList<ICakeTaskInfo> tasks) => "");

            return mockTaskGraphGenerator.Object;
        }
        
        public static IReadOnlyList<ICakeTaskInfo> CreateTasksWithDependencies()
        {
            var tasks = new List<ICakeTaskInfo>();
            var taskA = new ActionTask("A");
            var taskB = new ActionTask("B");
            var taskC = new ActionTask("C");
            var taskD = new ActionTask("D");

            taskB.AddDependency("A");
            taskC.AddDependency("B");
            taskD.AddDependency("A");

            tasks.Add(taskA);
            tasks.Add(taskB);
            tasks.Add(taskC);
            tasks.Add(taskD);

            return tasks;
        }

        public const string TaskCMermaidPattern = @"<div class=""mermaid"">
(\s)?graph TD;
(\s)?C-->B;
(\s)?B-->A;
(\s)?</div>
(\s)?";

        public const string TaskCCytoscapePattern = @"\[\{""data"":\{""id"":""C"",""source"":null,""target"":null\}\},\{""data"":\{""id"":""[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}"",""source"":""C"",""target"":""B""\}\},\{""data"":\{""id"":""B"",""source"":null,""target"":null\}\},\{""data"":\{""id"":""[0-9a-f]{8}(-[0-9a-f]{4}){3}-[0-9a-f]{12}"",""source"":""B"",""target"":""A""\}\},\{""data"":\{""id"":""A"",""source"":null,""target"":null\}\}\]";
    }
}