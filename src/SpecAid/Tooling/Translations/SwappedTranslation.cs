using System.Reflection;
using SpecAid.Tooling;
using SpecAid.Tooling.Extensions;
using SpecAid.Tooling.Helper;
using SpecAid.Tooling.Translations;

namespace SpecAid.Translations
{
    public class SwappedTranslation : Translation
    {
        public SwappedTranslation(Translator translator, RecallAid recallAid) : base(translator, recallAid)
        {
        }

        public override object Do(PropertyInfo info, string tableValue)
        {
            var tableValueSwapped = StringSwapper.Swap(Translator, tableValue);

            if (tableValueSwapped != tableValue)
            {
                // new text = new translation
                var item = Translator.Translate(info, tableValueSwapped);
                return item;
            }
            else
            {
                // if the swap doesn't do anything don't retranslate... 
                // but see if something else wants to do work.
                var item = Translator.TranslateContinueAfterOperation(info, tableValue, TranslationOrder.Swapped.ToInt32());
                return item;
            }
        }

        public override bool UseWhen(PropertyInfo info, string tableValue)
        {
            return tableValue.Contains("{") && tableValue.Contains("}");
        }

        public override int ConsiderOrder => TranslationOrder.Swapped.ToInt32();
    }
}
