using FingerPrint.Data.Model;
using FingerPrint.Data.Persistence;
using FingerPrint.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Controller
{
    public class DataController
    {

        private DataEvent _dataEvent;
        public DataController()
        {
            _dataEvent = new DataEvent(new FingerPrintData(), new UserData(), new UserRightsData());
        }

        public async Task<ICollection<UserModel>> GetAllUsers()
        {
            return await _dataEvent.GetAllUser();
        }

    }
}
