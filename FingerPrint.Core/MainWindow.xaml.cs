using FingerPrint.Core.Controller;
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
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Emgu.CV.WPF;
using System.Text.RegularExpressions;
using FingerPrint.View;

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
            FingerPrintData fingerDataBase = new FingerPrintData();


            FingerprintModel fingerprintData = new FingerprintModel();
            fingerprintData.UserId = 6;
            fingerprintData.SetFingerPrintImage(System.Drawing.Image.FromFile(@"D:\Imagens\APS\digital.png"));
            await fingerDataBase.Create(fingerprintData);

            FingerprintModel fingerprintData2 = new FingerprintModel();
            fingerprintData2.UserId = 8;
            fingerprintData2.SetFingerPrintImage(System.Drawing.Image.FromFile(@"D:\Imagens\APS\Digital2.jpg"));
            await fingerDataBase.Create(fingerprintData2);

            var resultComparison = await new ImageController().CompareDatabase(System.Drawing.Image.FromFile(@"D:\Imagens\APS\Digital3.jpg"));

            var result = await fingerDataBase.Get(6);
            foreach (FingerprintModel fingerprintModel in result)
                await fingerDataBase.Delete(fingerprintModel.Id);

            result = await fingerDataBase.Get(8);
            foreach (FingerprintModel fingerprintModel in result)
                await fingerDataBase.Delete(fingerprintModel.Id);
        }

        private async void btn_login_Click(object sender, RoutedEventArgs e)
        {
            new MainSystem().Show();
            this.Close();

            //UserData userData = new UserData();
            
            //LoginController login = new LoginController();            
            //var result = await login.LogarWithPasswd(new LoginModel()
            //{
            //    Id = Convert.ToInt32(tf_id.Text),
            //    Password = tf_passwd.Text
            //});

            //if (result)
            //    MessageBox.Show("Login realizado com sucesso.");
            //else
            //    MessageBox.Show("Não foi possivel realizar o login");

            
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
