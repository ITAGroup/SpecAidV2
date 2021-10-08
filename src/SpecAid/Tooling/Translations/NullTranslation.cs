using System;
using System.Reflection;
using SpecAid.Tooling.Extensions;

namespace SpecAid.Tooling.Translations
{
    //Used for symbollic links
    public class NullTranslation : Translation
    {
        public NullTranslation(Translator translator, RecallAid recallAid) : base(translator, recallAid)
        {
        }

        public override object Do(PropertyInfo info, string tableValue)
        {
            return null;
        }

        public override bool UseWhen(PropertyInfo info, string tableValue)
        {
            return tableValue.Equals("null", StringComparison.InvariantCultureIgnoreCase);
        }
        public override int ConsiderOrder => TranslationOrder.Null.ToInt32();
    }
}
