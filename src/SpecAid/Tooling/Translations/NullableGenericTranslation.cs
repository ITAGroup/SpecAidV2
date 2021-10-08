using System;
using System.Reflection;
using SpecAid.Tooling.Extensions;
using SpecAid.Tooling.Helper;

namespace SpecAid.Tooling.Translations
{
    public class NullableGenericTranslation : Translation
    {
        public NullableGenericTranslation(Translator translator, RecallAid recallAid) : base(translator, recallAid)
        {
        }

        public override object Do(PropertyInfo info, string tableValue)
        {
            if (string.IsNullOrWhiteSpace(tableValue))
                return null;

            var innerType = Nullable.GetUnderlyingType(info.PropertyType);

            try
            {
                return Convert.ChangeType(tableValue, innerType);
            }
            catch
            {
                //If we cannot change to the type explicitly then we'll default to the exception when an implicit conversion is attempted.
                return null;
            }
        }

        public override bool UseWhen(PropertyInfo info, string tableValue)
        {
            var innerType = Nullable.GetUnderlyingType(info.PropertyType);

            return info.PropertyType.IsGenericType && 
                   info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                   TypeHelpers.ImplementsIConvertible(innerType);
        }

        public override int ConsiderOrder => TranslationOrder.NullableGeneric.ToInt32();
    }
}
