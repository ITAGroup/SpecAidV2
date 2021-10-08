using System;
using System.Linq;

namespace SpecAid.Tooling.Helper
{
    public static class ObjectHelper
    {
        public static T CreateObjectWithTheDefaultConstructor<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        internal static bool ThisTypeHasADefaultConstructor<T>()
        {
            return typeof (T).GetConstructors().Any(c => c.GetParameters().Length == 0);
        }
    }
}
