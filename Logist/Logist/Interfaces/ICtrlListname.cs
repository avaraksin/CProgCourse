using Logist.Data;
namespace Logist.Interfaces
{
    public interface ICtrlListname
    {
        public string ErrMessage { get; set; }
        public List<Listname>? GetListname();
        public List<Listname> GetListname(int idList);
        public bool AddListname(Listname listname);
        public bool UpdateListname(Listname listname);
        public Task<Listname> GetListname(int idList, int id);
        public void DeleteListname(Listname listname);
    }
}
