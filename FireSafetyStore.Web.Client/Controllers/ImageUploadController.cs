using FireSafetyStore.Web.Client.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FireSafetyStore.Web.Client.Controllers
{
    public class ImageUploadController : Controller
    {
        // GET: ImageUpload
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// http://www.c-sharpcorner.com/blogs/create-thumbnail-of-an-image-using-mvc
        /// </summary>
        /// <param name="image"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ImageUploadThumnail(ImageUploadModel image, HttpPostedFileBase file)
        {
            try
            {
                if (file != null)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var thumbName = fileName.Split('.').ElementAt(0) + "_thumb." + fileName.Split('.').ElementAt(1);
                    fileName = Path.Combine(Server.MapPath("/Images"), fileName);
                    thumbName = Path.Combine(Server.MapPath("/Images"), thumbName);
                    image.ImagePath = fileName; //to store into database, if we use DbContext  
                    file.SaveAs(fileName);
                    Image img = Image.FromFile(fileName);
                    int imgHeight = 100;
                    int imgWidth = 100;
                    if (img.Width < img.Height)
                    {
                        //portrait image  
                        imgHeight = 100;
                        var imgRatio = (float)imgHeight / (float)img.Height;
                        imgWidth = Convert.ToInt32(img.Height * imgRatio);
                    }
                    else if(img.Height < img.Width)
                    {
                        //landscape image  
                        imgWidth = 100;
                        var imgRatio = (float)imgWidth / (float)img.Width;
                        imgHeight = Convert.ToInt32(img.Height * imgRatio);
                    }
                    Image thumb = img.GetThumbnailImage(imgWidth, imgHeight, () => false, IntPtr.Zero);
                    thumb.Save(thumbName);
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message.ToString();
                return View();
            }
        }
    }
}