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
        public ActionResult Index(string category = "")
        {
            vm = new HomeViewModel
            {
                BrandList = PopulateBrands(),
                ProductList = PopulateProducts(category),
            };
            return View(vm);
        }

        private List<ItemViewModel> PopulateProducts(string category)
        {            
            var products = db.Products.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(category))
            {
                var categoryId = new Guid(category);
                products.Where(x => x.CategoryId == categoryId);
            }            
            return MapToViewModel(products.ToList());
        }

        private List<ItemViewModel> MapToViewModel(List<Product> products)
        {
            var itemList = new List<ItemViewModel>();
            products.ForEach(x => itemList.Add(new ItemViewModel
            {
                CategoryId = x.CategoryId,
                ProductId = x.ItemId,
                ProductName = x.ItemName,
                Description = x.Description,
                ImageUrl = x.ImagePath
            }));
            return itemList;
        }

        private List<SelectListItem> PopulateBrands()
        {
            var brands = db.Brands.AsNoTracking().ToList();
            var brandsList = new List<SelectListItem>();
            brands.ForEach(x => brandsList.Add(new SelectListItem
            {
                Text = x.Description,
                Value = x.BrandId.ToString()
            }));
            return brandsList;
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
