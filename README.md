# Cake.Graph

## Information

### Build Status

Branch | Status
--- | ---
Master | [![Build status](https://ci.appveyor.com/api/projects/status/XXX/branch/master?svg=true)](https://ci.appveyor.com/project/cakecontrib/cake-graph/branch/master)
Develop | [![Build status](https://ci.appveyor.com/api/projects/status/XXX/branch/develop?svg=true)](https://ci.appveyor.com/project/cakecontrib/cake-graph/branch/develop)

### Nuget
[![NuGet](https://img.shields.io/nuget/v/Cake.Graph.svg)](https://www.nuget.org/packages/Cake.Graph/) 

### Licence
[![License](http://img.shields.io/:license-mit-blue.svg)](http://cake-contrib.mit-license.org)

## Usage

```c#
    #addin "Cake.Graph"

    Task("Graph")
        .Does(() => {
            Graph(Tasks).Deploy(s => {
                s.IsWyam = true;
            });
        });
```

## Scope
The purpose of this project is to provide a means of easily viewing build tasks defined in Cake scripts and their dependencies.