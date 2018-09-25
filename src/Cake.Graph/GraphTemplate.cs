//using RazorEngine.Templating;

using System.Threading.Tasks;
using RazorLight;

namespace Cake.Graph
{
    /// <inheritdoc />
    /// <summary>
    /// Template for processing web files before saving
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GraphTemplate<T> : TemplatePage<T>
    {
        /// <summary>
        /// Model to use in Template
        /// </summary>
        public new T Model => base.Model;

        public override Task ExecuteAsync()
        {
            return Task.CompletedTask;
        }
    }
}