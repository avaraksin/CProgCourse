using Logist.Data;
namespace Logist.Interfaces
{
    public interface ICtrlListname
    {
        public string ErrMessage { get; set; }
        
        public List<Listname> GetListname(int clnum, int idList);
        public bool AddListname(Listname listname);
        public bool DeleteListname(Listname listname);
    }
}
