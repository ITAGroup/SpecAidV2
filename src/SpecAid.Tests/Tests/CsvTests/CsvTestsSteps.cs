using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecAid.Tooling;
using SpecAid.Tooling.Helper;
using TechTalk.SpecFlow;

namespace SpecAid.Tests.CsvTests
{
    [Binding]
    [Scope(Tag = "CsvTests")]
    public class CsvTests
    {
        private readonly Sat _sat;

        public CsvTests(ScenarioContext scenarioContext)
        {
            _sat = new Sat(scenarioContext);
        }

        private List<string> _theList;
        private string _theString;

        [Given(@"String '(.*)'")]
        public void GivenIHaveString(string theString)
        {
            _theList = CsvHelper.Split(theString);
        }

        [Then(@"List")]
        public void ThenIHaveList(Table table)
        {
            _sat.Table.CompareSorted(table,_theList);
        }

        [Given(@"String For Csv '(.*)'")]
        public void GivenStringForCsv(string theString)
        {
            _theString = CsvHelper.ToCsv(theString);
        }

        [Then(@"Safe Csv String '(.*)'")]
        public void ThenSafeCsvString(string expected)
        {
            Assert.AreEqual(expected,_theString);
        }
    }
}
