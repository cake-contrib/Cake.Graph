using System.Threading.Tasks;

namespace Cake.Graph.Templates
{
    /// <summary>
    /// Parses templates into their resulting html/string
    /// </summary>
    public interface IGraphTemplateManager
    {
        /// <summary>
        /// Parse the template and return the string result
        /// </summary>
        /// <param name="templateTypeKey"></param>
        /// <param name="model"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<string> ParseTemplateAsync<T>(TemplateTypes templateTypeKey, T model);
    }
}