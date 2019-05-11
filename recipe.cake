#load nuget:?package=Cake.Recipe&version=1.0.0
#load "docs-prep.cake"

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./src",
                            title: "Cake.Graph",
                            repositoryOwner: "cake-contrib",
                            repositoryName: "Cake.Graph",
                            appVeyorAccountName: "cakecontrib",
                            shouldRunGitVersion: DirectoryExists(".git"), // This would allow building even without using a git repository
                            shouldRunDotNetCorePack: true);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context,
                            dupFinderExcludePattern: new string[] {
                                BuildParameters.RootDirectoryPath + "/src/Cake.Graph.Tests/*.cs"
                                },
                            testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Testing]* -[*.Tests]* -[Moq*]* -[AutoFixture*]* ",
                            testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
                            testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

#break
Build.RunDotNetCore();
