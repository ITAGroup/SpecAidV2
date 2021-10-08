using System;
using SpecAid.Tooling.Helper;
using TechTalk.SpecFlow;

namespace SpecAid.Tooling
{
    public class RecallAid
    {
        private readonly ScenarioContext _scenarioContext;

        public RecallAid(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public object this[string key]
        {
            get
            {
                var lookup = NormalizeKey(key);

                if (!_scenarioContext.ContainsKey(lookup))
                {
                    throw new Exception($"error: Given key \"{key}\" not found in dictionary");
                }

                return _scenarioContext[lookup];
            }
            set
            {
                var lookup = NormalizeKey(key);

                if (_scenarioContext.ContainsKey(lookup))
                {
                    Console.WriteLine("Caution: ScenarioContext already has item: " + key);
                }
                _scenarioContext[lookup] = value;
            }
        }

        public object this[Type type]
        {
            get => this[type.ToString()];
            set
            {
                var stuff = type.ToString();
                this[stuff] = value;
            }
        }

        public bool ContainsKey(string key)
        {
            var lookup = NormalizeKey(key);
            return _scenarioContext.ContainsKey(lookup);
        }

        public bool ContainsKey(Type type)
        {
            return ContainsKey(type.ToString());
        }

        public void Remove(string key)
        {
            string lookup = key.ToLower();
            _scenarioContext.Remove(lookup);
        }

        public void Remove(Type type)
        {
            Remove(type.ToString());
        }

        private string NormalizeKey(string key)
        {
            var keyLower = key.ToLowerInvariant();
            var tagNormal = TagHelper.Normalizer(keyLower);
            return tagNormal;
        }
    }
}
