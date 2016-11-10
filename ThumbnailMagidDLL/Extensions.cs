using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RavennaSoftware.ThumbnailMagic
{
    /// <summary>
    /// Giving options to save output byte array to a number of formats
    /// </summary>
    public static class ImageOutputExtensions
    {
        // The first parameter takes the "this" modifier
        public static void SaveThumbnailToJpg(this byte[] imageBytes, string saveToFilePath, int imageQuality)
        {
            if (imageQuality < 0 || imageQuality > 100)
                throw new ArgumentOutOfRangeException("Please chose a quality between 0 and 100.");

            Image imgObj = Utilities.byteArrayToImage(imageBytes);

            // Encoder parameter for image quality 
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, imageQuality);

            // Jpeg image codec 
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            imgObj.Save(saveToFilePath, jpegCodec, encoderParams);
        }

        public static void SaveThumbnailToPng(this byte[] imageBytes, string saveToFilePath)
        {
            Image imgObj = Utilities.byteArrayToImage(imageBytes);
            imgObj.Save(saveToFilePath, ImageFormat.Png);
        }

        public static Image SaveThumbnailToObj(this byte[] image)
        {
            return Utilities.byteArrayToImage(image);
        }

        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }

    }
}
