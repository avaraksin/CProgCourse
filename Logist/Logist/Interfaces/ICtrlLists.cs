using Logist.Data;

namespace Logist.Interfaces
{
    public interface ICtrlLists
    {
        public List<lists> GetLists();
        public void AddLists(lists _lists);
        public void UpdateLists(lists _lists);
        public lists GetLists(int id);
        public void DeleteLists(int id);
    }
}
