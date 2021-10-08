using System;
using System.Reflection;
using SpecAid.Tooling.Extensions;

namespace SpecAid.Tooling.Translations
{
    public class DateTimeTranslation : Translation
    {
        public DateTimeTranslation(Translator translator, RecallAid recallAid) : base(translator, recallAid)
        {
        }

        public override object Do(PropertyInfo info, string tableValue)
        {
            return DateTime.Parse(tableValue);
        }

        public override bool UseWhen(PropertyInfo info, string tableValue)
        {
            return info.PropertyType == typeof(DateTime);
        }

        public override int ConsiderOrder => TranslationOrder.DateTime.ToInt32();
    }
}
