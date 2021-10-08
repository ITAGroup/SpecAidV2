using SpecAid.Tooling.Extensions;

namespace SpecAid.Tooling.Helper
{
    public static class ListHelper
    {
        public static string NoBrackets(string theString)
        {
            if (theString.StartsWith("["))
                return theString.TrimAlphaOmega();

            return theString;
        }
    }
}
