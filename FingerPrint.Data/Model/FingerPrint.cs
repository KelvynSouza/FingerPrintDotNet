using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint.Data.Model
{
    public class Fingerprint
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public byte[] FingerPrintImage { get; set; }        

        public Image GetFingerPrintImage()
        {
            using (var ms = new MemoryStream(FingerPrintImage))
            {
                return Image.FromStream(ms);
            }
        }

        public void SetFingerPrintImage(byte[] value)
        {
            FingerPrintImage = value;
        }

        public void SetFingerPrintImage(Image value)
        {
            FingerPrintImage = ImageToByte(value);
        }        

        private byte[] ImageToByte(Image img)
        {           
            MemoryStream tmpStream = new MemoryStream();
            img.Save(tmpStream, ImageFormat.Png); // change to other format
            tmpStream.Seek(0, SeekOrigin.Begin);
            byte[] imgBytes = new byte[tmpStream.Length];
            tmpStream.Read(imgBytes, 0, (int)tmpStream.Length);
            return imgBytes;
        }
    }
}
