using System;
using System.Linq;
using System.Reflection;
using SpecAid.Tooling.Base;
using SpecAid.Tooling.Extensions;
using SpecAid.Tooling.Helper;
using SpecAid.Tooling.SetTranslations;
using SpecAid.Tooling.Translations;

namespace SpecAid.Tooling.ColumnActions
{
    public class IndexerSetAction : ColumnAction, ICreatorColumnAction
    {
        private PropertyInfo _info;
        private object _lookUp;


        public IndexerSetAction(
            RecallAid recallAid, Translator translator,
            SetTranslator setTranslator, ColumnActionFactory columnActionFactory,
            Type targetType, string columnName)
            : base(
                recallAid, translator, setTranslator, columnActionFactory,
                targetType, columnName)
        {
        }

        public void GoGoCreateColumnAction(object target, string tableValue)
        {
            if (tableValue == ConstantStrings.IgnoreCell)
                return;

            var value = Translator.Translate(_info, tableValue);
            value = SetTranslator.Translate(_info, target, value);

            try
            {
                _info.SetValue(target, value, new object[] { _lookUp });
            }
            catch (ArgumentException e)
            {
                var message = string.Format(
                    "Unable to set value of indexer on {0} to value of \"{1}\" to type {2}",
                    target.GetType(),
                    value == null ? "null" : value.ToString(),
                    _info.PropertyType.FullName);

                throw new Exception(message, e);
            }
        }

        public override bool UseWhen()
        {
            _info = PropertyInfoHelper.GetIndexerPropertyInfo(TargetType, ColumnName);

            if (_info == null)
                return false;

            var parameterType = _info.GetIndexParameters().First().ParameterType;
            _lookUp = Convert.ChangeType(ColumnName, parameterType);

            return true;
        }

        public override int ConsiderOrder => ActionOrder.IndexerSet.ToInt32();
    }
}
