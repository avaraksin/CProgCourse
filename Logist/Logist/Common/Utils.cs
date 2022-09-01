namespace Logist.Common
{
    public static class Utils
    {
        public static string? GetDateString(DateTime? dt)
        {
            return dt?.ToShortDateString();
        }
    }
}
