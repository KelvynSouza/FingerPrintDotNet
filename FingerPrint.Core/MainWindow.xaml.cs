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
            FingerPrintData finger = new FingerPrintData();
            Fingerprint fingerprint = new Fingerprint()
            {             
                UserId = 6                
            };
            fingerprint.SetFingerPrintImage(System.Drawing.Image.FromFile(@"D:\Imagens\APS\digital.png"));
            await finger.Create(fingerprint);
            var result = await finger.Get(6);
            image2.Source = BitmapSourceConvert.ConvertBitmap((Bitmap)result.GetFingerPrintImage());

            await finger.Delete(result.Id);

            //var result = new ImageController().CompareDatabase();


        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            image.Source = new BitmapImage(new Uri(@"D:\Imagens\APS\digital.png", UriKind.RelativeOrAbsolute));
        }
    }
}
