using FireSafetyStore.Web.Client.Infrastructure.Common;
using FireSafetyStore.Web.Client.Infrastructure.DbContext;
using FireSafetyStore.Web.Client.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FireSafetyStore.Web.Client.Controllers
{
    public class ShoppingController : Controller
    {
        private const string CartConstant = "ShoppingCart";
        private FiresafeDbContext db = new FiresafeDbContext();
        public ShoppingController()
        {

        }

        [Authorize]
        public ActionResult AddToCart(string id)
        {
            var shoppingCart = new ShoppingCartViewModel { ShoppingCartItems = new List<ItemViewModel>()};
            var productId = new Guid(id);
            var entity = db.Products.FirstOrDefault(x => x.ItemId == productId);
            var model = MapToViewModel(entity);
            var currentCart = SessionManager<ShoppingCartViewModel>.GetValue(CartConstant);
            if(currentCart == null || !currentCart.ShoppingCartItems.Any())
            {
                shoppingCart.ShoppingCartItems.Add(model);
                SessionManager<ShoppingCartViewModel>.SetValue(CartConstant, shoppingCart);
            }
            else
            {
                shoppingCart = currentCart;
                shoppingCart.ShoppingCartItems.Add(model);
                SessionManager<ShoppingCartViewModel>.SetValue(CartConstant, shoppingCart);
            }
            return View(shoppingCart);
        }

        [Authorize]
        public ActionResult RemoveFromCart(string id)
        {
            var shoppingCart = new ShoppingCartViewModel { ShoppingCartItems = new List<ItemViewModel>() };
            var productId = new Guid(id);
            var entity = db.Products.Find(productId);

            var model = MapToViewModel(entity);
            var currentCart = SessionManager<ShoppingCartViewModel>.GetValue(CartConstant);
            if (currentCart == null || !currentCart.ShoppingCartItems.Any())
            {
                shoppingCart.ShoppingCartItems.Add(model);
                SessionManager<ShoppingCartViewModel>.SetValue(CartConstant, shoppingCart);
            }
            else
            {
                shoppingCart = currentCart;
                shoppingCart.ShoppingCartItems.Add(model);
                SessionManager<ShoppingCartViewModel>.SetValue(CartConstant, shoppingCart);
            }
            return View(shoppingCart);
        }

        public ActionResult Details(string id)
        {

            var productId = new Guid(id);
            var entity = db.Products.FirstOrDefault(x => x.ItemId == productId);
            var model = MapToViewModel(entity);
            return View(model);
        }

        private ItemViewModel MapToViewModel(Product entity)
        {
            if (entity == null) throw new ArgumentNullException("ShoppingCartViewModel");
            else
                return new ItemViewModel
                {
                    ProductId = entity.ItemId,
                    ProductName = entity.ItemName,
                    Description = entity.Description,
                    CategoryId = entity.CategoryId,
                    ImageUrl = entity.ImagePath,
                    Quantity = entity.Quantity,
                    Rate = entity.Rate
                };
        }




        private void RemoveItem(Product product)
        {

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}