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

        public static string Format(this string self, params object[] parameters)
        {
            return string.Format(self, parameters);
        }

        public static string SqlLike(this string self)
        {
            if (self == null)
            {
                throw new ArgumentNullException();
            }

            return $"%{self}%";
        }
    }
}
