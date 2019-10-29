using FingerPrint.Data.Model;
using FingerPrint.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Core.Events
{
    public class LoginEvent
    {
        private IFingerPrintData _fingerPrintData;
        private IUserData _userData;
        private IUserRightsData _userRights;
        public LoginEvent(IFingerPrintData fingerPrintData, IUserData userData, IUserRightsData userRights)
        {
            _fingerPrintData = fingerPrintData;
            _userData = userData;
            _userRights = userRights;
        }

        public async Task<UserModel> Login(LoginModel login)
        {
            return await Task.Run<UserModel>(async () =>
            {
                try
                {
                    var user = await _userData.GetUser(login.Id, login.Password);

                    return user;
                }
                catch(Exception)
                {
                    return null;
                }
            });
        }

        public async Task<UserRightsModel> GetPermissions(int id)
        {
            return await Task.Run<UserRightsModel>(async () =>
            {
                try
                {
                    var user = await _userRights.Get(id);

                    return user;
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }


    }
}
