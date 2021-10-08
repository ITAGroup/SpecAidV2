using System.Reflection;
using SpecAid.Tooling.Extensions;

namespace SpecAid.Tooling.Translations
{
    public class BooleanTranslation : Translation
    {
        public BooleanTranslation(Translator translator, RecallAid recallAid) : base(translator, recallAid)
        {
        }

        public override object Do(PropertyInfo info, string tableValue)
        {
            return bool.Parse(tableValue);
        }

        public override bool UseWhen(PropertyInfo info, string tableValue)
        {
            return info.PropertyType == typeof(bool);
        }

        public override int ConsiderOrder => TranslationOrder.Boolean.ToInt32();
    }
}
