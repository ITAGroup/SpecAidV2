using System.Reflection;
using SpecAid.Tooling.Extensions;

namespace SpecAid.Tooling.Translations
{
    public class QuotedStringTranslation : Translation
    {
        public QuotedStringTranslation(Translator translator, RecallAid recallAid) : base(translator, recallAid)
        {
        }

        public override object Do(PropertyInfo info, string tableValue)
        {
            // When using quotes we want the raw values.
            // no translation
            // no [datetime] alter.
            // no {#tag} alter.

            var trimmedString = tableValue.TrimAlphaOmega();

            return trimmedString;
        }

        public override bool UseWhen(PropertyInfo info, string tableValue)
        {
            return tableValue.StartsWith("\"") && tableValue.EndsWith("\"");
        }

        // I am the ultimate override
        public override int ConsiderOrder => TranslationOrder.QuotedString.ToInt32();
    }
}
