using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.WPF;
using FingerPrint.Core.Events;
using FingerPrint.Data.Persistence;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace FingerPrint.Core.Controller
{
    public class ImageController
    {
               

        public async Task<int> CompareDatabase(Image fingerPrint)
        {
            ProcessImageEvent processImage = new ProcessImageEvent(new FingerPrintData(), new UserData());
            var user = await processImage.CompareImages(fingerPrint);
            int id = (user is null) ?  0 :  user.UserId;
            return id;

            
        }



    }
}
