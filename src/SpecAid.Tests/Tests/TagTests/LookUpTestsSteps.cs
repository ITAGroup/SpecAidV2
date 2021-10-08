using SpecAid.Tooling;
using SpecAidTests.Vaporware;
using TechTalk.SpecFlow;

namespace SpecAid.Tests.TagTests
{
    [Binding]
    [Scope(Tag = "LookupTestsSteps")]
    public class LookupTestsSteps
    {
        private readonly Sat _sat;

        public LookupTestsSteps(ScenarioContext scenarioContext)
        {
            _sat = new Sat(scenarioContext);
        }

        [Given(@"There are EveryThing Objects")]
        public void GivenIHaveEveryThingObjects(Table table)
        {
            _sat.Table.Create<EveryThingObject>(table);
        }

        [Then(@"There are EveryThing Objects via Lookup")]
        public void ThenThereAreEveryThingObjects(Table table)
        {
            _sat.Table.Compare(
                table,
                (row) =>
                {
                    if (row.ContainsKey("!LookUp"))
                    {
                        var tag = row["!LookUp"];
                        return (EveryThingObject)_sat.Recall[tag];
                    }
                    return null;
                });
        }

        [Then(@"There are EveryThing Objects via Recall")]
        public void ThenThereAreEveryThingObjectsViaRecall(Table table)
        {
            _sat.Table.CompareRecall<EveryThingObject>(table);
        }
    }
}
