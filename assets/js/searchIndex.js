
var camelCaseTokenizer = function (obj) {
    var previous = '';
    return obj.toString().trim().split(/[\s\-]+|(?=[A-Z])/).reduce(function(acc, cur) {
        var current = cur.toLowerCase();
        if(acc.length === 0) {
            previous = current;
            return acc.concat(current);
        }
        previous = previous.concat(current);
        return acc.concat([current, previous]);
    }, []);
}
lunr.tokenizer.registerFunction(camelCaseTokenizer, 'camelCaseTokenizer')
var searchModule = function() {
    var idMap = [];
    function y(e) { 
        idMap.push(e); 
    }
    var idx = lunr(function() {
        this.field('title', { boost: 10 });
        this.field('content');
        this.field('description', { boost: 5 });
        this.field('tags', { boost: 50 });
        this.ref('id');
        this.tokenizer(camelCaseTokenizer);

        this.pipeline.remove(lunr.stopWordFilter);
        this.pipeline.remove(lunr.stemmer);
    });
    function a(e) { 
        idx.add(e); 
    }

    a({
        id:0,
        title:"GraphTemplateRepository",
        content:"GraphTemplateRepository",
        description:'',
        tags:''
    });

    a({
        id:1,
        title:"IGraphTemplateRepository",
        content:"IGraphTemplateRepository",
        description:'',
        tags:''
    });

    a({
        id:2,
        title:"CytoscapeHtmlGenerator",
        content:"CytoscapeHtmlGenerator",
        description:'',
        tags:''
    });

    a({
        id:3,
        title:"Node",
        content:"Node",
        description:'',
        tags:''
    });

    a({
        id:4,
        title:"GraphSettingsExtensions",
        content:"GraphSettingsExtensions",
        description:'',
        tags:''
    });

    a({
        id:5,
        title:"GraphHtmlModel",
        content:"GraphHtmlModel",
        description:'',
        tags:''
    });

    a({
        id:6,
        title:"CakeTaskExtensions",
        content:"CakeTaskExtensions",
        description:'',
        tags:''
    });

    a({
        id:7,
        title:"GraphSettings",
        content:"GraphSettings",
        description:'',
        tags:''
    });

    a({
        id:8,
        title:"MermaidGraphGenerator",
        content:"MermaidGraphGenerator",
        description:'',
        tags:''
    });

    a({
        id:9,
        title:"MermaidHtmlGenerator",
        content:"MermaidHtmlGenerator",
        description:'',
        tags:''
    });

    a({
        id:10,
        title:"TemplateTypes",
        content:"TemplateTypes",
        description:'',
        tags:''
    });

    a({
        id:11,
        title:"GraphAliases",
        content:"GraphAliases",
        description:'',
        tags:''
    });

    a({
        id:12,
        title:"GraphTemplateManager",
        content:"GraphTemplateManager",
        description:'',
        tags:''
    });

    a({
        id:13,
        title:"GraphTemplate",
        content:"GraphTemplate",
        description:'',
        tags:''
    });

    a({
        id:14,
        title:"TaskGraphGeneratorHelpers",
        content:"TaskGraphGeneratorHelpers",
        description:'',
        tags:''
    });

    a({
        id:15,
        title:"GraphRunner",
        content:"GraphRunner",
        description:'',
        tags:''
    });

    a({
        id:16,
        title:"Node NodeData",
        content:"Node NodeData",
        description:'',
        tags:''
    });

    a({
        id:17,
        title:"IGraphTemplateManager",
        content:"IGraphTemplateManager",
        description:'',
        tags:''
    });

    a({
        id:18,
        title:"CytoscapeGraphGenerator",
        content:"CytoscapeGraphGenerator",
        description:'',
        tags:''
    });

    a({
        id:19,
        title:"ITaskGraphGenerator",
        content:"ITaskGraphGenerator",
        description:'',
        tags:''
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph.Templates/GraphTemplateRepository',
        title:"GraphTemplateRepository",
        description:""
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph.Templates/IGraphTemplateRepository',
        title:"IGraphTemplateRepository",
        description:""
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph.Generators/CytoscapeHtmlGenerator',
        title:"CytoscapeHtmlGenerator",
        description:""
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph/Node',
        title:"Node",
        description:""
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph/GraphSettingsExtensions',
        title:"GraphSettingsExtensions",
        description:""
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph.Models/GraphHtmlModel',
        title:"GraphHtmlModel",
        description:""
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph.Generators/CakeTaskExtensions',
        title:"CakeTaskExtensions",
        description:""
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph/GraphSettings',
        title:"GraphSettings",
        description:""
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph.Generators/MermaidGraphGenerator',
        title:"MermaidGraphGenerator",
        description:""
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph.Generators/MermaidHtmlGenerator',
        title:"MermaidHtmlGenerator",
        description:""
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph.Templates/TemplateTypes',
        title:"TemplateTypes",
        description:""
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph/GraphAliases',
        title:"GraphAliases",
        description:""
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph.Templates/GraphTemplateManager',
        title:"GraphTemplateManager",
        description:""
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph/GraphTemplate_1',
        title:"GraphTemplate<T>",
        description:""
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph.Generators/TaskGraphGeneratorHelpers',
        title:"TaskGraphGeneratorHelpers",
        description:""
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph/GraphRunner',
        title:"GraphRunner",
        description:""
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph/NodeData',
        title:"Node.NodeData",
        description:""
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph.Templates/IGraphTemplateManager',
        title:"IGraphTemplateManager",
        description:""
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph.Generators/CytoscapeGraphGenerator',
        title:"CytoscapeGraphGenerator",
        description:""
    });

    y({
        url:'/Cake.Graph/api/Cake.Graph.Generators/ITaskGraphGenerator',
        title:"ITaskGraphGenerator",
        description:""
    });

    return {
        search: function(q) {
            return idx.search(q).map(function(i) {
                return idMap[i.ref];
            });
        }
    };
}();
