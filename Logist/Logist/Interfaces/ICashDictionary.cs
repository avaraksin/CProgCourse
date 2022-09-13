using Logist.Data;
using Logist.Data.Pages;
using Logist.Data.Usr;

namespace Logist.Interfaces
{
    public interface ICashDictionary
    {
        public List<Listname> listDict { get; set; }
        public List<Users> users { get; set; }
        public List<LCustSetting> settings { get; set; }
        public Task GetListname(int clnum);
        public Task GetUsers(int clnum);
        public Task GetSettings(int clnum);
        public void Refresh(int clnum);
    }

    
}
