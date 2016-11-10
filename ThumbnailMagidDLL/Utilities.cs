using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RavennaSoftware.ThumbnailMagic
{
    static class Utilities
    {
        #region Conversions
        public static byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static byte[] getBytesFromFile(string filePath)
        {
            Image myImg = Image.FromFile(filePath);
            return Utilities.imageToByteArray(myImg);
        }

        public static byte[] getBytesFromURI(string fileUriPath)
        {
            byte[] thumbBytes = new byte[0]; //init return
            using (WebClient webClient = new WebClient())
            {
                thumbBytes = webClient.DownloadData(new Uri(fileUriPath));
            }

            return thumbBytes;
        }

        public static byte[] getBytesFromObj(Image imgObj)
        {
            return Utilities.imageToByteArray(imgObj);
        }
        #endregion
    }
}
