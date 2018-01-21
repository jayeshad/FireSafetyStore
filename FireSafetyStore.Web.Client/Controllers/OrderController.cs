using FireSafetyStore.Web.Client.Infrastructure.Common;
using FireSafetyStore.Web.Client.Infrastructure.DbContext;
using FireSafetyStore.Web.Client.Models;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using FireSafetyStore.Web.Client.Infrastructure.Security;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace FireSafetyStore.Web.Client.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private FiresafeDbContext db = new FiresafeDbContext();

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
        public async Task<ActionResult> Index()
        {
            var orders = await db.OrderMasters.OrderByDescending(x=>x.OrderDate).ToListAsync();
            return View(orders);
        }

        [Authorize]
        public async Task<ActionResult> DispatchOrderList()
        {
            var status = Infrastructure.Common.Constants.OrderStatuses.NotYetDispatched;
            var orders = await db.OrderMasters.Where(x => x.OrderStatus == status).OrderBy(x => x.OrderDate).ToListAsync();
            return View(orders);
        }

        [Authorize]
        public async Task<ActionResult> DispatchOrder(string id)
        {
            var orderId = new Guid(id);
            var order = await db.OrderMasters.FindAsync(orderId);
            return View(order);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> DispatchOrder(OrderMaster order)
        {

            if (order != null)
            {
                order.OrderStatus = Infrastructure.Common.Constants.OrderStatuses.Dispatched;
                db.OrderMasters.Attach(order);
                db.Entry(order).Property(x => x.DeliveryAgencyName).IsModified = true;
                db.Entry(order).Property(x => x.DeliveryAgentBoyName).IsModified = true;
                db.Entry(order).Property(x => x.DeliveryAgentContactNumber).IsModified = true;                
                db.Entry(order).Property(x => x.DeliveryDate).IsModified = true;
                if (new[] { "0", "1", "2", "3" }.Contains(order.SelectedStatus))
                {
                    order.OrderStatus = Convert.ToInt32(order.SelectedStatus);
                    db.Entry(order).Property(x => x.OrderStatus).IsModified = true;
                }                    
                await db.SaveChangesAsync();
            }
            return View(order);
        }

        [Authorize]
        public async Task<ActionResult> TrackOrder(string id)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            var userId = new Guid(user.Id);
            var orders = db.OrderMasters.Where(x=>x.UserId == userId).OrderByDescending(x => x.OrderDate);
            var vm = MapToViewModel(orders);
            return View(vm);
        }

        private List<TrackOrderViewModel> MapToViewModel(IOrderedQueryable<OrderMaster> orders)
        {
            var list = new List<TrackOrderViewModel>();
            orders.ForEachAsync(x => {
                list.Add(new TrackOrderViewModel
                {
                    OrderId = x.OrderId,
                    OrderCode = x.OrderCode,
                    OrderDate = x.OrderDate,
                    OrderStatus = GetOrderStatus(x.OrderStatus),
                    DeliveryDate = x.DeliveryDate,
                    DeliveryAgencyName = x.DeliveryAgencyName
                });
            });
            return list;
        }

        private string GetOrderStatus(int orderStatus)
        {
            switch (orderStatus)
            {
                case Infrastructure.Common.Constants.OrderStatuses.NotYetDispatched: return "Preparing to Dispatch";
                case Infrastructure.Common.Constants.OrderStatuses.Dispatched: return "Dispatched";
                case Infrastructure.Common.Constants.OrderStatuses.OutForDelivery: return "Out for Delivery";
                case Infrastructure.Common.Constants.OrderStatuses.Delivered: return "Delivered";
                default: return "";
            }
        }

        public ActionResult PlaceOrder(CheckoutViewModel checkoutmodel)
        {
            var order = SessionManager<CheckoutViewModel>.GetValue(Infrastructure.Common.Constants.PaymentSessionKey);
            var orderId = AddOrder(order, checkoutmodel);
            var result = db.OrderMasters.Find(orderId);
            return RedirectToAction("OrderSummary",new { id = orderId } );
        }

        public ActionResult ProcessOrder(string id)
        {
            var orderId = new Guid(id);
            var order = db.OrderMasters.Find(orderId);
            return View(order);
        }

        public ActionResult Edit(string id)
        {
            var orderId = new Guid(id);
            var order = db.OrderMasters.Find(orderId);
            return View(order);
        }

        public ActionResult OrderSummary(string id)
        {
            var orderId = new Guid(id);
            var result = db.OrderMasters.Find(orderId);
            return View(result);
        }

        private Guid AddOrder(CheckoutViewModel order, CheckoutViewModel checkoutmodel)
        {
            if(order != null)
            {
                try
                {
                    var orderId = Guid.NewGuid();
                    db.OrderMasters.Add(MapOrderMaster(orderId, checkoutmodel, order));
                    db.OrderDetails.AddRange(MapOrderDetails(orderId, order.OrderDetails));
                    UpdateStock(order.OrderDetails);
                    db.SaveChanges();
                    return orderId;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("",ex);
                }
                finally
                {
                    SessionManager<List<OrderDetail>>.SetValue(Infrastructure.Common.Constants.CartSessionKey, null);
                    SessionManager<CheckoutViewModel>.SetValue(Infrastructure.Common.Constants.PaymentSessionKey, null);
                }
    
            }
            return Guid.Empty;
        }

        private void UpdateStock(List<OrderDetailViewModel> orderDetails)
        {
            foreach (var order in orderDetails)
            {
                var product = db.Products.Find(order.ItemId);
                if(product != null)
                {
                    var availableQty = product.Quantity - order.Quantity;
                    db.Products.Attach(product);
                    product.Quantity = availableQty;
                    db.Entry(product).Property(x => x.Quantity).IsModified = true;
                }
            }
        }

        private IEnumerable<OrderDetail> MapOrderDetails(Guid orderId, List<OrderDetailViewModel> orderDetails)
        {
            var list = new List<OrderDetail>();
            orderDetails.ForEach(x =>
            {
                list.Add(new OrderDetail
                {
                    OrderDetailsId = Guid.NewGuid(),
                    OrderId = orderId,
                    ItemId = x.ItemId,
                    Quantity = x.Quantity,
                    Rate = x.Rate,
                    Total = x.Total
                });
            });
            return list;
        }

        private OrderMaster MapOrderMaster(Guid orderId, CheckoutViewModel checkoutmodel, CheckoutViewModel order)
        {
            return new OrderMaster
            {
                OrderId = orderId,
                OrderCode = order.OrderMaster.OrderCode,
                OrderDate = order.OrderMaster.OrderDate,
                UserId = order.OrderMaster.UserId,
                CustomerFullName = order.OrderMaster.CustomerFullName,
                CustomerAddress = order.OrderMaster.CustomerAddress,
                CustomerState = order.OrderMaster.CustomerState,
                CustomerCountry = order.OrderMaster.CustomerCountry,
                CustomerPostalCode = order.OrderMaster.CustomerPostalCode,
                CustomerContactNumber = order.OrderMaster.CustomerContactNumber,
                ContactEmail = order.OrderMaster.ContactEmail,
                OrderStatus = order.OrderMaster.OrderStatus,
                IsOrderCancelled = order.OrderMaster.IsOrderCancelled,
                OrderAmount = CalculateOrderAmount(order, Infrastructure.Common.Constants.AmountType.ProductCost),
                ShippingAmount = CalculateOrderAmount(order, Infrastructure.Common.Constants.AmountType.ShippingCost),
                TotalAmount = CalculateOrderAmount(order, Infrastructure.Common.Constants.AmountType.TotalCost)
            };
        }
        private decimal CalculateOrderAmount(CheckoutViewModel order, Infrastructure.Common.Constants.AmountType productCost)
        {
            decimal amount = 0;
            switch (productCost)
            {
                case Infrastructure.Common.Constants.AmountType.ProductCost:
                    order.OrderDetails.ForEach(x =>
                    {
                        amount += x.Total;
                    });
                    break;
                case Infrastructure.Common.Constants.AmountType.ShippingCost: amount = 10;
                    break;
                case Infrastructure.Common.Constants.AmountType.TotalCost:
                    order.OrderDetails.ForEach(x =>
                    {
                        amount += x.Total;
                    });
                    amount = amount + 10;
                    break;
                default:
                    break;
            }
            return amount;
        }


    }
}