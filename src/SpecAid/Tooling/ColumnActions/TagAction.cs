using System;
using SpecAid.Tooling.Base;
using SpecAid.Tooling.Extensions;
using SpecAid.Tooling.SetTranslations;
using SpecAid.Tooling.Translations;

namespace SpecAid.Tooling.ColumnActions
{
    //Action for storing items in the dictionary
    public class TagAction : ColumnAction, ICreatorColumnAction, IComparerColumnAction
    {
        public TagAction(
            RecallAid recallAid, Translator translator,
            SetTranslator setTranslator, ColumnActionFactory columnActionFactory,
            Type targetType, string columnName)
            : base(recallAid, translator, setTranslator,
                columnActionFactory, targetType, columnName)
        {
        }

        public CompareColumnResult GoGoCompareColumnAction(object obj, string tableValue)
        {
            if (tableValue == ConstantStrings.IgnoreCell)
                return new CompareColumnResult();

            var compareResults = new CompareColumnResult();
            compareResults.ActualPrint = tableValue;
            compareResults.ExpectedPrint = tableValue;

            TagIt(obj, tableValue);

            return compareResults;
        }

        public CompareColumnResult GoGoCompareColumnAction(object obj)
        {
            var compareResults = new CompareColumnResult();
            return compareResults;
        }

        public CompareColumnResult GoGoCompareColumnAction(string tableValue)
        {
            if (tableValue == ConstantStrings.IgnoreCell)
                return new CompareColumnResult();

            var compareResults = new CompareColumnResult();
            compareResults.ExpectedPrint = tableValue;

            return compareResults;
        }

        public void GoGoCreateColumnAction(object target, string tableValue)
        {
            if (tableValue == ConstantStrings.IgnoreCell)
                return;

            TagIt(target, tableValue);
        }

        private void TagIt(object target, string tableValue)
        {
            if (string.IsNullOrWhiteSpace(tableValue))
                return;

            RecallAid[tableValue] = target;
        }

        public override bool UseWhen()
        {
            return "Tag It".Equals(ColumnName, StringComparison.InvariantCultureIgnoreCase);
        }

        public override int ConsiderOrder => ActionOrder.Tag.ToInt32();
    }
}
