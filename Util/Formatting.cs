namespace Util
{
    public static class Formatting
    {
        public static string DecorateWithOrdinalIndicator(long n)
        {
            return n.ToString() + GetOrdinalIndicator(n);
        }
        public static string GetOrdinalIndicator(long n)
        {
            if (n % 100 == 10)
                return "th";

            switch (n % 10)
            {
                case 1:
                    return "st";
                case 2:
                    return "nd";
                case 3:
                    return "rd";
                default:
                    return "th";
            }
        }
    }
}
