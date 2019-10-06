﻿using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BitConvert = Emgu.CV.WPF.BitmapSourceConvert;
namespace FingerPrint.Core.Helper
{
    public class ProcessImageHelper
    {
        public Mat _input_thinned;
        public Mat PrepareImage(Mat Image)
        {
            Mat inputBinary = new Mat();
            CvInvoke.Threshold(Image, inputBinary, 0, 255, ThresholdType.Binary | ThresholdType.Otsu);            
            _input_thinned = Skelatanize(inputBinary.Bitmap).Mat;             
            Mat harris_corners = Mat.Zeros(_input_thinned.Rows, _input_thinned.Cols, DepthType.Cv32F, 3);
            CvInvoke.CornerHarris(_input_thinned, harris_corners, 2, 3, 0.04, BorderType.Default);
            Mat harris_normalised = new Mat();
            CvInvoke.Normalize(harris_corners, harris_normalised, 0, 255, NormType.MinMax, DepthType.Cv32F);           
            return harris_normalised;
        }

        public Mat FingerprintRecognition(Mat input)
        {
            var harris_normalised = PrepareImage(input);

            float threshold = 125.0f;
            List<MKeyPoint> mKeyPoints = new List<MKeyPoint>();
            Mat rescaled = new Mat();
            VectorOfKeyPoint keypoints = new VectorOfKeyPoint();
            double scale = 1.0, shift = 0.0;
            CvInvoke.ConvertScaleAbs(harris_normalised, rescaled, scale, shift);
            Mat[] mat = new Mat[] { rescaled, rescaled, rescaled };
            VectorOfMat vectorOfMat = new VectorOfMat(mat);
            int[] from_to = { 0, 0, 1, 1, 2, 2 };
            Mat harris_c = new Mat(rescaled.Size, DepthType.Cv8U, 3);
            CvInvoke.MixChannels(vectorOfMat, harris_c, from_to);
            for (int x = 0; x < harris_c.Width; x++)
                for (int y = 0; y < harris_c.Height; y++)
                {
                    if (GetFloatValue(harris_c, x, y) > threshold)
                    {
                        MKeyPoint m = new MKeyPoint
                        {
                            Size = 1,
                            Point = new PointF(x, y)
                        };
                        mKeyPoints.Add(m);
                    }

                }

            keypoints.Push(mKeyPoints.ToArray());
            Mat descriptors = new Mat();
            ORBDetector ORBCPU = new ORBDetector();
            ORBCPU.Compute(_input_thinned, keypoints, descriptors);

            return descriptors;

        }

        public static float GetFloatValue(Mat mat, int row, int col)
        {
            var value = new float[1];
            Marshal.Copy(mat.DataPointer + (row * mat.Cols + col) * mat.ElementSize, value, 0, 1);
            return value[0];
        }


        public static Image<Gray, byte> Skelatanize(Bitmap image)
        {
            Image<Gray, byte> imgOld = new Image<Gray, byte>(image);
            Image<Gray, byte> img2 = (new Image<Gray, byte>(imgOld.Width, imgOld.Height, new Gray(255))).Sub(imgOld);
            Image<Gray, byte> eroded = new Image<Gray, byte>(img2.Size);
            Image<Gray, byte> temp = new Image<Gray, byte>(img2.Size);
            Image<Gray, byte> skel = new Image<Gray, byte>(img2.Size);
            skel.SetValue(0);
            CvInvoke.Threshold(img2, img2, 127, 256, 0);
            var element = CvInvoke.GetStructuringElement(ElementShape.Cross, new System.Drawing.Size(3, 3), new System.Drawing.Point(-1, -1));
            bool done = false;

            while (!done)
            {
                CvInvoke.Erode(img2, eroded, element, new System.Drawing.Point(-1, -1), 1, BorderType.Reflect, default(MCvScalar));
                CvInvoke.Dilate(eroded, temp, element, new System.Drawing.Point(-1, -1), 1, BorderType.Reflect, default(MCvScalar));
                CvInvoke.Subtract(img2, temp, temp);
                CvInvoke.BitwiseOr(skel, temp, skel);
                eroded.CopyTo(img2);
                if (CvInvoke.CountNonZero(img2) == 0) done = true;
            }
            return skel;
        }
    }
}