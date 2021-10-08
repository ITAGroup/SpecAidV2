using System;
using System.Reflection;
using SpecAid.Tooling.Extensions;

namespace SpecAid.Tooling.Translations
{
    public class TomorrowTranslation : Translation
    {
        public TomorrowTranslation(Translator translator, RecallAid recallAid) : base(translator, recallAid)
        {
        }

        public override object Do(PropertyInfo info, string tableValue)
        {
            return DateTime.Today.AddDays(1);  
        }

        public override bool UseWhen(PropertyInfo info, string tableValue)
        {
            
            return tableValue.Equals("[tomorrow]", StringComparison.InvariantCultureIgnoreCase);
        }

        public override int ConsiderOrder => TranslationOrder.Tomorrow.ToInt32();
    }
}
