using System;
using System.Linq;
using System.Reflection;
using SpecAid.Tooling.Extensions;
using SpecAid.Tooling.Helper;

namespace SpecAid.Tooling.Translations
{
    public class DeepLinkTranslation : Translation
    {
        public DeepLinkTranslation(Translator translator, RecallAid recallAid) : base(translator, recallAid)
        {
        }

        public override object Do(PropertyInfo info, string tableValue)
        {
            // get the link from recallaid
            // then traverse the object getting each property until the last one
            // then return it's value

            var splits = tableValue.Split('.').ToList();
            var tag = splits[0];

            if (!RecallAid.ContainsKey(tag))
                throw new Exception("Could not find tag/link: " + tag);

            var recalledValue = RecallAid[tag];

            if (recalledValue == null)
                throw new Exception("Could not find tag/link: " + tableValue);

            object propertyValue = recalledValue;
            string previousPropertyName = tag;

            foreach (var propertyName in splits.Skip(1))
            {
                if (propertyValue == null)
                    throw new Exception($"Property is null \"{previousPropertyName}\" in tag \"{tableValue}\"");

                var targetProperty = PropertyInfoHelper.GetCaseInsensitivePropertyInfo(propertyValue.GetType(), propertyName);

                if (targetProperty == null)
                    throw new Exception($"Could not find property \"{propertyName}\" in tag \"{tableValue}\"");

                propertyValue = targetProperty.GetValue(propertyValue, null);
                previousPropertyName = propertyName;
            }

            return propertyValue;
        }

        public override bool UseWhen(PropertyInfo info, string tableValue)
        {
            // Tags are in the form #TagName or <<TagName>>
            // Tags can also have Deep-Linked properties using the Tag as a starting point
            // Example:
            //   #Dealer001.SalesDistrict.Code
            //   - or -
            //   <<Dealer001>>.SalesDistrict.Code

            var propertyNames = tableValue.Split('.');

            // 2 = object (1) + property (1)
            if (propertyNames.Length < 2)
                return false;

            var tag = propertyNames[0];
            return TagHelper.IsTag(tag);
        }

        public override int ConsiderOrder => TranslationOrder.DeepLink.ToInt32();
    }
}
