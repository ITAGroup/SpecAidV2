using System;
using System.Linq;
using System.Reflection;
using SpecAid.Tooling.Base;
using SpecAid.Tooling.Helper;
using SpecAid.Tooling.SetTranslations;
using SpecAid.Tooling.Translations;
using SpecAid.Tooling.Extensions;

namespace SpecAid.Tooling.ColumnActions
{
    // Action for setting the value in the object nested in other objects
    public class DeepSetAction : ColumnAction, ICreatorColumnAction
    {
        private ICreatorColumnAction _deepAction;
        private PropertyInfo _info;
        private bool _isIndexer;
        private object _lookUp;

        public DeepSetAction(RecallAid recallAid, Translator translator, SetTranslator setTranslator, ColumnActionFactory columnActionFactory, Type targetType, string columnName) : base(recallAid, translator, setTranslator, columnActionFactory, targetType, columnName)
        {
        }

        public void GoGoCreateColumnAction(object target, string tableValue)
        {
            if (tableValue == ConstantStrings.IgnoreCell)
                return;

            var value = GetActual(target);

            // should never happen....
            if (_deepAction != null)
                _deepAction.GoGoCreateColumnAction(value, tableValue);
        }

        private object GetActual(object target)
        {
            if (_isIndexer)
                return _info.GetValue(target, new[] { _lookUp });

            return _info.GetValue(target, null);
        }

        public override bool UseWhen()
        {
            if (!DeepHelper.IsDeep(ColumnName))
                return false;

            var columnNameParts = DeepHelper.SplitColumnName(ColumnName);

            if (!UseWhenProperty(columnNameParts.FirstColumn))
                return false;

            _deepAction = ColumnActionFactory.GetAction<ICreatorColumnAction>(
                _info.PropertyType, columnNameParts.OtherColumns);

            if (_deepAction == null)
                return false;

            return _deepAction.UseWhen();
        }

        private bool UseWhenProperty(string firstColumnName)
        {
            _info = PropertyInfoHelper.GetCaseInsensitivePropertyInfo(
                TargetType, firstColumnName);

            if (_info != null)
                return true;

            _info = PropertyInfoHelper.GetIndexerPropertyInfo(
                TargetType, firstColumnName);

            if (_info == null)
                return false;

            _isIndexer = true;

            var parameterType = _info.GetIndexParameters().First().ParameterType;
            _lookUp = Convert.ChangeType(firstColumnName, parameterType);

            return true;
        }

        public override int ConsiderOrder => ActionOrder.DeepSet.ToInt32();
    }
}
