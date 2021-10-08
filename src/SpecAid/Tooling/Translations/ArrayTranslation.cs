using System;
using System.Reflection;
using SpecAid.Tooling.Base;
using SpecAid.Tooling.Extensions;
using SpecAid.Tooling.Helper;

namespace SpecAid.Tooling.Translations
{
    public class ArrayTranslation : Translation
    {
        public ArrayTranslation(Translator translator, RecallAid recallAid) : base(translator, recallAid)
        {
        }

        public override object Do(PropertyInfo info, string tableValue)
        {
            var tableValueNoBrackets = ListHelper.NoBrackets(tableValue);

            var tableValueEntries = CsvHelper.Split(tableValueNoBrackets);
            
            var numEntries = tableValueEntries.Count;
            var baseType = info.PropertyType.GetElementType();

            var array = Array.CreateInstance(baseType, numEntries);

            var propertyInfo = InstantProperty.FromType(baseType);

            for (int i = 0; i < numEntries; i++)
            {
                var expectedValue = Translator.Translate(propertyInfo, tableValueEntries[i]);
                array.SetValue(expectedValue,i);
            }

            return array;
        }

        public override bool UseWhen(PropertyInfo info, string tableValue)
        {
            if (info.PropertyType.IsArray)
                return true;

            return false;
        }

        public override int ConsiderOrder => TranslationOrder.Array.ToInt32();
    }
}
