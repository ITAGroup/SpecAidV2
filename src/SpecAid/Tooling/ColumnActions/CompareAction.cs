using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpecAid.Tooling.Base;
using SpecAid.Tooling.Extensions;
using SpecAid.Tooling.Helper;
using SpecAid.Tooling.SetTranslations;
using SpecAid.Tooling.Translations;

namespace SpecAid.Tooling.ColumnActions
{
    public class CompareAction : ColumnAction, IComparerColumnAction
    {
        // Populated on Use When.
        private PropertyInfo _info;

        public CompareAction(
            RecallAid recallAid, Translator translator,
            SetTranslator setTranslator, ColumnActionFactory columnActionFactory,
            Type targetType, string columnName)
            : base(
                recallAid, translator, setTranslator,
                columnActionFactory, targetType, columnName)
        {
        }

        public CompareColumnResult GoGoCompareColumnAction(object target, string tableValue)
        {
            if (tableValue == ConstantStrings.IgnoreCell)
                return new CompareColumnResult();

            var compareResult = new CompareColumnResult();

            var expectedValue = Translator.Translate(_info, tableValue);
            expectedValue = SetTranslator.Translate(_info, target, expectedValue);

            var actualValue = GetActual(target);

            compareResult.ExpectedPrint = ToStringHelper.SafeToString(expectedValue);
            compareResult.ActualPrint = ToStringHelper.SafeToString(actualValue);

            // Refactor to be blah = blah 
            try
            {
                Assert.AreEqual(TypeConversion.SafeConvert(expectedValue,actualValue), actualValue);
            }
            catch (Exception)
            {
                compareResult.IsError = true;
                compareResult.ErrorMessage = "Error on Property " + _info.Name + ", Expected '" + compareResult.ExpectedPrint + "', Actual '" + compareResult.ActualPrint + "'";
            }

            return compareResult;
        }

        public CompareColumnResult GoGoCompareColumnAction(string tableValue)
        {
            if (tableValue == ConstantStrings.IgnoreCell)
                return new CompareColumnResult();

            // While the record is missing... this column isn't an error as it is n/a
            var compareResult = new CompareColumnResult();
            var expectedValue = Translator.Translate(_info, tableValue);

            compareResult.ExpectedPrint = ToStringHelper.SafeToString(expectedValue);
            compareResult.IsError = false;
            return compareResult;
        }

        public CompareColumnResult GoGoCompareColumnAction(object target)
        {
            // While the record is missing... this column isn't an error as it is n/a
            var compareResult = new CompareColumnResult();
            var actualValue = GetActual(target);
            compareResult.ActualPrint = ToStringHelper.SafeToString(actualValue);
            compareResult.IsError = false;
            return compareResult;
        }

        private object GetActual(object target)
        {
            if (target == null)
                return null;

            return _info.GetValue(target, null);
        }

        public override bool UseWhen()
        {
            _info = PropertyInfoHelper.GetCaseInsensitivePropertyInfo(TargetType, ColumnName);

            //could not find the property on the object
            return (_info != null);
        }

        public override int ConsiderOrder => ActionOrder.Compare.ToInt32();
    }
}
