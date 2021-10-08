using System;
using System.Reflection;
using SpecAid.Tooling.Extensions;

namespace SpecAid.Tooling.Translations
{
    public class YesterdayTranslation : Translation
    {
        public YesterdayTranslation(Translator translator, RecallAid recallAid) : base(translator, recallAid)
        {
        }

        public override object Do(PropertyInfo info, string tableValue)
        {
            return DateTime.Today.AddDays(-1);  
        }

        public override bool UseWhen(PropertyInfo info, string tableValue)
        {
            return tableValue.Equals("[yesterday]", StringComparison.InvariantCultureIgnoreCase);
        }

        public override int ConsiderOrder => TranslationOrder.Yesterday.ToInt32();
    }
}
