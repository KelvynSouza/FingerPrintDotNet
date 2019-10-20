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

        private async void Processar(object sender, RoutedEventArgs e)
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

            var resultComparison = await new ImageController().CompareDatabase(System.Drawing.Image.FromFile(@"D:\Imagens\APS\Digital2.jpg"));

            var result = await fingerDataBase.Get(6);
            foreach (FingerprintModel fingerprintModel in result)
                await fingerDataBase.Delete(fingerprintModel.Id);

            result = await fingerDataBase.Get(8);
            foreach (FingerprintModel fingerprintModel in result)
                await fingerDataBase.Delete(fingerprintModel.Id);


        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            image.Source = new BitmapImage(new Uri(@"D:\Imagens\APS\digital.png", UriKind.RelativeOrAbsolute));
        }
    }
}
