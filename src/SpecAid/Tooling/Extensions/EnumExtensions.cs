using System;

namespace SpecAid.Tooling.Extensions
{
    public static class EnumExtensions
    {
        public static int ToInt32(this Enum enumValue)
        {
            return Convert.ToInt32(enumValue);
        }
    }
}
