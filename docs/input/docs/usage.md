Description: Library usage instructions.
---
To use Cake.Graph add the following line to the top of your cake script

```c#
#addin "Cake.Graph"
```

After this you can use the Graph method alias to create a GraphRunner and then can deploy the files. See [About Cake.Graph](../about.html) for details.

# Deploy Cake.Graph

There are two stages to the Cake.Graph deployment process. Deploying the web files and deploying the nodesets. Some settings such as the `TaskListFileName` or `OutputPath` are shared by the two steps and must be the same for each. Overloads are available for all deployment methods which will accept either a `GraphSettings` object or an `Action<GraphSettings>`.

It's recommended that you perform the "Deploy the web files" step once and commit the resulting files, then add the "Deploy the node set files" step to your build process preceding any generate documentation step. You should also add the node sets path to your .gitignore file.

## Deploy the web files

This step will deploy several css, javascript, html and razor files which make up the web page which displays the graphs. The page uses [Cytoscape.js](http://js.cytoscape.org/) to interpret the nodeset files and produce the graphs.

```csharp
Task("Deploy-Graph-WebFiles")
    .Does(() =>
    {
        Graph(Tasks).DeployWebFiles(s => s
            .UseWyam()
            .SetOutputPath("docs/output")
        );
    });
```

## Deploy the node set files

This step will check each task and its dependencies building a collection of nodes and edges representing tasks and their dependencies. This information will be written to a set of json files and are referred to as the nodeset files. [Cytoscape.js](http://js.cytoscape.org/) is able to interpret this information and produce the graphs.

```csharp
Task("Deploy-Graph-Nodesets")
    .Does(() =>
    {
        Graph(Tasks).GenerateNodeSets(s => s
            .UseWyam()
            .SetOutputPath("docs/output")
        );
    });
```

