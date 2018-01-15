using AutoMapper;
using FireSafetyStore.Web.Client.Infrastructure.Common;
using FireSafetyStore.Web.Client.Infrastructure.DbContext;
using FireSafetyStore.Web.Client.Infrastructure.Security;
using FireSafetyStore.Web.Client.Models;
using Kendo.Mvc.Extensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FireSafetyStore.Web.Client.Controllers
{
    public class ShoppingController : Controller
    {
        private ShoppingCartViewModel vm;
        private FiresafeDbContext db = new FiresafeDbContext();
        public ShoppingController()
        {
            vm = new ShoppingCartViewModel { ShoppingCartItems = new List<ItemViewModel>() };

        }

        public ShoppingController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        

        [Authorize]
        public ActionResult Index()
        {            
            var currentCart = SessionManager<List<OrderDetail>>.GetValue(Infrastructure.Common.Constants.CartSessionKey);
            if (currentCart != null)
            {
                vm.ShoppingCartItems = MapToViewModel(currentCart);
            }
            return View(vm);
        }

        [Authorize]
        public ActionResult Checkout()
        {
            var currentCart = SessionManager<List<OrderDetail>>.GetValue(Infrastructure.Common.Constants.CartSessionKey);
            if (currentCart != null || currentCart.Any())
            {                
                vm.ShoppingCartItems = MapToViewModel(currentCart);
            }
            return View(vm);
        }

        [Authorize]
        public ActionResult Payment()
        {
            var  viewmodel = new CheckoutViewModel();
            viewmodel.OrderMaster = new OrderMasterViewModel();
            viewmodel.OrderDetails = new List<OrderDetailViewModel>();
            viewmodel.OrderMaster.Total = 0;
            var model = PopulateCustomerInfo();
            viewmodel.OrderMaster = MapOrderMasterToView(model);
            var currentCart = SessionManager<List<OrderDetail>>.GetValue(Infrastructure.Common.Constants.CartSessionKey);
            if (currentCart != null && currentCart.Any())
            {
                currentCart.ForEach(x =>
                {
                    viewmodel.OrderMaster.Total += x.Total;
                    viewmodel.OrderDetails.Add(new OrderDetailViewModel
                    {
                         ItemId = x.ItemId,
                         ItemName = GetProductInfo(x.ItemId).ItemName,
                         Quantity = x.Quantity,
                         Rate = x.Rate,
                         Total = x.Total
                    });
                });                
                SessionManager<CheckoutViewModel>.SetValue(Infrastructure.Common.Constants.PaymentSessionKey, viewmodel);
            }
            return View(viewmodel);
        }

        private OrderMasterViewModel MapOrderMasterToView(OrderMaster model)
        {
            return new OrderMasterViewModel
            {
                OrderId = Guid.NewGuid(),
                OrderCode = model.OrderCode,
                OrderDate = model.OrderDate,
                UserId = model.UserId,
                CustomerFullName = model.CustomerFullName,
                CustomerAddress = model.CustomerAddress,
                CustomerState = model.CustomerState,
                CustomerCountry = model.CustomerCountry,
                CustomerPostalCode = model.CustomerPostalCode,
                CustomerContactNumber = model.CustomerContactNumber,
                ContactEmail = model.ContactEmail,
                IsOrderConfirmed = true,
                IsOrderCancelled = false
            };
        }

        [Authorize]
        public ActionResult AddToCart(string id)
        {
            var productId = new Guid(id);
            var orders = new List<OrderDetail>();
            var product = GetProductInfo(productId);
            if(product != null)
            {
                var activeCart = SessionManager<List<OrderDetail>>.GetValue(Infrastructure.Common.Constants.CartSessionKey);
                if (activeCart == null || !activeCart.Any())
                {
                    orders.Add(new OrderDetail
                    {
                        OrderDetailsId = Guid.NewGuid(),
                        OrderId = Guid.Empty,
                        ItemId = productId,
                        Quantity = 1,
                        Rate = product.Rate,
                        Total = product.Rate
                    });
                    vm.ShoppingCartItems.AddRange(MapToViewModel(orders));
                    SessionManager<List<OrderDetail>>.SetValue(Infrastructure.Common.Constants.CartSessionKey, orders);
                }
                else
                {
                    orders = AppendToShoppingCart(activeCart, product);
                    vm.ShoppingCartItems = MapToViewModel(orders);
                    SessionManager<List<OrderDetail>>.SetValue(Infrastructure.Common.Constants.CartSessionKey, orders);
                }
            }
            
            return View(vm);
        }

       

        [Authorize]
        public ActionResult RemoveFromCart(string id)
        {
            var productId = new Guid(id);
            var currentCart = SessionManager<List<OrderDetail>>.GetValue(Infrastructure.Common.Constants.CartSessionKey);
            if (currentCart != null && currentCart.Any())
            {
                var itemsInCart = currentCart.Where(x => x.ItemId != productId).ToList();
                vm.ShoppingCartItems.AddRange(MapToViewModel(itemsInCart));
                SessionManager<List<OrderDetail>>.SetValue(Infrastructure.Common.Constants.CartSessionKey, itemsInCart);
            }
            return RedirectToAction("Index", vm);
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

        private List<ItemViewModel> MapToViewModel(List<OrderDetail> currentCart)
        {
            var itemsList = new List<ItemViewModel>();
            currentCart.ForEach(x =>
            {
                itemsList.Add(new ItemViewModel
                {
                    ProductId = x.ItemId,
                    ProductName = GetProductInfo(x.ItemId).ItemName,
                    Description = GetProductInfo(x.ItemId).Description,
                    CategoryId = GetProductInfo(x.ItemId).CategoryId,
                    ImageUrl = GetProductInfo(x.ItemId).ImagePath,
                    Quantity = x.Quantity,
                    Rate = x.Rate,
                    Total = decimal.Multiply(Convert.ToDecimal(x.Quantity), x.Rate)
                });
            });
            return itemsList;
        }

        private OrderMaster PopulateCustomerInfo()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var master = new OrderMaster
            {
                OrderId = Guid.NewGuid(),
                OrderCode = GenerateOrderCode(),
                UserId = new Guid(user.Id),
                OrderDate = DateTime.Now,
                CustomerFullName = string.Format("{0} {1}", user.FirstName, user.LastName),
                CustomerAddress = user.Address,
                CustomerState = user.State,
                CustomerCountry = "India",
                CustomerPostalCode = user.PostalCode,
                CustomerContactNumber = user.PhoneNumber,
                ContactEmail = user.Email,
                IsOrderConfirmed = true,
                IsOrderCancelled = false
            };
            return master;
        }

        private List<OrderDetail> AppendToShoppingCart(List<OrderDetail> activeList, Product product)
        {
            var newList = new List<OrderDetail>();
            int updatedcount = 0;
            foreach (OrderDetail item in activeList)
            {
                if (item.ItemId == product.ItemId)
                {
                    item.Quantity += 1;
                    item.Rate = product.Rate * item.Quantity;
                }
                else
                {
                    updatedcount += 1;
                    newList.Add(new OrderDetail
                    {
                        ItemId = product.ItemId,
                        Quantity = 1,
                        Rate = product.Rate,
                        Total = product.Rate
                    });
                }
            }
            if(newList.Any() && updatedcount == 1)
            activeList.AddRange(newList);
            return activeList;
        }

        private Product GetProductInfo(Guid productId)
        {
            var entity = db.Products.FirstOrDefault(x => x.ItemId == productId);
            return entity;
        }

        private string GenerateOrderCode()
        {
            var date = DateTime.Now;
            return string.Format("#FS-{0}{1}{2}{3}{4}{5}", date.Day, date.Month, date.Year, date.Minute, date.Second, date.Millisecond);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}