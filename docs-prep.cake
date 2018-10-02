#addin nuget:?package=Cake.FileHelpers&version=3.1.0

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