using System;
using SpecAid.Tooling.Base;
using SpecAid.Tooling.Extensions;
using SpecAid.Tooling.SetTranslations;
using SpecAid.Tooling.Translations;

namespace SpecAid.Tooling.ColumnActions
{
    public class UnknownAction : ColumnAction, IComparerColumnAction, ICreatorColumnAction
    {
        public UnknownAction(
            RecallAid recallAid, Translator translator,
            SetTranslator setTranslator, ColumnActionFactory columnActionFactory,
            Type targetType, string columnName)
            : base(
                recallAid, translator, setTranslator,
                columnActionFactory, targetType, columnName)
        {
        }

        public CompareColumnResult GoGoCompareColumnAction(object obj, string tableValue)
        {
            return new CompareColumnResult();
        }

        public CompareColumnResult GoGoCompareColumnAction(object obj)
        {
            return new CompareColumnResult();
        }

        public CompareColumnResult GoGoCompareColumnAction(string tableValue)
        {
            return new CompareColumnResult();
        }

        public void GoGoCreateColumnAction(object obj, string tableValue)
        {
            //intentionally do nothing
        }

        public override bool UseWhen()
        {
            if (!SpecAidSettings.AssertWhenUnknownColumn)
            {
                Console.WriteLine("Unable to determine what to do with column:" + base.ColumnName);
                return true;
            }

            return false;
        }

        public override int ConsiderOrder => ActionOrder.Unknown.ToInt32();
    }
}
