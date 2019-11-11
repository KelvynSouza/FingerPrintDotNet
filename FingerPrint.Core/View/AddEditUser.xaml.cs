using Emgu.CV.WPF;
using FingerPrint.Controller;
using FingerPrint.Data.Model;
using FingerPrint.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FingerPrint.View
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddEditUser : Window
    {
        private Window _controlPanel;
        private DataController _dataController;

        private UserCompleteDataModel _userEdit;
        private List<FingerprintModel> _UserfingerprintsImage;

        public AddEditUser(Window controlPanel, UserCompleteDataModel userEdit = null)
        {
            InitializeComponent();
            _dataController = new DataController();
            _UserfingerprintsImage = new List<FingerprintModel>();

            
            _controlPanel = controlPanel;

            if (userEdit != null)
            {
                ModelToData(userEdit);
                _userEdit = userEdit;
            }


        }        
        private void CreateImage(BitmapImage img, int fingerId = 0)
        {
            Image addedFingerprints = new Image
            {
                Stretch = Stretch.Fill,
                StretchDirection = StretchDirection.Both,
                Width = 100,
                Height = 170

            };

            addedFingerprints.Margin = new Thickness()
            {
                Top = 5,
                Left = 5
            };
            addedFingerprints.Source = img;
            addedFingerprints.MouseLeftButtonDown += async (ss, ee) =>
           {

               if (_userEdit != null)
                   await _dataController.DeleteFingerPrint(fingerId);
               else
                   _UserfingerprintsImage.Remove(_UserfingerprintsImage.FirstOrDefault(i => i.FingerPrintImage == ImageConvert.ImageToByte(img)));

               stc_fingerprint.Children.Remove(addedFingerprints);
           };
            stc_fingerprint.Children.Add(addedFingerprints);

        }
        private async Task ModelToData(UserCompleteDataModel userEdit)
        {
            //tb_id.IsEnabled = true;
            tb_id.Text = Convert.ToString(userEdit.User.Id);

            tb_fname.Text = userEdit.User.FirstName;
            tb_lname.Text = userEdit.User.LastName;
            tb_bdate.Text = userEdit.User.BirthDate.ToString("dd/MM/yyyy");
            tb_job.Text = userEdit.User.JobName;
            tb_password.Text = userEdit.User.Password;

            cb_read.IsChecked = userEdit.UserRights.Read;
            cb_write.IsChecked = userEdit.UserRights.Write;
            cb_delete.IsChecked = userEdit.UserRights.Delete;

            foreach (var fingerprint in userEdit.UserFingerprints)
            {
                MemoryStream stream = new MemoryStream(fingerprint.FingerPrintImage);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.EndInit();
                CreateImage(bitmap, fingerprint.Id);
            }
        }
        private UserCompleteDataModel EditDataToModel()
        {
            return new UserCompleteDataModel()
            {
                User = new UserModel()
                {
                    Id = _userEdit.User.Id,
                    FirstName = tb_fname.Text,
                    LastName = tb_lname.Text,
                    BirthDate = Convert.ToDateTime(tb_bdate.Text),
                    JobName = tb_job.Text,
                    Password = tb_password.Text
                },

                UserRights = new UserRightsModel()
                {
                    UserID = _userEdit.User.Id,
                    Read = (bool)cb_read.IsChecked,
                    Write = (bool)cb_write.IsChecked,
                    Delete = (bool)cb_delete.IsChecked
                }

            };
        }
        private UserCompleteDataModel CreateDataToModel()
        {
            return new UserCompleteDataModel()
            {
                User = new UserModel()
                {
                    FirstName = tb_fname.Text,
                    LastName = tb_lname.Text,
                    BirthDate = DateTime.Parse(tb_bdate.Text),
                    JobName = tb_job.Text,
                    Password = tb_password.Text
                },

                UserRights = new UserRightsModel()
                {
                    Read = (bool)cb_read.IsChecked,
                    Write = (bool)cb_write.IsChecked,
                    Delete = (bool)cb_delete.IsChecked
                },
                UserFingerprints = _UserfingerprintsImage

            };
        }
        private void bt_back_Click(object sender, RoutedEventArgs e)
        {
            
            _controlPanel.Show();            
            this.Close();
            
        }
        private async void bt_salvar(object sender, RoutedEventArgs e)
        {
            if (_userEdit is null)
                await _dataController.AddUser(CreateDataToModel());
            else
                await _dataController.EditUser(EditDataToModel());


            _controlPanel.Show();
            this.Close();
        }
        private async void bt_addFingerprint_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog
            {
                InitialDirectory = "c:\\Pictures",
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png",
                RestoreDirectory = true

            };

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string selectedFileName = dlg.FileName;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedFileName);
                bitmap.EndInit();
                CreateImage(bitmap);

                if (_userEdit == null)
                    _UserfingerprintsImage.Add(new FingerprintModel()
                    {
                        FingerPrintImage = ImageConvert.ImageToByte(bitmap)
                    });
                else
                {
                    await _dataController.AddFingerprint(new FingerprintModel()
                    {
                        UserId = _userEdit.User.Id,
                        FingerPrintImage = ImageConvert.ImageToByte(bitmap)

                    });
                }
            }



        }

        private void ValidateInput()
        {

        }
    }
}
