using FingerPrint.Data.Model;
using FingerPrint.Data.Persistence;
using FingerPrint.Events;
using FingerPrint.Model;
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

        #region Add
        public async Task<int> AddUser(UserCompleteDataModel user)
        {
            return await _dataEvent.AddUser(user);
        }
        public async Task<bool> AddFingerprint(FingerprintModel user)
        {
            return await _dataEvent.AddFinger(user);
        }
        #endregion


        #region Edit
        public async Task<bool> EditUser(UserCompleteDataModel user)
        {
            return await _dataEvent.EditUserInfo(user);
        }

        #endregion

        #region Get
        public async Task<ICollection<UserModel>> GetAllUsers()
        {
            return await _dataEvent.GetAllUser();
        }

        public async Task<UserCompleteDataModel> GetUserInfo(int id)
        {
            return await _dataEvent.GetUserInfo(id);
        }

        #endregion

        #region Delete
        public async Task<bool> DeleteFingerPrint(int id)
        {
            return await _dataEvent.DeleteFingerprint(id);
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _dataEvent.DeleteUser(id);
        }
        #endregion





    }
}
