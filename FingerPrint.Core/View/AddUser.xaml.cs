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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        private Window _controlPanel;
        public AddUser(Window controlPanel )
        {
            _controlPanel = controlPanel;
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            _controlPanel.Show();
            this.Close();
        }
    }
}
