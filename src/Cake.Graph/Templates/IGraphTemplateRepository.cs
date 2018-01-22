using System.Collections.Generic;

namespace Cake.Graph.Templates
{
    /// <summary>
    /// Reads resources to strings
    /// </summary>
    public interface IGraphTemplateRepository
    {
        /// <summary>
        /// Provides tempalte types and their keys
        /// </summary>
        IDictionary<TemplateTypes, string> TemplateResourcePaths { get; }
        /// <summary>
        /// Reads the specified resource to string
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        string ReadResourceToString(string resource);
    }
}