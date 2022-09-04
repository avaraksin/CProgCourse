using Microsoft.EntityFrameworkCore;
using Logist.Interfaces;
using Logist.Common;
using Logist.PageHelp;

namespace Logist.Data
{
    public class CtrlLists : ICtrlLists
    {
        private readonly AppFactory? _dbContext;
        public CtrlLists(IDbContextFactory<AppFactory> dbContext)
        {
            //_userConnectionData = userConnectionData;
            _dbContext = dbContext.CreateDbContext();
        }
       
       
        /// <summary>
        /// Получение списка справочников
        /// </summary>
        public List<Lists>? GetLists(int clnum)
        {
            return  _dbContext?.lists?.Where(l => l.clnum == clnum).ToList();
        }

        public Lists? GetLists(int clnum, int id)
        {
            return _dbContext?.lists?.FirstOrDefault(x => x.id == id && x.clnum == clnum);
        }
    }
}
