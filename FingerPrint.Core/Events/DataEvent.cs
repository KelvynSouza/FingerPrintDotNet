using FingerPrint.Data.Model;
using FingerPrint.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Events
{
    public class DataEvent
    {
        private IFingerPrintData _fingerPrintData;
        private IUserData _userData;
        private IUserRightsData _userRights;
        public DataEvent(IFingerPrintData fingerPrintData, IUserData userData, IUserRightsData userRights)
        {
            _fingerPrintData = fingerPrintData;
            _userData = userData;
            _userRights = userRights;
        }

        public async Task<ICollection<UserModel>> GetAllUser()
        {
            return await Task.Run<ICollection<UserModel>>(async () =>
            {
                try
                {
                    var users = await _userData.GetAll();

                    return users;
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }

    }
}
