using System;
using System.Reflection;
using SpecAid.Tooling.Base;

namespace SpecAid.Tooling.Helper
{
    public static class InstantProperty
    {
        public static PropertyInfo FromType(Type type)
        {
            Type customInstanceType = typeof(ObjectField<>).MakeGenericType(type);
            var instance = Activator.CreateInstance(customInstanceType);
            var propertyInfo = instance.GetType().GetProperty("Field");

            return propertyInfo;
        }
    }
}
