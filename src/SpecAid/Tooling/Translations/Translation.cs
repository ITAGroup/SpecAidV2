using System.Reflection;
using SpecAid.Tooling.Base;

namespace SpecAid.Tooling.Translations
{
    public abstract class Translation : ITranslation
    {
        // For Array
        protected Translator Translator;

        // For Tag
        protected RecallAid RecallAid;

        protected Translation(
            Translator translator,
            RecallAid recallAid)
        {
            Translator = translator;
            RecallAid = recallAid;
        }

        public abstract int ConsiderOrder { get; }
        public abstract object Do(PropertyInfo info, string tableValue);
        public abstract bool UseWhen(PropertyInfo info, string tableValue);
    }
}
