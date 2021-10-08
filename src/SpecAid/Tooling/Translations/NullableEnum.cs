using System;
using System.Reflection;
using SpecAid.Tooling.Extensions;

namespace SpecAid.Tooling.Translations
{
    public class NullableEnumTranslation : Translation
    {
        public NullableEnumTranslation(Translator translator, RecallAid recallAid) : base(translator, recallAid)
        {
        }

        public override object Do(PropertyInfo info, string tableValue)
        {
            if (string.IsNullOrWhiteSpace(tableValue))
                return null;

            var innerType = Nullable.GetUnderlyingType(info.PropertyType);

            return Enum.Parse(innerType, tableValue, true);
        }

        public override bool UseWhen(PropertyInfo info, string tableValue)
        {
            var innerType = Nullable.GetUnderlyingType(info.PropertyType);

            if (innerType == null)
                return false;

            return innerType.IsEnum;
        }

        public override int ConsiderOrder => TranslationOrder.NullableEnum.ToInt32();
    }
}