using FireSafetyStore.Web.Client.Models;
using System;
using System.Web.Mvc;

namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var vm = new ProductsViewModel();
            vm.ProductList.Add(new ItemViewModel { ProductId = Guid.NewGuid().ToString(), ProductName = "Name1", Description = "Test1", ImageUrl = "../FileStore/D.jpg" });
            vm.ProductList.Add(new ItemViewModel { ProductId = Guid.NewGuid().ToString(), ProductName = "Name2", Description = "Test1", ImageUrl = "../FileStore/D.jpg" });
            vm.ProductList.Add(new ItemViewModel { ProductId = Guid.NewGuid().ToString(), ProductName = "Name3", Description = "Test1", ImageUrl = "../FileStore/D.jpg" });
            vm.ProductList.Add(new ItemViewModel { ProductId = Guid.NewGuid().ToString(), ProductName = "Name4", Description = "Test1", ImageUrl = "../FileStore/D.jpg" });
            vm.ProductList.Add(new ItemViewModel { ProductId = Guid.NewGuid().ToString(), ProductName = "Name5", Description = "Test1", ImageUrl = "../FileStore/D.jpg" });
            vm.ProductList.Add(new ItemViewModel { ProductId = Guid.NewGuid().ToString(), ProductName = "Name6", Description = "Test1", ImageUrl = "../FileStore/D.jpg" });
            return View(vm);
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
