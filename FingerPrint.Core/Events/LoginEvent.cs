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
        public LoginEvent(IFingerPrintData fingerPrintData, IUserData userData)
        {
            _fingerPrintData = fingerPrintData;
            _userData = userData;
        }

        public async Task<bool> Login(LoginModel login)
        {
            return await Task.Run<bool>(async () =>
            {
                try
                {
                    var user = await _userData.GetUser(login.Id, login.Password);
                    if (user is null)
                        return false;
                    else
                        return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            });
        }
    }
}
