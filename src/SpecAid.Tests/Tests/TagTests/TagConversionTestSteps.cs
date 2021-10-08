using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecAid.Tooling;
using SpecAid.Tooling.Helper;
using TechTalk.SpecFlow;

namespace SpecAid.Tests.TagTests
{
    [Binding]
    [Scope(Tag = "TagConversionTestSteps")]
    public class TagConversionTestSteps
    {
        private readonly Sat _sat;

        public TagConversionTestSteps(ScenarioContext scenarioContext)
        {
            _sat = new Sat(scenarioContext);
        }

        private const string TheTagTag = "theTag";

        [Given(@"Tag '(.*)'")]
        public void GivenTag(string tag)
        {
            _sat.Recall[TheTagTag] = tag;
        }

        [When(@"Tag Is Normalized")]
        public void WhenTagIsNormalized()
        {
            var tag = (string)_sat.Recall[TheTagTag];
            var normalizedTag = TagHelper.Normalizer(tag);
            _sat.Recall[TheTagTag] = normalizedTag;
        }

        [Then(@"Tag is '(.*)'")]
        public void ThenTagIs(string expectTag)
        {
            var actualTag = (string)_sat.Recall[TheTagTag];
            Assert.AreEqual(expectTag, actualTag);
        }
    }
}
