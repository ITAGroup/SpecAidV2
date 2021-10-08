using System.Collections.Generic;
using SpecAid.Tooling;
using SpecAidTests.Vaporware;
using TechTalk.SpecFlow;

namespace SpecAid.Tests.BasicTests
{
    [Binding]
    [Scope(Tag = "TypeConversionTestsSteps")]
    public class TypeConversionTestsSteps
    {
        private readonly Sat _sat;

        public TypeConversionTestsSteps(ScenarioContext scenarioContext)
        {
            _sat = new Sat(scenarioContext);
        }

        private readonly List<EveryThingObject> _allEverything = new List<EveryThingObject>();

        [Given(@"There are EveryThing Objects")]
        public void GivenIHaveEveryThingObjects(Table table)
        {
            _sat.Table.Create<EveryThingObject>(table, (tr, o) => { _allEverything.Add(o); });
        }

        [Then(@"There are EveryThing Objects")]
        public void ThenThereAreEveryThingObjects(Table table)
        {
            _sat.Table.Compare(table, _allEverything);
        }
    }
}
