using System;
using SpecAid.Tooling.ColumnActions;
using SpecAid.Tooling.SetTranslations;
using SpecAid.Tooling.Translations;

namespace SpecAid.Tooling.Base
{
    public abstract class ColumnAction : IColumnAction
    {
        protected RecallAid RecallAid { get; }
        protected Translator Translator { get; }
        protected SetTranslator SetTranslator { get; }
        protected ColumnActionFactory ColumnActionFactory { get; }

        public Type TargetType { get; set; }
        public string ColumnName { get; set; }

        protected ColumnAction(
            RecallAid recallAid,
            Translator translator,
            SetTranslator setTranslator,
            ColumnActionFactory columnActionFactory,
            Type targetType, string columnName)
        {
            RecallAid = recallAid;
            Translator = translator;
            SetTranslator = setTranslator;
            ColumnActionFactory = columnActionFactory;

            TargetType = targetType;
            ColumnName = columnName;
        }

        public abstract bool UseWhen();
        public abstract int ConsiderOrder { get; }
    }
}
