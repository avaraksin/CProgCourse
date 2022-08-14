namespace Logist.Common
{
    public static class ClNum
    {
        public static int clnum = 1;
    }

    public class AppStatus
    {
        public event Action OnChange;

        public  int clnum { get; set; }
        
        public  void UpdateState() => NotifyStateChanged();

         void NotifyStateChanged() => OnChange?.Invoke();
    }
}
