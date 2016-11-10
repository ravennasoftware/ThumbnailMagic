using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing.Imaging;
using System.Drawing;

using RavennaSoftware.ThumbnailMagic;
using RavennaSoftware;
using System.IO;

namespace ThumbnailMagicUnitTest
{
    [TestClass]
    public class ThumbMagicTest
    {
        const string TestImageReadFolder = @"E:\CODE\thumbnailmagic\ThumbnailMagicUnitTest\TestImages\";
        const string TestImageWriteFolder = TestImageReadFolder + @"Thumbs\";
        const string TestImageWebUriPath = @"http://www.ravennasoftware.com/mila.jpg";

        [TestMethod]
        public void ThumbnailFromWebToObj()
        {
            //set as kind of random sizes
            int thumbWidth = 120;
            int thumbHeight = 120;

            Image i = ThumbMaker.GenerateFromUri(
                TestImageWebUriPath, 
                ImageFormat.Jpeg,
                thumbHeight, 
                thumbWidth, 
                ThumbMaker.VerticalAlign.Center, 
                ThumbMaker.HorizontalAlign.Center).SaveThumbnailToObj();

            Assert.AreEqual(thumbHeight, i.Height, "generated height was not expected.");
            Assert.AreEqual(thumbWidth, i.Width, "generated width was not expcected");
        }

        [TestMethod]
        public void ThumbnailFromWebToFile()
        {
            ThumbMaker.GenerateFromUri(
                TestImageWebUriPath,
                ImageFormat.Jpeg,
                100,
                400,
                ThumbMaker.VerticalAlign.Center,
                ThumbMaker.HorizontalAlign.Center).SaveThumbnailToJpg(TestImageWriteFolder +  System.Reflection.MethodBase.GetCurrentMethod() + ".png", 100);
        }

        [TestMethod]
        public void ThumbnailFromFileToFilePNG()
        {
            string path = TestImageReadFolder + "SubjectCenter.jpg";
            ThumbMaker.GenerateFromFile(
                path, 
                ImageFormat.Jpeg, 
                150, 
                150, 
                ThumbMaker.VerticalAlign.Center,
                ThumbMaker.HorizontalAlign.Center).SaveThumbnailToPng(TestImageWriteFolder + System.Reflection.MethodBase.GetCurrentMethod() + ".jpg");
        }

        [TestMethod]
        public void ThumbnailFromFileToFileByteArray()
        {
            string path = TestImageReadFolder + "SubjectCenter.jpg";
            byte[] thumb = ThumbMaker.GenerateFromFile(
                path,
                ImageFormat.Jpeg,
                150,
                150,
                ThumbMaker.VerticalAlign.Center,
                ThumbMaker.HorizontalAlign.Center);
        }

        [TestMethod]
        public void ThumbnailFromFileToFileJPG()
        {
            string path = TestImageReadFolder + "FullSubjectAtRight.jpg";
            ThumbMaker.GenerateFromFile(
                path,
                ImageFormat.Jpeg,
                100,
                100,
                ThumbMaker.VerticalAlign.Center,
                ThumbMaker.HorizontalAlign.Center).SaveThumbnailToJpg(TestImageWriteFolder + System.Reflection.MethodBase.GetCurrentMethod() + ".jpg", 100);
        }

        [TestMethod]
        public void ThumbnailFromMemoryToFile()
        {
            string s = Path.GetRandomFileName();

            Image inputImage = Image.FromFile(TestImageReadFolder + "FullSubjectAtLeft.jpg");
            ThumbMaker.GenerateFromObj(
                inputImage,  
                ImageFormat.Jpeg, 
                100, 
                100, 
                ThumbMaker.VerticalAlign.Center,
                ThumbMaker.HorizontalAlign.Center).SaveThumbnailToJpg(TestImageWriteFolder + System.Reflection.MethodBase.GetCurrentMethod() + ".jpg", 50);
        }
    }
}
