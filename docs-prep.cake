#addin nuget:?package=Cake.FileHelpers&version=2.0.0

Task("Copy-Readme-For-Docs")
    .WithCriteria(() => FileExists("README.md"))
    .Does(() => {
        var readme = FileReadText("README.md");

        var header = 
"Title: About " + BuildParameters.RepositoryName + @"
---
";
        var path = System.IO.Path.Combine(BuildParameters.WyamRootDirectoryPath.GetDirectoryName(), "input/about.md");
        FileWriteText(path, header + readme);
    });

BuildParameters.Tasks.PublishDocumentationTask
    .IsDependentOn("Copy-Readme-For-Docs");
BuildParameters.Tasks.PreviewDocumentationTask
    .IsDependentOn("Copy-Readme-For-Docs");
BuildParameters.Tasks.ForcePublishDocumentationTask
    .IsDependentOn("Copy-Readme-For-Docs");

Action<Action> UpdateCakeGraph = (action) => {
    var script = MakeAbsolute(File($"./{Guid.NewGuid()}.cake"));
    try
    {
        var arguments = new Dictionary<string, string>();

        // var nugetPackage = GetFiles("./BuildArtifacts/Packages/NuGet/*.nupkg")
        //     .OrderBy(x => x.FullPath)
        //     .First();

        if(BuildParameters.CakeConfiguration.GetValue("NuGet_UseInProcessClient") != null) {
            arguments.Add("nuget_useinprocessclient", BuildParameters.CakeConfiguration.GetValue("NuGet_UseInProcessClient"));
        }

        var command = $"#r ./src/Cake.Graph/bin/Cake.Graph.dll";
        //#addin nuget:file://./BuildArtifacts/Packages/NuGet/?package=Cake.Graph
        Information("A");
        System.IO.File.WriteAllText(script.FullPath, command);
        CakeExecuteScript(script,
            new CakeSettings
            {
                Arguments = arguments
            });
    }
    finally
    {
        if (FileExists(script))
        {
            //DeleteFile(script);
        }
    }

    action();
};

// Override Cake.Recipe command to use different settings from latest build
BuildParameters.Tasks.DeployGraphDocumentation.Task.Actions.Clear();
BuildParameters.Tasks.DeployGraphDocumentation.Does(() => {
    //Graph(Tasks).Deploy();
    //#r ./src/Cake.Graph/bin/Cake.Graph.dll
    //UpdateCakeGraph(() => Information("Cake updated?"));
    Graph(Tasks).Deploy(s => s.WithCytoscapeHtmlGenerator());
});