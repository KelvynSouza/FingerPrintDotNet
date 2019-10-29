using FingerPrint.Core.Controller;
using FingerPrint.Core.Events;
using FingerPrint.Data.Model;
using FingerPrint.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FingerPrint.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainSystem : Window
    {

        private UserModel _user;
        private LoginController _loginController;

        public MainSystem(UserModel user = null)
        {
            _user = user;
            _loginController = new LoginController();
            InitializeComponent();
            InitializeUser();
        }

        public async Task InitializeUser()
        {
            await CheckPermissions();
        }

        public async Task CheckPermissions()
        {
            var permissions = await _loginController.GetPermissions(_user.Id);
            cb_read.Fill = permissions.Read == true ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
            cb_write.Fill = permissions.Write == true ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
            cb_delete.Fill = permissions.Delete == true ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);

        }
    }
}
