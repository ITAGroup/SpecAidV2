using System;
using SpecAid.Tooling.Base;
using SpecAid.Tooling.Extensions;
using SpecAid.Tooling.SetTranslations;
using SpecAid.Tooling.Translations;

namespace SpecAid.Tooling.ColumnActions
{
    public class IgnoreAction : ColumnAction, IComparerColumnAction, ICreatorColumnAction
    {
        public IgnoreAction(
            RecallAid recallAid,
            Translator translator, SetTranslator setTranslator,
            ColumnActionFactory columnActionFactory,
            Type targetType, string columnName)
            : base(
                recallAid, translator, setTranslator, columnActionFactory,
                targetType, columnName)
        {
        }

        public IgnoreAction(string columnName)
            : base(null, null, null, null, null, columnName)
        {

        }

        public CompareColumnResult GoGoCompareColumnAction(object obj, string tableValue)
        {
            if (tableValue == ConstantStrings.IgnoreCell)
                return new CompareColumnResult();

            var compareResults = new CompareColumnResult
            {
                ActualPrint = tableValue,
                ExpectedPrint = tableValue
            };

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


        public void GoGoCreateColumnAction(object obj, string tableValue)
        {
            //intentionally do nothing
        }

        public override bool UseWhen()
        {
            return ColumnName.StartsWith("!");
        }

        public override int ConsiderOrder => ActionOrder.Ignore.ToInt32();
    }
}
