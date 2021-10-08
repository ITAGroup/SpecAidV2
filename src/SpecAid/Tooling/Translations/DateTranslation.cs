using System;
using System.Reflection;
using System.Text.RegularExpressions;
using SpecAid.Tooling.Extensions;

namespace SpecAid.Tooling.Translations
{
    public class DateTranslation : Translation
    {
        public DateTranslation(Translator translator, RecallAid recallAid) : base(translator, recallAid)
        {
        }

        public override object Do(PropertyInfo info, string tableValue)
        {
            var myDatetimeAsString = tableValue.Substring(1,10);
            return DateTime.ParseExact(myDatetimeAsString, "yyyy-MM-dd", null);
        }

        public override bool UseWhen(PropertyInfo info, string tableValue)
        {
            var r = new Regex(@"\[\d\d\d\d-\d\d-\d\d\]");
            return r.IsMatch(tableValue);
        }

        public override int ConsiderOrder => TranslationOrder.Date.ToInt32();
    }
}
