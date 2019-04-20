namespace System
{
    public static class ObjectExtensions
    {
        public static void ArgumentNullCheck(this object self, string name)
        {
            if (self == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
