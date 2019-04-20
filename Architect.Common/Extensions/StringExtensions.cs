namespace System
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string self)
        {
            return string.IsNullOrEmpty(self);
        }

        public static bool IsNotNullOrEmpty(this string self)
        {
            return !string.IsNullOrEmpty(self);
        }

        public static bool IsDifferent(this string self, string toCompare)
        {
            return self != toCompare;
        }
    }
}
