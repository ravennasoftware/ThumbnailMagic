using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Configuration;

namespace RavennaSoftware.ThumbnailMagic
{
    public static class ThumbMaker
    {
        #region API
        public static byte[] GenerateFromUri(string sourceFileUri, ImageFormat targetFormat, int height, int width, VerticalAlign valign, HorizontalAlign halign)
        {
            return GenerateFromBytes(Utilities.getBytesFromURI(sourceFileUri), targetFormat, height, width, valign, halign);
        }

        public static byte[] GenerateFromFile(string sourceFilePath, ImageFormat targetFormat, int height, int width, VerticalAlign valign, HorizontalAlign halign)
        {
            return GenerateFromBytes(Utilities.getBytesFromFile(sourceFilePath), targetFormat, height, width, valign, halign);
        }

        public static byte[] GenerateFromObj(Image objSource, ImageFormat targetFormat, int height, int width, VerticalAlign valign, HorizontalAlign halign)
        {
            return GenerateFromBytes(Utilities.getBytesFromObj(objSource), targetFormat, height, width, valign, halign);
        }

        public static byte[] GenerateFromBytes(byte[] sourceImageBytes, ImageFormat targetFormat, int height, int width, VerticalAlign valign, HorizontalAlign halign)
        {

            MemoryStream ms = new MemoryStream(sourceImageBytes);

                using (Image img = Image.FromStream(ms))
            
                {
                    using (Image targetImg = AspecResizer(img, height, width, valign, halign))
                    {
                        //return the resized, cropped thumbnail
                        return Utilities.imageToByteArray(targetImg);
                    }
                }
        }
        #endregion

        #region Internal Workings
        /// <summary>
        /// Method to do actual worl on thumbnail generation
        /// </summary>
        /// <param name="imgSrc"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="valign"></param>
        /// <param name="halign"></param>
        /// <returns></returns>
        private static Image AspecResizer(Image imgSrc, int height, int width, VerticalAlign valign, HorizontalAlign halign)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics gObj = Graphics.FromImage(result))
            {
                gObj.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                gObj.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                float ratio = (float)height / (float)imgSrc.Height;
                int temp = (int)((float)imgSrc.Width * ratio);
                if (temp == width)
                {
                    //no corrections are needed!
                    gObj.DrawImage(imgSrc, 0, 0, width, height);
                    return result;
                }
                else if (temp > width)
                {
                    int overFlow = (temp - width);
                    
                    //action based on haligh
                    if (halign == HorizontalAlign.Center)
                    {
                        gObj.DrawImage(imgSrc, 0 - overFlow / 2, 0, temp, height);
                    }
                    else if (halign == HorizontalAlign.Left)
                    {
                        gObj.DrawImage(imgSrc, 0, 0, temp, height);
                    }
                    else if (halign == HorizontalAlign.Right)
                    {
                        gObj.DrawImage(imgSrc, -overFlow, 0, temp, height);
                    }
                }
                else
                {
                    ratio = (float)width / (float)imgSrc.Width;
                    temp = (int)((float)imgSrc.Height * ratio);
                    int overFlow = (temp - height);
                    
                    //action based on valigh
                    if (valign == VerticalAlign.Top)
                    {
                        gObj.DrawImage(imgSrc, 0, 0, width, temp);
                    }
                    else if (valign == VerticalAlign.Center)
                    {
                        gObj.DrawImage(imgSrc, 0, -overFlow / 2, width, temp);
                    }
                    else if (valign == VerticalAlign.Bottom)
                    {
                        gObj.DrawImage(imgSrc, 0, -overFlow, width, temp);
                    }
                }
            }
            return result;
        }

        #endregion

        #region enums

        public enum VerticalAlign
        {
            Top,
            Center,
            Bottom
        }

        public enum HorizontalAlign
        {
            Left,
            Center,
            Right
        }

        #endregion
    }
}