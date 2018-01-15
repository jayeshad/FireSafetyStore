using FireSafetyStore.Web.Client.Infrastructure.Common;
using FireSafetyStore.Web.Client.Infrastructure.DbContext;
using FireSafetyStore.Web.Client.Models;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FireSafetyStore.Web.Client.Controllers
{
    public class ShoppingController : Controller
    {
        private FiresafeDbContext db = new FiresafeDbContext();
        public ShoppingController()
        {

        }

        [Authorize]
        public ActionResult Index()
        {
            var shoppingCart = new ShoppingCartViewModel { ShoppingCartItems = new List<ItemViewModel>() };
            var currentCart = SessionManager<ShoppingCartViewModel>.GetValue(Constants.CartSessionKey);
            if (currentCart != null)
            {
                shoppingCart = SessionManager<ShoppingCartViewModel>.GetValue(Constants.CartSessionKey);
            }
            return View(shoppingCart);
        }

        [HttpPost]
        public ActionResult GoToCart()
        {
            var shoppingCart = new ShoppingCartViewModel { ShoppingCartItems = new List<ItemViewModel>() };
            var currentCart = SessionManager<ShoppingCartViewModel>.GetValue(Constants.CartSessionKey);
            if (currentCart != null || currentCart.ShoppingCartItems.Any())
            {
                shoppingCart = SessionManager<ShoppingCartViewModel>.GetValue(Constants.CartSessionKey);
            }
            return View(shoppingCart);
        }

        [Authorize]
        public ActionResult AddToCart(string id)
        {
            var shoppingCart = new ShoppingCartViewModel { ShoppingCartItems = new List<ItemViewModel>()};
            var productId = new Guid(id);
            var entity = db.Products.FirstOrDefault(x => x.ItemId == productId);
            var model = MapToViewModel(entity);
            var currentCart = SessionManager<ShoppingCartViewModel>.GetValue(Constants.CartSessionKey);
            if(currentCart == null || !currentCart.ShoppingCartItems.Any())
            {
                shoppingCart.ShoppingCartItems.Add(model);
                SessionManager<ShoppingCartViewModel>.SetValue(Constants.CartSessionKey, shoppingCart);
            }
            else
            {
                shoppingCart = currentCart;
                shoppingCart.ShoppingCartItems.Add(model);
                SessionManager<ShoppingCartViewModel>.SetValue(Constants.CartSessionKey, shoppingCart);
            }
            return View(shoppingCart);
        }

        [Authorize]
        public ActionResult RemoveFromCart(string id)
        {
            var shoppingCart = new ShoppingCartViewModel { ShoppingCartItems = new List<ItemViewModel>() };
            var productId = new Guid(id);
            var currentCart = SessionManager<ShoppingCartViewModel>.GetValue(Constants.CartSessionKey);
            if (currentCart != null && currentCart.ShoppingCartItems.Any())
            {
                var itemsInCart = currentCart.ShoppingCartItems.Where(x => x.ProductId != productId).ToList();
                shoppingCart.ShoppingCartItems.AddRange(itemsInCart);
                SessionManager<ShoppingCartViewModel>.SetValue(Constants.CartSessionKey, shoppingCart);
            }
            return RedirectToAction("Index", shoppingCart);
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}