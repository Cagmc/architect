namespace System
{
    public static class ObjectExtensions
    {
        public static T ArgumentNullCheck<T>(this T self, string name)
            where T: class
        {
            if (self == null)
            {
                throw new ArgumentNullException(name);
            }

            return self;
        }
    }
}
