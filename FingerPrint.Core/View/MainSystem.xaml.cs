using FingerPrint.Controller;
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
        private UserRightsModel _permissions;

        private LoginController _loginController;
        private DataController _dataController;

        public MainSystem(UserModel user)
        {
            _user = user;
            _loginController = new LoginController();
            _dataController = new DataController();

            InitializeComponent();

        }

        public async Task InitializeSystem()
        {
            GetUserInfo();
            await CheckPermissions();
            await GetData();
        }

        private async Task CheckPermissions()
        {
            _permissions = await _loginController.GetPermissions(_user.Id);
            cb_read.Fill = _permissions.Read == true ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
            cb_write.Fill = _permissions.Write == true ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);
            cb_delete.Fill = _permissions.Delete == true ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.White);

        }
        private async Task GetData()
        {
            dataGrid.ItemsSource = await _dataController.GetAllUsers();
        }

        private void GetUserInfo()
        {
            tb_firstName.Text += _user.FirstName;
            tb_lastName.Text += _user.LastName;
            tb_job.Text += _user.JobName;
            tb_birthDate.Text += _user.BirthDate.ToString("dd/MM/yyyy");


        }

        private void btn_addUser_Click(object sender, RoutedEventArgs e)
        {
            new AddUser(this).Show();
            this.Hide();
        }
    }
}
