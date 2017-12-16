using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace FireSafetyStore.Web.Client.Infrastructure.Common
{
    public static class FileManager
    {
        private const string Base64ImageFormat = "data:image;base64,{0}";

        public static string UploadFile(this HttpPostedFileBase file, string destinationPath)
        {            
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.InputStream.CopyTo(memoryStream);
                Image img = Image.FromStream(memoryStream);
                img.Save(destinationPath, ImageFormat.Jpeg);
            }
            return destinationPath;
        }

        public static byte[] GetByteArray(this HttpPostedFileBase file)
        {
            byte[] data;
            using (Stream inputStream = file.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }
            return data;
        }

        public static byte[] ImageToByteArray(this Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms,ImageFormat.Jpeg);
            return ms.ToArray();
        }
        public static Image ByteArrayToImage(this byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static string GenerateBase64ImageString(this byte[] arr)
        {
            return string.Format(Base64ImageFormat , Convert.ToBase64String(arr));
        }
}
}