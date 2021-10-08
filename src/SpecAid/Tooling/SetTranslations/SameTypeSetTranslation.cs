using System.Reflection;
using SpecAid.Base;
using SpecAid.Tooling.Extensions;

namespace SpecAid.Tooling.SetTranslations
{
    public class SameTypeSetTranslation : ISetTranslation
    {
        public object Do(PropertyInfo info, object target, object newItem)
        {
            return newItem;
        }

        public bool UseWhen(PropertyInfo info, object target, object newItem)
        {
            return info.PropertyType.IsInstanceOfType(newItem);
        }

        public int ConsiderOrder => SetTranslationOrder.SameType.ToInt32();
    }
}
