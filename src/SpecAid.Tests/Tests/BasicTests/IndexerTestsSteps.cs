using System.Collections.Generic;
using SpecAid;
using SpecAid.Tests.Vaporware;
using SpecAid.Tooling;
using SpecAidTests.Vaporware;
using TechTalk.SpecFlow;

namespace SpecAidTests.BasicTests
{
    [Binding]
    [Scope(Tag = "IndexerTestsSteps")]
    public class IndexerTestsSteps
    {
        private readonly Sat _sat;

        public IndexerTestsSteps(ScenarioContext scenarioContext)
        {
            _sat = new Sat(scenarioContext);
        }

        private readonly List<Dictionary<string, string>> _stringStringDictionaryList = new List<Dictionary<string, string>>();
        private readonly List<Dictionary<string, decimal>> _stringDecimalDictionaryList = new List<Dictionary<string, decimal>>();
        private readonly List<Dictionary<string, Informant>> _stringInformantDictionaryList = new List<Dictionary<string, Informant>>();
        private readonly List<Dictionary<int, string>> _intStringDictionaryList = new List<Dictionary<int, string>>();
        private readonly List<ComplexIndexerObject> _complexIndexerObjects = new List<ComplexIndexerObject>();

        [Given(@"The String String Dictionary")]
        public void GivenTheStringStringDictionary(Table table)
        {
            _sat.Table.Create<Dictionary<string, string>>(table, (tr, o) => { _stringStringDictionaryList.Add(o); });
        }

        [Then(@"The String String Dictionary")]
        public void ThenTheStringStringDictionary(Table table)
        {
            _sat.Table.Compare(table, _stringStringDictionaryList);
        }

        [Given(@"The String Decimal Dictionary")]
        public void GivenTheStringDecimalDictionary(Table table)
        {
            _sat.Table.Create<Dictionary<string, decimal>>(table, (tr, o) => { _stringDecimalDictionaryList.Add(o); });
        }

        [Then(@"The String Decimal Dictionary")]
        public void ThenTheStringDecimalDictionary(Table table)
        {
            _sat.Table.Compare(table, _stringDecimalDictionaryList);
        }

        [Given(@"The String Informant Dictionary")]
        public void GivenTheStringInformantDictionary(Table table)
        {
            _sat.Table.Create<Dictionary<string, Informant>>(table, (tr, o) => { _stringInformantDictionaryList.Add(o); });
        }

        [Then(@"The String Informant Dictionary")]
        public void ThenTheStringInformantDictionary(Table table)
        {
            _sat.Table.Compare(table, _stringInformantDictionaryList);
        }

        [Given(@"The Int String Dictionary")]
        public void GivenTheIntStringDictionary(Table table)
        {
            _sat.Table.Create<Dictionary<int, string>>(table, (tr, o) => { _intStringDictionaryList.Add(o); });
        }

        [Then(@"The Int String Dictionary")]
        public void ThenTheIntStringDictionary(Table table)
        {
            _sat.Table.Compare(table, _intStringDictionaryList);
        }

        [Given(@"The Complex Indexer Object")]
        public void GivenTheComplexIndexerObject(Table table)
        {
            _sat.Table.Create<ComplexIndexerObject>(table, (tr, o) => { _complexIndexerObjects.Add(o); });
        }

        [Then(@"The Complex Indexer Object")]
        public void ThenTheComplexIndexerObject(Table table)
        {
            _sat.Table.Compare(table, _complexIndexerObjects);
        }
    }
}
