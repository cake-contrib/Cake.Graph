
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
        title:"GraphAliases",
        content:"GraphAliases",
        description:'',
        tags:''
    });

    a({
        id:1,
        title:"Node NodeData",
        content:"Node NodeData",
        description:'',
        tags:''
    });

    a({
        id:2,
        title:"GraphSettings",
        content:"GraphSettings",
        description:'',
        tags:''
    });

    a({
        id:3,
        title:"GraphTemplate",
        content:"GraphTemplate",
        description:'',
        tags:''
    });

    a({
        id:4,
        title:"GraphRunner",
        content:"GraphRunner",
        description:'',
        tags:''
    });

    a({
        id:5,
        title:"Node",
        content:"Node",
        description:'',
        tags:''
    });

    y({
        url:'/Cake.Graph/Cake.Graph/api/Cake.Graph/GraphAliases',
        title:"GraphAliases",
        description:""
    });

    y({
        url:'/Cake.Graph/Cake.Graph/api/Cake.Graph/NodeData',
        title:"Node.NodeData",
        description:""
    });

    y({
        url:'/Cake.Graph/Cake.Graph/api/Cake.Graph/GraphSettings',
        title:"GraphSettings",
        description:""
    });

    y({
        url:'/Cake.Graph/Cake.Graph/api/Cake.Graph/GraphTemplate_1',
        title:"GraphTemplate<T>",
        description:""
    });

    y({
        url:'/Cake.Graph/Cake.Graph/api/Cake.Graph/GraphRunner',
        title:"GraphRunner",
        description:""
    });

    y({
        url:'/Cake.Graph/Cake.Graph/api/Cake.Graph/Node',
        title:"Node",
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
