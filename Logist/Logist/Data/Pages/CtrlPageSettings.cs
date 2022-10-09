using Logist.Interfaces;
using Logist.PageHelp;
using MudBlazor;

namespace Logist.Data.Pages
{
    public class CtrlPageSettings
    {
        private readonly IDialogService _dialogService;

        public CtrlPageSettings(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public bool ShowFieldSettingsDialog(int clnum)
        {
            var dialog = _dialogService.Show<LCustParam>();
            var result = dialog.Result;
            return true;
        }
    }
}
