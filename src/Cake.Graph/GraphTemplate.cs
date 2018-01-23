using RazorEngine.Templating;

namespace Cake.Graph
{
    /// <inheritdoc />
    /// <summary>
    /// Template for processing web files before saving
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GraphTemplate<T> : TemplateBase<T>
    {
        /// <summary>
        /// Model to use in Template
        /// </summary>
        public new T Model
        {
            get => base.Model;
        }
    }
}