using Microsoft.EntityFrameworkCore;
using Logist.Interfaces;
using Logist.Common;
namespace Logist.Data
{
    public class CtrlListname : ICtrlListname
    {
        private readonly ApplicationDbContext _dbContext;

        public string ErrMessage { get; set; }
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
                    .OrderBy(l => l.Name)
                    .ToList();
                return listname;
            }
            catch
            {
                return null;
            }
        }

        public List<Listname>? GetListname(int idList)
        {
            try
            {
                return _dbContext.listname.Where(l => l.clnum == ClNum.clnum && l.Idlist == idList && l.IsDel == 0).ToList();
            }
            catch
            {
                return null;
            }
        }
        //Для добавления новой записи пользователя
        public bool AddListname(Listname listname)
        {
            List<string> lst = new List<string>();
            try
            {
                var listnames = GetListname(listname.Idlist);
                if (listnames != null && listnames.Count > 0)
                {
                    if (listname.id == 0)
                    {
                        lst = listnames.Select(l => l.Name.ToLower()).ToList();
                    }
                    else
                    {
                         lst = listnames.Where(l => l.id != listname.id).Select(l => l.Name.ToLower()).ToList();
                    }

                    if (lst.Contains(listname.Name.ToLower()))
                    {
                        ErrMessage = "Такой элемент уже существует в справочнике!";
                        return false;
                    }
                    
                }
                if (listname.id == 0) // Новый элемент
                {
                    listname.id = (listnames == null && listnames.Count == 0) ?
                                    1 :
                                    listnames.Select(l => l.id).ToList().Max() + 1;
                    _dbContext.listname.Add(listname);
                }
                else
                {
                    //_dbContext.Entry(listname).State = EntityState.Modified;
                    _dbContext.Entry(_dbContext.listname.FirstOrDefault(x => x.id == listname.id &&
                                                                            x.Idlist == listname.Idlist &&
                                                                            x.id2 == listname.id2 &&
                                                                            x.clnum == listname.clnum)).CurrentValues.SetValues(listname);
                }

                int result = _dbContext.SaveChanges();
                if (result == 0)
                {
                    ErrMessage = "Ошибка при сохранении в Базе данных!";
                }
                return result == 0 ? false : true;
            }
            catch
            {
                ErrMessage = "Возникла проблема при соединении с Базой данных.";
                return false;
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
        public async Task<Listname> GetListname(int idList, int id)
        {
            try
            {
                Listname? listname = await _dbContext.listname.FirstOrDefaultAsync(l => l.Idlist ==  idList && l.id == id);
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
