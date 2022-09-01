using Logist.Common;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using Logist.PageHelp;

namespace Logist.Data.Usr
{
    public class CtrlUsers
    {
        private AppFactory _context;
        public string? ErrMessage { get; set; }

        private readonly IDialogService _dialogService;
        private readonly ISnackbar Snackbar;
        

        public CtrlUsers(IDbContextFactory<AppFactory> dbContext, IDialogService dialogService, ISnackbar snackbar)
        {
            _context = dbContext.CreateDbContext();
            _dialogService = dialogService;
            Snackbar = snackbar;
        }
        public List<Users>? GetUsers(int clnum)
        {
            List<Users>? usr = null;
            try
            {
                usr = _context.users.Where(u => u.clnum == clnum && u.isdel == 0)
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

        public Users? GetUsers(int clnum, int id)
        {
            Users? usr = null;
            try
            {
                usr = _context.users.FirstOrDefault(u => u.clnum == clnum && u.id == id && u.isdel == 0);
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

        public async Task<bool> UpdateUsers(Users users)
        {
            if (users == null) return false;

            try
            {
                List<Users> usr = _context.users.Where(u => u.clnum == users.clnum).ToList();
                if (users.isdel == -1)
                {
                    if (usr.Select(u => u.id).ToList().Contains(users.id))
                    {
                        ErrMessage = "Пользователь с таким id уже существует!";
                        return false;
                    }

                    if (usr.Where(x => x.isdel == 0).Select(x => x.Shortname.ToLower()).ToList().Contains(users.Shortname.ToLower()))
                    {
                        ErrMessage = "Пользователь с таким именем уже существут!";
                        return false;
                    }
                    users.CrDate = DateTime.Now;
                }
                else
                {
                    if (usr.Where(x => x.id != users.id && x.isdel == 0).Select(x => x.Shortname.ToLower()).ToList().Contains(users.Shortname.ToLower()))
                    {
                        ErrMessage = "Пользователь с таким именем уже существут!";
                        return false;
                    }
                }
                
                users.chdate = DateTime.Now;


                if (users.isdel == -1)
                {
                    users.isdel = 0;
                    _context.users.Add(users);
                }
                else
                {
                    _context.Entry(users).State = EntityState.Modified;
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                ErrMessage = "Ошибка при обновлении пользователя.\n" + ex.Message;
            }

            return false;
        }

        public async Task<bool> DeleteUser(Users user)
        {
            if (user == null) return false;

            user.isdel = 1;

            return await UpdateUsers(user);
        }

        public async Task<bool> CorrectElement(Users correctItem)
        {
            DialogOptions closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true };
            DialogParameters param = new DialogParameters();

            param.Add("Item", correctItem);
            var dialog = _dialogService.Show<DialogUser>(correctItem.id == 0 ? "Новый пользователь" : "Редактирование пользователя", param, closeOnEscapeKey);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {

                if (await UpdateUsers(correctItem))
                {
                    Snackbar.Add("Данные успешно сохранены!", Severity.Success);
                    return true;
                }

                Snackbar.Add("При сохранении данных возникла ошибка:\n" + ErrMessage, Severity.Error);
                return false;
            }
            return false;
        }

        public async Task<bool> DeleteItem(Users Item)
        {
            DialogOptions closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true };
            DialogParameters param = new DialogParameters();

            param.Add("name", Item.name);

            var dialog = _dialogService.Show<DelDialog>("Удаление пользователя", param, closeOnEscapeKey);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                if (await DeleteUser(Item))
                {
                    Snackbar.Add("Данные успешно удалены!", Severity.Success);
                    await Task.CompletedTask;
                    return true;
                }

                Snackbar.Add("При удалении данных возникла ошибка:\n" + ErrMessage, Severity.Error);
                await Task.CompletedTask;
                return false;
            }

            return false;
        }
    }
}
