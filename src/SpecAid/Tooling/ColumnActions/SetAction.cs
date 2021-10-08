using System;
using System.Reflection;
using SpecAid.Tooling.Base;
using SpecAid.Tooling.Extensions;
using SpecAid.Tooling.Helper;
using SpecAid.Tooling.SetTranslations;
using SpecAid.Tooling.Translations;

namespace SpecAid.Tooling.ColumnActions
{
    //Action for setting the value in the object
    public class SetAction : ColumnAction, ICreatorColumnAction
    {
        private PropertyInfo _info;


        public SetAction(
            RecallAid recallAid, Translator translator,
            SetTranslator setTranslator, ColumnActionFactory columnActionFactory,
            Type targetType, string columnName)
            : base(
                recallAid, translator, setTranslator,
                columnActionFactory, targetType, columnName)
        {
        }

        public void GoGoCreateColumnAction(object target, string tableValue)
        {
            if (tableValue == ConstantStrings.IgnoreCell)
                return;

            var value = Translator.Translate(_info, tableValue);

            // Convert the Translated value to the type of the targeted property.
            // Translators guilty of not honoring the PropertyInfo ... Tag, Deep Link, Dates, etcetera

            value = SetTranslator.Translate(_info, target, value);

            try
            {
                _info.SetValue(target, value, null);
            }
            catch (ArgumentException e)
            {
                var message = string.Format(
                    "Unable to set value of property {0} on {1} to value of \"{2}\" to type {3}",
                    _info.Name,
                    target.GetType(),
                    value == null ? "null" : value.ToString(),
                    _info.PropertyType.FullName);

                throw new Exception(message, e);
            }
        }

        public override bool UseWhen()
        {
            _info = PropertyInfoHelper.GetCaseInsensitivePropertyInfo(TargetType, ColumnName);

            //could not find the property on the object
            return (_info != null);
        }

        public override int ConsiderOrder => ActionOrder.Set.ToInt32();
    }
}
