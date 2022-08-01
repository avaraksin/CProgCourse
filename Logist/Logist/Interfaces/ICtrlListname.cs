using Logist.Data;
namespace Logist.Interfaces
{
    public interface ICtrlListname
    {
        public List<Listname>? GetListname();
        public List<Listname> GetListname(int idList);
        public void AddListname(Listname listname);
        public void UpdateListname(Listname listname);
        public Listname GetListname(int idList, int id);
        public void DeleteListname(int idList, int id);
    }
}
