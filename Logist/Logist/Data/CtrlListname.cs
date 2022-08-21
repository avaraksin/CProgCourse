using Microsoft.EntityFrameworkCore;
using Logist.Interfaces;
using Logist.Common;
namespace Logist.Data
{
    public class CtrlListname : ICtrlListname
    {
        private readonly AppFactory _dbContext;
        //private readonly UserConnectionData? _userConnectionData;
        public string ErrMessage { get; set; }

        public CtrlListname(IDbContextFactory<AppFactory> dbContext)
        {
            _dbContext = dbContext.CreateDbContext();
        }
        
        public List<Listname>? GetListname(int clnum, int idList)
        {
            return _dbContext?.listname?.Where(l => l.clnum == clnum 
                         && l.Idlist == idList && l.IsDel == 0)
                    .OrderBy(l => l.Name)
                    .Include(l => l.user)
                    .ToList();
        }
        //Для добавления новой записи пользователя
        public bool AddListname(Listname listname)
        {
            List<string> lst = new List<string>();
            

            var allListRec = _dbContext.listname.Where(l => l.clnum == listname.clnum && l.Idlist == listname.Idlist).ToList();
            if (allListRec != null && allListRec.Count > 0)
            {
                if (listname.id == 0)
                {
                    lst = allListRec.Where(x => x.IsDel == 0).Select(l => l.Name.ToLower()).ToList();
                }
                else
                {
                    lst = allListRec.Where(l => l.id != listname.id && l.IsDel == 0).Select(l => l.Name.ToLower()).ToList();
                }

                if (lst.Contains(listname.Name.ToLower()))
                {
                    throw new Exception("AlReadyExistException");
                }

            }
            
            // Получаем текущего пользователя
            
            listname.chdate = DateTime.Now;

            if (listname.id == 0) // Новый элемент
            {

                listname.id = (allListRec == null && allListRec.Count == 0) ?
                                1 :
                                allListRec.Select(l => l.id).ToList().Max() + 1;
                _dbContext.listname.Add(listname);
            }
            else
            {
                _dbContext.Entry(listname).State = EntityState.Modified;
                //_dbContext.Entry(_dbContext.listname.FirstOrDefault(x => x.id == listname.id &&
                //                                                        x.Idlist == listname.Idlist &&
                //                                                        x.id2 == listname.id2 &&
                //                                                        x.clnum == listname.clnum)).CurrentValues.SetValues(listname);
            }

            int result = _dbContext.SaveChanges();
            
            return result == 0 ? false : true;
        }
        
        
        //Для удаления записи справочника
        public bool DeleteListname(Listname listname)
        {
            if (listname != null)
            {
                listname.IsDel = 1;
                return AddListname(listname);
            }
            else
            {
                return false;
            }
            
        }
    }
}
