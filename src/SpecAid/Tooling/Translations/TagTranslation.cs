using System;
using System.Reflection;
using SpecAid.Tooling.Extensions;
using SpecAid.Tooling.Helper;

namespace SpecAid.Tooling.Translations
{
    // Used for symbolic links
    public class TagTranslation : Translation
    {
        public TagTranslation(Translator translator, RecallAid recallAid) : base(translator, recallAid)
        {
        }

        public override object Do(PropertyInfo info, string tableValue)
        {
            if (!RecallAid.ContainsKey(tableValue))
            {
                throw new Exception("Could not find tag: " + tableValue);
            }

            return RecallAid[tableValue];  
        }

        public override bool UseWhen(PropertyInfo info, string tableValue)
        {
            return TagHelper.IsTag(tableValue);
        }

        // After String
        public override int ConsiderOrder => TranslationOrder.Tag.ToInt32();
    }
}
