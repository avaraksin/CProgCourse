using Microsoft.EntityFrameworkCore;
using Logist.Interfaces;
using Logist.PageHelp;

using MudBlazor;
using MudBlazor.Dialog;
namespace Logist.Data
{
    public class CtrlListname : ICtrlListname
    {
        private readonly AppFactory _dbContext;
        private readonly IDialogService _dialogService;
        private readonly ISnackbar Snackbar;
        //private readonly UserConnectionData? _userConnectionData;
        public string ErrMessage { get; set; }

        public CtrlListname(IDbContextFactory<AppFactory> dbContext, IDialogService dialogService, ISnackbar snackbar)
        {
            _dbContext = dbContext.CreateDbContext();
            _dialogService = dialogService;
            Snackbar = snackbar;
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
            
            // Получаем текущую дату
            
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

        public async Task<bool> CorrectElement(Listname correctItem)
        {
            DialogOptions closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true };
            DialogParameters param = new DialogParameters();

            param.Add("Item", correctItem);
            var dialog = _dialogService.Show<Dialog>(correctItem.id == 0 ? "Новый элемент" : "Редактирование справочников", param, closeOnEscapeKey);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {

                var changeResult = AddListname(correctItem);

                if (changeResult)
                {
                    Snackbar.Add("Данные успешно сохранены!", Severity.Success);
                    return true;
                }

                Snackbar.Add("При сохранении данных возникла ошибка:\n" + ErrMessage, Severity.Error);
                return false;
            }
            return false;
        }

        public async Task<bool> DeleteItem(Listname Item)
        {
            DialogOptions closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true };
            DialogParameters param = new DialogParameters();

            param.Add("name", Item.Name);

            var dialog = _dialogService.Show<DelDialog>("Удаление элемента справочника", param, closeOnEscapeKey);
            var result = await dialog.Result;

            if (!result.Cancelled)
            {
                var delResult = DeleteListname(Item);

                if (delResult)
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
