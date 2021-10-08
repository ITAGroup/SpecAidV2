using System.Reflection;
using SpecAid.Tooling;
using SpecAid.Tooling.Translations;

namespace SpecAid.Tests.Vaporware
{
    public class Informant
    {
        public string Identity { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class InformantTranslation : Translation
    {
        public InformantTranslation(
            Translator translator, RecallAid recallAid)
            : base(translator, recallAid)
        {
        }

        public override object Do(PropertyInfo info, string tableValue)
        {
            var informant = new Informant {Identity = tableValue};
            return informant;
        }

        public override bool UseWhen(PropertyInfo info, string tableValue)
        {
            return (info.PropertyType == typeof(Informant));
        }

        // Tag is 60.  Must be after Tag.
        public override int ConsiderOrder => 120;
    }
}
