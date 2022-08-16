using Logist.Data;

namespace Logist.Interfaces
{
    public interface ICtrlLists
    {
        public List<Lists>? GetLists();
        public Lists? GetLists(int id);
    }
}
