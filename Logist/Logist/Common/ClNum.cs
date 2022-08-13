namespace Logist.Common
{
    public static class ClNum
    {
        public static int clnum = 1;
    }

    public static class AppStatus
    {
        public static event Action OnChange;
        
        public static void UpdateState() => NotifyStateChanged();

        static void NotifyStateChanged() => OnChange?.Invoke();
    }
}
