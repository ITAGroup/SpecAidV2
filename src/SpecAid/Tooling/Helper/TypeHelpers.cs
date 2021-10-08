using System;

namespace SpecAid.Tooling.Helper
{
    public class TypeHelpers
    {
        internal static bool ImplementsIConvertible(Type t)
        {
            return t.GetInterface(nameof(IConvertible)) != null;
        }
    }
}
