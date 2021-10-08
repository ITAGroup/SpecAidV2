using System;
using SpecAid.Tooling.ColumnActions;
using SpecAid.Tooling.SetTranslations;
using SpecAid.Tooling.Translations;
using TechTalk.SpecFlow;

namespace SpecAid.Tooling
{
    // Sat.  Specflow Aid and Tooling.
    public class Sat
    {
        private readonly ScenarioContext _scenarioContext;

        public Sat(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        private const string RecallAidKey = "InternalRecallAidKey";
        private RecallAid _recallAid;

        public RecallAid Recall
        {
            get
            {
                return _recallAid ??= Singleton(RecallAidKey,
                    () => new RecallAid(_scenarioContext));
            }
        }

        private const string TableAidKey = "InternalTableAidKey";
        private TableAid _tableAid;

        public TableAid Table
        {
            get
            {
                return _tableAid ??= Singleton(TableAidKey,
                    () => new TableAid(ColumnActionFactory, Recall));
            }
        }

        private const string FieldAidKey = "InternalFieldAidKey";
        private FieldAid _fieldAid;

        public FieldAid Field
        {
            get
            {
                return _fieldAid ??= Singleton(FieldAidKey,
                    () => new FieldAid(ColumnActionFactory));
            }
        }

        private const string ColumnActionFactoryKey = "InternalColumnActionFactoryKey";
        private ColumnActionFactory _columnActionFactory;

        public ColumnActionFactory ColumnActionFactory
        {
            get
            {
                return _columnActionFactory ??= Singleton(ColumnActionFactoryKey,
                    () => new ColumnActionFactory(Recall, Translator, SetTranslator));
            }
        }

        private const string TranslatorKey = "InternalTranslatorKey";
        private Translator _translator;

        private Translator Translator
        {
            get
            {
                return _translator ??= Singleton(TranslatorKey,
                    () => new Translator(Recall));
            }
        }

        private const string SetTranslatorKey = "InternalSetTranslatorKey";
        private SetTranslator _setTranslator;

        private SetTranslator SetTranslator
        {
            get
            {
                return _setTranslator ??= Singleton(SetTranslatorKey,
                    () => new SetTranslator());
            }
        }

        private TItem Singleton<TItem>(string key, Func<TItem> create)
        {
            // Another Step file must have created it.
            if (_scenarioContext.ContainsKey(key))
                return (TItem)_scenarioContext[key];

            var reference = create();
            _scenarioContext[key] = reference;
            return reference;
        }
    }
}
