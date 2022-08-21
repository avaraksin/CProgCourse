using Logist.Data;

namespace Logist.Interfaces
{
    public interface ICtrlLists
    {
        public List<Lists>? GetLists(int clnum);
        public Lists? GetLists(int clnum, int id);
    }
}
