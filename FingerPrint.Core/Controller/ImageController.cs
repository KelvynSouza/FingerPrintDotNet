using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using FingerPrint.Core.Helper;
using System;

namespace FingerPrint.Core.Controller
{
    public class ImageController
    {

        #region Example
        //public BitmapSource FingerprintRecognition(string inputImage)
        //{
        //    var processImage = new ProcessImage();
        //    Mat input = CvInvoke.Imread(@"D:\Imagens\APS\digital.png", ImreadModes.Grayscale);

        //    var harris_normalised = processImage.PrepareImage(input);
        //    var input_thinned = processImage.input_thinned;


        //    float threshold = 125.0f;
        //    List<MKeyPoint> mKeyPoints = new List<MKeyPoint>();
        //    Mat rescaled = new Mat();
        //    VectorOfKeyPoint keypoints = new VectorOfKeyPoint();
        //    double scale = 1.0, shift = 0.0;
        //    CvInvoke.ConvertScaleAbs(harris_normalised, rescaled, scale, shift);
        //    Mat[] mat = new Mat[] { rescaled, rescaled, rescaled };
        //    VectorOfMat vectorOfMat = new VectorOfMat(mat);
        //    int[] from_to = { 0, 0, 1, 1, 2, 2 };
        //    Mat harris_c = new Mat(rescaled.Size, DepthType.Cv8U, 3);
        //    CvInvoke.MixChannels(vectorOfMat, harris_c, from_to);
        //    for (int x = 0; x < harris_c.Width; x++)
        //        for (int y = 0; y < harris_c.Height; y++)
        //        {
        //            if (GetFloatValue(harris_c, x, y) > threshold)
        //            {
        //                MKeyPoint m = new MKeyPoint
        //                {
        //                    Size = 1,
        //                    Point = new PointF(x, y)
        //                };
        //                mKeyPoints.Add(m);
        //                //CvInvoke.Circle(harris_normalised, new Point(x,y), 5, new MCvScalar(0, 255, 0), 1);
        //                //CvInvoke.Circle(harris_normalised, new Point(x, y), 1, new MCvScalar(0, 0, 255), 1);
        //            }

        //        }

        //    keypoints.Push(mKeyPoints.ToArray());
        //    Mat descriptors = new Mat();
        //    ORBDetector ORBCPU = new ORBDetector();
        //    ORBCPU.Compute(input_thinned, keypoints, descriptors);



        //    BFMatcher bF = new BFMatcher(DistanceType.Hamming);
        //    VectorOfDMatch matches = new VectorOfDMatch();

        //    //Here we need to loop over the database to find the right fingerprint
        //    {
        //        //Here you put the firgerPrint's Mat you want to compare.
        //        bF.Match(descriptors, descriptors, matches);

        //        //Algorithm to Compare fingerprints
        //        //Len() is a function returning the number of objects within the list
        //        //       Calculate score
        //        float score = 0;
        //        foreach (MDMatch match in matches.ToArray())
        //            score += match.Distance;
        //        float score_threshold = 33;
        //        if (score / matches.ToArray().Length < score_threshold)
        //            Console.WriteLine("Fingerprint matches.");
        //        else
        //            Console.WriteLine("Fingerprint does not match.");
        //    }
        //    return BitConvert.ToBitmapSource(harris_normalised.ToImage<Bgr, byte>());
        //}

        //------------------------------------------------------------------------------------------------

        //public Mat FingerprintRecognition(Mat input)
        //{
        //    var processImage = new ProcessImage();
        //    var harris_normalised = processImage.PrepareImage(input);
        //    var input_thinned = processImage._input_thinned;

        //    float threshold = 125.0f;
        //    List<MKeyPoint> mKeyPoints = new List<MKeyPoint>();
        //    Mat rescaled = new Mat();
        //    VectorOfKeyPoint keypoints = new VectorOfKeyPoint();
        //    double scale = 1.0, shift = 0.0;
        //    CvInvoke.ConvertScaleAbs(harris_normalised, rescaled, scale, shift);
        //    Mat[] mat = new Mat[] { rescaled, rescaled, rescaled };
        //    VectorOfMat vectorOfMat = new VectorOfMat(mat);
        //    int[] from_to = { 0, 0, 1, 1, 2, 2 };
        //    Mat harris_c = new Mat(rescaled.Size, DepthType.Cv8U, 3);
        //    CvInvoke.MixChannels(vectorOfMat, harris_c, from_to);
        //    for (int x = 0; x < harris_c.Width; x++)
        //        for (int y = 0; y < harris_c.Height; y++)
        //        {
        //            if (GetFloatValue(harris_c, x, y) > threshold)
        //            {
        //                MKeyPoint m = new MKeyPoint
        //                {
        //                    Size = 1,
        //                    Point = new PointF(x, y)
        //                };
        //                mKeyPoints.Add(m);
        //            }

        //        }

        //    keypoints.Push(mKeyPoints.ToArray());
        //    Mat descriptors = new Mat();
        //    ORBDetector ORBCPU = new ORBDetector();
        //    ORBCPU.Compute(input_thinned, keypoints, descriptors);

        //    return descriptors;

        //}
        #endregion


        public bool CompareDatabase()
        {
            ProcessImageHelper processImage = new ProcessImageHelper();
            Mat input = CvInvoke.Imread(@"Source\digital2.jpg", ImreadModes.Grayscale);
            Mat input2 = CvInvoke.Imread(@"Source\digital2.jpg", ImreadModes.Grayscale);
            var descriptors1 = processImage.FingerprintRecognition(input);
            var descriptors2 = processImage.FingerprintRecognition(input2);
            return CompareImages(descriptors1, descriptors2);
            
        }

        public bool CompareImages(Mat descriptors1, Mat descriptors2)
        {
            BFMatcher bF = new BFMatcher(DistanceType.Hamming);
            VectorOfDMatch matches = new VectorOfDMatch();

            //Here we need to loop over the database to find the right fingerprint
            {
                //Here you put the firgerPrint's Mat you want to compare.
                bF.Match(descriptors1, descriptors2, matches);

                //Algorithm to Compare fingerprints
                //Calculate score
                float score = 0;
                foreach (MDMatch match in matches.ToArray())
                    score += match.Distance;
                float score_threshold = 33;
                if (score / matches.ToArray().Length < score_threshold)
                    return true;
                else
                    return false;

            }
        }


    }
}
