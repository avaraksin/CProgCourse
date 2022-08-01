using Microsoft.EntityFrameworkCore;
using Logist.Interfaces;
using Logist.Common;

namespace Logist.Data
{
    public class CtrlLists : ICtrlLists
    {
        private readonly ApplicationDbContext _dbContext;
        public CtrlLists(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<lists> GetLists()
        {
            try
            {
                return _dbContext.lists.Where(l => l.clnum == ClNum.clnum).OrderBy(l => l.clnum).OrderBy(l => l.id).ToList();
                //return _dbContext.lists.Where(l => l.clnum == ClNum.clnum).ToList();
            }
            catch
            {
                throw;
            }
        }

         //Для добавления новой записи пользователя
        public void AddLists(lists _lists)
        {
            try
            {
                _dbContext.lists.Add(_lists);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //Для обновления записи конкретного пользователя
        public void UpdateLists(lists _lists)
        {
            try
            {
                _dbContext.Entry(_lists).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        //Для получения информации о конкретном пользователе
        public lists GetLists(int id)
        {
            try
            {
                lists? _lists = _dbContext.lists.Where(l => l.clnum == ClNum.clnum && l.id == id).ToList()[0];
                if (_lists != null)
                {
                    return _lists;
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
        public void DeleteLists(int id)
        {
            try
            {
                lists? _lists = _dbContext.lists.Where(l => l.clnum == ClNum.clnum && l.id == id).ToList()[0];
                if (_lists != null)
                {
                    _dbContext.Remove(_lists);
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
