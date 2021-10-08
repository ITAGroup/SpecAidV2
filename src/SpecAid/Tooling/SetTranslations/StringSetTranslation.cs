using System;
using System.Reflection;
using SpecAid.Base;
using SpecAid.Tooling.Extensions;

namespace SpecAid.Tooling.SetTranslations
{
    public class StringSetTranslation : ISetTranslation
    {
        public object Do(PropertyInfo info, object target, object newItem)
        {
            return newItem.ToString();
        }

        public bool UseWhen(PropertyInfo info, object target, object newItem)
        {
            if (newItem == null)
                return false;

            if (!(info.PropertyType == typeof (String)))
                return false;

            return true;
        }

        public int ConsiderOrder => SetTranslationOrder.String.ToInt32();
    }
}
