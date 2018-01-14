using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using FireSafetyStore.Web.Client;
using FireSafetyStore.Web.Client.Infrastructure.DbContext;

namespace FireSafetyStore.Web.Client.Controllers
{
    public class GridController : Controller
    {
        private FiresafeDbContext db = new FiresafeDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Products_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Product> products = db.Products;
            DataSourceResult result = products.ToDataSourceResult(request, product => new {
                ItemId = product.ItemId,
                ItemName = product.ItemName,
                Description = product.Description,
                Quantity = product.Quantity,
                Rate = product.Rate,
                Image = product.Image,
                ImagePath = product.ImagePath,
                OriginalFileName = product.OriginalFileName,
                UpdatedAt = product.UpdatedAt,
                IsActive = product.IsActive
            });

            return Json(result);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
