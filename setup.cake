#load nuget:https://www.myget.org/F/cake-contrib/api/v2?package=Cake.Recipe&prereleases
#reference "D:\wozzo\Documents\Visual Studio 2017\Projects\Cake.Graph2\src\Cake.Graph\Cake.Graph\bin\Debug\Cake.Graph.dll"
#load "docs-prep.cake"

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            title: "Cake.Graph",
                            repositoryOwner: "wozzo",
                            repositoryName: "Cake.Graph",
                            appVeyorAccountName: "wozzo");

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context,
                            dupFinderExcludePattern: new string[] {
                                BuildParameters.RootDirectoryPath + "/src/Cake.Graph.Tests/*.cs" },
                            testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* ",
                            testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
                            testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

Task("Graph").Does(() => {
    Graph(Tasks)
        .Deploy(settings => {
            settings.IsWyam = true;
        });
});

BuildParameters.Tasks.PublishDocumentationTask
    .IsDependentOn("Graph");
BuildParameters.Tasks.PreviewDocumentationTask
    .IsDependentOn("Graph");
BuildParameters.Tasks.ForcePublishDocumentationTask
    .IsDependentOn("Graph");

Build.Run();