using Microsoft.EntityFrameworkCore;
using Logist.Interfaces;
using Logist.Common;
using Logist.PageHelp;

namespace Logist.Data
{
    public class CtrlLists : ICtrlLists
    {
        private readonly AppFactory? _dbContext;
        private  int clnum;
        private readonly UserConnectionData? _userConnectionData;
        public CtrlLists(IDbContextFactory<AppFactory> dbContext, UserConnectionData userConnectionData)
        {
            _userConnectionData = userConnectionData;
            _dbContext = dbContext.CreateDbContext();
        }
       
       
        /// <summary>
        /// Получение списка справочников
        /// </summary>
        public List<Lists>? GetLists()
        {
            _userConnectionData?.UpdateState();
            return  _dbContext?.lists?.Where(l => l.clnum ==_userConnectionData.CurrentClNum).ToList();
        }

        public Lists? GetLists(int id)
        {
            _userConnectionData?.UpdateState();
            return _dbContext?.lists?.FirstOrDefault(x => x.id == id && x.clnum == _userConnectionData.CurrentClNum);
        }
    }
}
