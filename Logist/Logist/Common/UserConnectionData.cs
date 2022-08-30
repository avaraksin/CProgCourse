using Logist.Data;
using Logist.Data.Usr;
using Logist.Interfaces;

namespace Logist.Common
{
    public enum FormOnDisplay
    {
        FormMain        = 0,
        FormListname    = 1
    }
    public class UserConnectionData
    {

        private readonly ICtrlListname? _ctrlListname; 
        
        public UserConnectionData(ICtrlListname ctrlListname)
        {
            _ctrlListname = ctrlListname;
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

            return false;
        }
    }
}
