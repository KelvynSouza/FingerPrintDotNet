using FingerPrint.Controller;
using FingerPrint.Core;
using FingerPrint.Core.Controller;
using FingerPrint.Core.Events;
using FingerPrint.Data.Model;
using FingerPrint.Data.Persistence;
using System;
using System.Collections;
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
            await GetData();
            GetUserInfo();
            await CheckPermissions();
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
            dataGrid.ItemsSource = await new DataController().GetAllUsers();


        }




        private void GetUserInfo()
        {
            tb_firstName.Text = "FirstName: " + _user.FirstName;
            tb_lastName.Text = "LastName: " +_user.LastName;
            tb_job.Text ="Job: "+ _user.JobName;
            tb_birthDate.Text = "BirthDate: "+ _user.BirthDate.ToString("dd/MM/yyyy");


        }

        private void btn_addUser_Click(object sender, RoutedEventArgs e)
        {
            new AddEditUser(this).Show();
            this.Hide();
        }

        private async void dataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var id = (e.Row.Item as UserModel).Id;
            var UserEditIndo = await new DataController().GetUserInfo(id);
            new AddEditUser(this, UserEditIndo).Show();
            this.Hide();
        }

        //Alterar, executando sempre que tela maximiza
        

        private void btn_logoff_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        
        private async void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(this.IsVisible == true)
            {
                await InitializeSystem();
            }
        }
    }
}
