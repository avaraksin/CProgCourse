using Microsoft.EntityFrameworkCore;
using Logist.Interfaces;
using Logist.Common;
namespace Logist.Data
{
    public class CtrlListname : ICtrlListname
    {
        private readonly ApplicationDbContext _dbContext;
        public CtrlListname(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Listname>? GetListname()
        {
            try
            {
                List<Listname> listname = new();
                listname = _dbContext.listname
                    .Where(l => l.clnum == ClNum.clnum && l.IsDel == 0)
                    .OrderBy(l => l.clnum)
                    .OrderBy(l => l.Idlist)
                    .ToList();
                return listname;
            }
            catch
            {
                return null;
            }
        }

        public List<Listname> GetListname(int idList)
        {
            try
            {
                return _dbContext.listname.Where(l => l.clnum == ClNum.clnum && l.Idlist == idList && l.IsDel == 0).ToList();
            }
            catch
            {
                throw;
            }
        }
        //Для добавления новой записи пользователя
        public void AddListname(Listname listname)
        {
            try
            {
                _dbContext.listname.Add(listname);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //Для обновления записи конкретного пользователя
        public void UpdateListname(Listname listname)
        {
            try
            {
                _dbContext.Entry(listname).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //Для получения информации о конкретном пользователе
        public Listname GetListname(int idList, int id)
        {
            try
            {
                Listname? listname = _dbContext.listname.Where(l => l.Idlist ==  idList && l.id == id).ToList()[0];
                if (listname != null)
                {
                    return listname;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }
        //Для удаления записи конкретного пользователя
        public void DeleteListname(int idList, int id)
        {
            try
            {
                Listname? listname = _dbContext.listname.Where(l => l.Idlist ==  idList && l.id == id).ToList()[0];
                if (listname != null)
                {
                    listname.IsDel = 1;
                    UpdateListname(listname);
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
