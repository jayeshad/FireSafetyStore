using FireSafetyStore.Web.Client.Models;
using System;
using System.Web.Mvc;
using System.Collections.Generic;
using FireSafetyStore.Web.Client.Infrastructure.DbContext;
using System.Linq;
using FireSafetyStore.Web.Client;

namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {
        private FiresafeDbContext db = new FiresafeDbContext();

        private HomeViewModel vm;
        public HomeController()
        {
            
        }
        public ActionResult Index(string category = "",string brand = "")
        {
            vm = new HomeViewModel
            {
                ProductList = PopulateProductsByFilter(category, brand),
            };
            return View(vm);
        }

        private List<ItemViewModel> PopulateProductsByFilter(string category, string brand)
        {            
            var products = db.Products.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(category))
            {
                var categoryId = new Guid(category);
                products = products.Where(x => x.CategoryId == categoryId);
            }
            if (!string.IsNullOrEmpty(brand))
            {
                var brandId = new Guid(brand);
                products = products.Where(x => x.BrandId == brandId);
            }
            return MapToViewModel(products.ToList());
        }

        private List<ItemViewModel> MapToViewModel(List<Product> products)
        {
            var itemList = new List<ItemViewModel>();
            products.ForEach(x => itemList.Add(new ItemViewModel
            {
                ProductId = x.ItemId,
                ProductName = x.ItemName,
                Description = x.Description,
                CategoryId = x.CategoryId,
                Rate = x.Rate,
                Quantity = x.Quantity,
                ImageUrl = x.ImagePath,
                Total = x.Rate
            }));
            return itemList;
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}
