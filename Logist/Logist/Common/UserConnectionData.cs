using Logist.Data;
using Logist.Data.Usr;
using Logist.Data.Pages;
using Logist.Interfaces;

namespace Logist.Common
{
    public enum FormOnDisplay
    {
        FormMain            = 0,
        FormListname        = 1,
        FormUsers           = 2,
        FormLCust           = 3,
        FormFieldSettings   = 4
    }

    public class UserConnectionData
    {

        private readonly ICtrlListname? _ctrlListname; 
        private readonly CtrlUsers? _ctrlUsers;
        private readonly CtrlPageSettings _cash;
        
        public UserConnectionData(ICtrlListname ctrlListname, CtrlUsers ctrlUsers, CtrlPageSettings cashDictionary)
        {
            _ctrlListname = ctrlListname;
            _ctrlUsers = ctrlUsers;
            _cash = cashDictionary;
        }

        public async Task<bool>? CorrectElement(object correctItem)
        {
            if (correctItem == null)
            {
                return false;
            }

            if (correctItem.GetType() == typeof(Listname))
            {
                return await _ctrlListname?.CorrectElement((Listname)correctItem);
            }

            if (correctItem.GetType() == typeof(Users))
            {
                return await _ctrlUsers?.CorrectElement((Users)correctItem);
            }

            return false;
        }

        public async Task<bool>? DeleteElement(object Item)
        {
            if (Item == null)
            {
                return false;
            }

            if (Item.GetType() == typeof(Listname))
            {
                return await _ctrlListname?.DeleteItem((Listname)Item);
            }
            else if (Item.GetType() == typeof(Users))
            {
                return await _ctrlUsers?.DeleteItem((Users)Item);
            }
            return false;
        }

        public async Task<bool>? FieldsSettings(object Item)
        {
            return _cash.ShowFieldSettingsDialog(0);
        }
    }
}
