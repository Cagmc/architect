namespace System
{
    public static class IntegerExtensions
    {
        public static int CountPages(this int self, int? pageSize)
        {
            int count = 1;
            if (pageSize.HasValue)
            {
                count = (self / pageSize.Value) + 1;
            }

            return count;
        }
    }
}
