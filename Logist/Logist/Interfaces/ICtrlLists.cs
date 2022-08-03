using Logist.Data;

namespace Logist.Interfaces
{
    public interface ICtrlLists
    {
        public List<Lists> GetLists();
        public void AddLists(Lists _lists);
        public void UpdateLists(Lists _lists);
        public Lists GetLists(int id);
        public void DeleteLists(int id);
    }
}
