using Ploeh.AutoFixture;
using Shouldly;
using Xunit;

namespace Cake.Graph.Tests
{
    public class GraphSettingsTests
    {
        private IFixture autofixture = new Fixture();

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void UseWyam_Sets_IsWyam_Property(bool value)
        {
            var settings = new GraphSettings();
            settings.UseWyam(value);
            settings.IsWyam.ShouldBe(value);
        }

        [Fact]
        public void UseWyam_Sets_IsWyam_True_By_Default_Property()
        {
            var settings = new GraphSettings();
            settings.UseWyam();
            settings.IsWyam.ShouldBe(true);
        }

        [Fact]
        public void SetOutputPath_Sets_OutputPath_Property()
        {
            var settings = new GraphSettings();
            var randomString = autofixture.Create<string>();
            settings.SetOutputPath(randomString);
            settings.OutputPath.ShouldBe(randomString);
        }

        [Fact]
        public void SetNodeSetsPath_Sets_NodeSetsPathh_Property()
        {
            var settings = new GraphSettings();
            var randomString = autofixture.Create<string>();
            settings.SetNodeSetsPath(randomString);
            settings.NodeSetsPath.ShouldBe(randomString);
        }

        [Fact]
        public void SetMainPageName_Sets_MainPageName_Property()
        {
            var settings = new GraphSettings();
            var randomString = autofixture.Create<string>();
            settings.SetMainPageName(randomString);
            settings.MainPageName.ShouldBe(randomString);
        }

        [Fact]
        public void SetCssPath_Sets_CssPath_Property()
        {
            var settings = new GraphSettings();
            var randomString = autofixture.Create<string>();
            settings.SetCssPath(randomString);
            settings.CssPath.ShouldBe(randomString);
        }

        [Fact]
        public void SetCytoscapeJsPath_Sets_CytoscapeJsPath_Property()
        {
            var settings = new GraphSettings();
            var randomString = autofixture.Create<string>();
            settings.SetCytoscapeJsPath(randomString);
            settings.CytoscapeJsPath.ShouldBe(randomString);
        }

        [Fact]
        public void SetJsPath_Sets_JsPath_Property()
        {
            var settings = new GraphSettings();
            var randomString = autofixture.Create<string>();
            settings.SetJsPath(randomString);
            settings.JsPath.ShouldBe(randomString);
        }

        [Fact]
        public void SetJQueryPath_Sets_JQueryPath_Property()
        {
            var settings = new GraphSettings();
            var randomString = autofixture.Create<string>();
            settings.SetJQueryPath(randomString);
            settings.JQueryPath.ShouldBe(randomString);
        }

        [Fact]
        public void SetTaskListFileName_Sets_TaskListFileName_Property()
        {
            var settings = new GraphSettings();
            var randomString = autofixture.Create<string>();
            settings.SetTaskListFileName(randomString);
            settings.TaskListFileName.ShouldBe(randomString);
        }
    }
}
