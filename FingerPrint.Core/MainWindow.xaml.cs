using FingerPrint.Controller;
using FingerPrint.Core.Controller;
using FingerPrint.Data.Model;
using FingerPrint.Data.Persistence;
using FingerPrint.View;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace FingerPrint.Core
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }



        private async void btn_biometry_Click(object sender, RoutedEventArgs e)
        {
            System.Drawing.Image SelectedFingerPrint = null;
            OpenFileDialog dlg = new OpenFileDialog
            {
                InitialDirectory = "c:\\Pictures",
                Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*",
                RestoreDirectory = true

            };

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFileName = dlg.FileName;
                 SelectedFingerPrint = System.Drawing.Image.FromFile(selectedFileName);


            }

            FingerPrintData fingerDataBase = new FingerPrintData();
            var resultComparison = await new ImageController().CompareDatabase(SelectedFingerPrint);

            if (resultComparison != 0)
            {
                var user = await new DataController().GetUserInfo(resultComparison);
                MainSystem newSystem = new MainSystem(user.User);
            }
            else
            {
                System.Windows.MessageBox.Show("Não foi possivel realizar o login");
            }



        }

        private async void btn_login_Click(object sender, RoutedEventArgs e)
        {



            
            LoginController login = new LoginController();

            var result = await login.LogarWithPasswd(new LoginModel()
            {
                Id = 1009,//Convert.ToInt32(tf_id.Text),
                Password = "Desenvolvedor"//tf_passwd.Password
            });

            if (result is null)
                System.Windows.MessageBox.Show("Não foi possivel realizar o login");
            else
            {
                MainSystem newSystem = new MainSystem(result);
                newSystem.Show();
                await newSystem.InitializeSystem();
                this.Close();
            }
        }

        private void tf_id_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
