namespace SpecAid.Tooling.Extensions
{
    public static class StringExtensions
    {
        public static string TrimAlphaOmega(this string thisValue)
        {
            if (string.IsNullOrEmpty(thisValue))
                return thisValue;

            if (thisValue.Length < 2)
                return thisValue;

            return thisValue.Substring(1, thisValue.Length - 2);
        }
    }
}
