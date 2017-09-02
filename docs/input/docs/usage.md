Description: Library usage instructions.
---
To use Cake.Graph add the following line to the top of your cake script

```c#
#addin "Cake.Graph"
```

After this you can use the Graph method alias to create a GraphRunner and then can deploy the files. See [About Cake.Graph](../about.html) for details.

```csharp
#addin "Cake.Graph"

var target = Argument("target", "Default");

Task("Graph")
  .Does(() =>
{
    Graph(Tasks).Deploy(s => {
      s.IsWyam = true;
    });
});

RunTarget(target);
```