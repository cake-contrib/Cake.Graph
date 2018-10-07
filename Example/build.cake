#addin Cake.Graph&prerelease&loaddependencies=true

Task("Dependency");
Task("Graph")
    .Does(() => {
        Graph(Tasks).DeployAsync();
    });
Task("Default")
    .IsDependentOn("Dependency")
    .IsDependentOn("Graph");

RunTarget("Default");