﻿using FingerPrint.Core.Controller;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            UserData user = new UserData();
            await user.Delete("7");
           //var result = new ImageController().CompareDatabase();
            
            
        }

        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            image.Source = new BitmapImage(new Uri(@"D:\Imagens\APS\digital.png", UriKind.RelativeOrAbsolute));
        }
    }
}
