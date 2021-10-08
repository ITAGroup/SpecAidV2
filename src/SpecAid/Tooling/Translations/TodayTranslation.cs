using System;
using System.Reflection;
using SpecAid.Tooling.Extensions;

namespace SpecAid.Tooling.Translations
{
    public class TodayTranslation : Translation
    {
        public TodayTranslation(Translator translator, RecallAid recallAid) : base(translator, recallAid)
        {
        }

        public override object Do(PropertyInfo info, string tableValue)
        {
            return DateTime.Today;  
        }

        public override bool UseWhen(PropertyInfo info, string tableValue)
        {
            return tableValue.Equals("[today]", StringComparison.InvariantCultureIgnoreCase);
        }

        public override int ConsiderOrder => TranslationOrder.Today.ToInt32();
    }
}
