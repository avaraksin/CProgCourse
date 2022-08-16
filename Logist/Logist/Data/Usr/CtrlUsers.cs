using Logist.Common;
using Microsoft.EntityFrameworkCore;
namespace Logist.Data.Usr
{
    public class CtrlUsers
    {
        private AppFactory _context;
        public string ErrMessage { get; set; }
        public CtrlUsers(IDbContextFactory<AppFactory> dbContext)
        {
            _context = dbContext.CreateDbContext();
        }
        public List<Users>? GetUsers()
        {
            List<Users>? usr = null;
            try
            {
                usr = _context.users.Where(u => u.clnum == ClNum.clnum && u.isdel == 0)
                    .OrderBy(u => u.id)
                    .ToList();
                if (usr == null)
                {
                    usr = new List<Users>();
                }
            }
            catch(Exception ex) {
                ErrMessage = "Возникла проблема при соединении с Базой данных.\n" +
                    ex.Message;
            }

            return usr;
        }

        public Users? GetUsers(int id)
        {
            Users? usr = null;
            try
            {
                usr = _context.users.FirstOrDefault(u => u.clnum == ClNum.clnum && u.id == id && u.isdel == 0);
                if (usr == null)
                {
                    usr = new Users();
                }
            }
            catch(Exception ex) {
                ErrMessage = "Возникла проблема при соединении с Базой данных.\n" +
                    ex.Message;
            }

            return usr;
        }

        //public bool AddUsers(Users users)
        //{
        //    try
        //    {
        //        List<Users> usr = _context.users.Where(u => u.clnum == users.clnum).ToList();
        //        if (users.id == -1)
        //        {
        //            users.id = usr.Max(u => u.id) + 1;

        //        }
        //    }
        //}
    }
}
