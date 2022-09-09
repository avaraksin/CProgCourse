namespace Logist.Settings
{
    public static class UserSettings
    {
        public static string? ClientIP { get; set; }

        public static List<string> privLevel = new List<string>() { "Admin", "User", "Observer" };

    }
}
