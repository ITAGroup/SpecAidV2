using System;
using System.Reflection;
using SpecAid.Tooling.Extensions;

namespace SpecAid.Tooling.Translations
{
    public class GuidTranslation : Translation
    {
        public GuidTranslation(Translator translator, RecallAid recallAid) : base(translator, recallAid)
        {
        }

        public override object Do(PropertyInfo info, string tableValue)
        {
            return new Guid(tableValue);
        }

        public override bool UseWhen(PropertyInfo info, string tableValue)
        {
            if (info.PropertyType == typeof (Guid))
                return true;
            if (info.PropertyType == typeof(Guid?))
                return true;
            return false;
        }

        public override int ConsiderOrder => TranslationOrder.Guid.ToInt32();
    }
}
