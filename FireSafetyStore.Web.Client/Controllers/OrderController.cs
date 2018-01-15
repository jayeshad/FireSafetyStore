using FireSafetyStore.Web.Client.Infrastructure.Common;
using FireSafetyStore.Web.Client.Infrastructure.DbContext;
using FireSafetyStore.Web.Client.Models;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace FireSafetyStore.Web.Client.Controllers
{
    public class OrderController : Controller
    {
        private FiresafeDbContext db = new FiresafeDbContext();
        

        [Authorize]
        public async Task<ActionResult> Index()
        {
            var orders = await db.OrderMasters.OrderByDescending(x=>x.OrderDate).ToListAsync();
            return View(orders);
        }

        [Authorize]
        public async Task<ActionResult> DispatchOrder()
        {
            var orders = await db.OrderMasters.Where(x => x.DeliveryDate == null).OrderBy(x => x.OrderDate).ToListAsync();
            return View(orders);
        }

        public ActionResult PlaceOrder(CheckoutViewModel checkoutmodel)
        {
            var order = SessionManager<CheckoutViewModel>.GetValue(Constants.PaymentSessionKey);
            var orderId = AddOrder(order, checkoutmodel);
            var result = db.OrderMasters.Find(orderId);
            return RedirectToAction("OrderSummary",new { id = orderId } );
        }

        public ActionResult ProcessOrder(string id)
        {
            var orderId = new Guid(id);
            var order = db.OrderMasters.Find(orderId);
            return View();
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
                    SessionManager<List<OrderDetail>>.SetValue(Constants.CartSessionKey, null);
                    SessionManager<CheckoutViewModel>.SetValue(Constants.PaymentSessionKey, null);
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

            throw new NotImplementedException();
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
                IsOrderConfirmed = order.OrderMaster.IsOrderConfirmed,
                IsOrderCancelled = order.OrderMaster.IsOrderCancelled,
                OrderAmount = CalculateOrderAmount(order, Constants.AmountType.ProductCost),
                ShippingAmount = CalculateOrderAmount(order, Constants.AmountType.ShippingCost),
                TotalAmount = CalculateOrderAmount(order, Constants.AmountType.TotalCost)
            };
        }
        private decimal CalculateOrderAmount(CheckoutViewModel order, Constants.AmountType productCost)
        {
            decimal amount = 0;
            switch (productCost)
            {
                case Constants.AmountType.ProductCost:
                    order.OrderDetails.ForEach(x =>
                    {
                        amount += x.Total;
                    });
                    break;
                case Constants.AmountType.ShippingCost: amount = 10;
                    break;
                case Constants.AmountType.TotalCost:
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