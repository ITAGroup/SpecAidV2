using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecAid;
using SpecAid.Tooling;
using TechTalk.SpecFlow;

namespace SpecAidTests.TranslationTests
{
    [Binding]
    [Scope(Tag = "NullableEnumTranslationTestSteps")]
    public class NullableEnumTranslationTestSteps
    {
        private readonly Sat _sat;

        public NullableEnumTranslationTestSteps(ScenarioContext scenarioContext)
        {
            _sat = new Sat(scenarioContext);
        }

        [Given(@"I have celebrities")]
        public void GivenIHaveCelebrities(Table table)
        {
            _sat.Table.Create<Celebrity>(table);
        }

        [Then(@"'(.*)' graduated from '(.*)'")]
        public void ThenGraduatedFrom(string lastNameTag, string universityName)
        {
            var celebrity = (Celebrity) _sat.Recall[lastNameTag];
            Assert.AreEqual(universityName, celebrity.AlmaMater.ToString());
        }

        [Then(@"'(.*)' did not graduate")]
        public void ThenDidNotGraduate(string lastNameTag)
        {
            var celebrity = (Celebrity)_sat.Recall[lastNameTag];
            Assert.IsNull(celebrity.AlmaMater);
        }
    }

    public class Celebrity
    {
        public string LastName { get; set; }
        public AlmaMater? AlmaMater { get; set; }
    }

    public enum AlmaMater
    {
        Iowa,
        IowaState
    }
}
