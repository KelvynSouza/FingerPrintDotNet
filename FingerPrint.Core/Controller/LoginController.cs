using FingerPrint.Core.Events;
using FingerPrint.Data.Model;
using FingerPrint.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Core.Controller
{
    public class LoginController
    {
        private LoginEvent _loginEvent;
        public LoginController()
        {
            _loginEvent = new LoginEvent(new FingerPrintData(), new UserData(), new UserRightsData());
        }

        public async Task<UserModel> LogarWithPasswd(LoginModel login)
        {
            
            return await _loginEvent.Login(login);
        }

        public async Task<UserRightsModel> GetPermissions(int id)
        {
            return await _loginEvent.GetPermissions(id);
        }

    }
}
