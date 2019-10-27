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

        public async Task<bool> LogarWithPasswd(LoginModel login)
        {
            LoginEvent loginEvent = new LoginEvent(new FingerPrintData(), new UserData());
            return await loginEvent.Login(login);
        }

    }
}
