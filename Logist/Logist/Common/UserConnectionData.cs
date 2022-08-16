using Logist.Data.Usr;

namespace Logist.Common
{
    public class UserConnectionData
    {
        public event Action? OnChange;

        public int CurrentClNum { get; set; }
        public Users? CurrentUser { get; set; }

        public void UpdateState() => NotifyStateChanged();

        void NotifyStateChanged() => OnChange?.Invoke();
    }
}
