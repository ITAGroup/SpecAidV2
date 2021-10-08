using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecAid.Tooling.Base;
using TechTalk.SpecFlow;

namespace SpecAid.Tests.Common
{
    [Binding]
    [Scope(Tag = "CommonSteps")]
    public class CommonSteps
    {
        [Given(@"SpecAid Setting UseHashTag is '(.*)'")]
        public void GivenSpecAidSettingUseHashTagIs(bool setting)
        {
            SpecAidSettings.UseHashTag = setting;
        }

        [Then(@"Assert True")]
        public void ThenAssertTrue()
        {
            Assert.AreEqual(true, true);
        }
    }
}
