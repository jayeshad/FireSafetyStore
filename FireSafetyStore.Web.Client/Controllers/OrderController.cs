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
        public async Task<ActionResult> DispatchOrderList()
        {
            var status = Constants.OrderStatuses.NotYetDispatched;
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

            var entity = db.OrderMasters.Find(order.OrderId);
            if (entity != null)
            {
                entity.OrderStatus = Constants.OrderStatuses.Dispatched;
                db.OrderMasters.Attach(entity);
                db.Entry(entity).Property(x => x.DeliveryAgencyName).IsModified = true;
                db.Entry(entity).Property(x => x.DeliveryAgentBoyName).IsModified = true;
                db.Entry(entity).Property(x => x.DeliveryAgentContactNumber).IsModified = true;
                db.Entry(entity).Property(x => x.DeliveryDate).IsModified = true;
                db.Entry(entity).Property(x => x.OrderStatus).IsModified = true;
                await db.SaveChangesAsync();
            }
            return View(entity);
        }

        //[Authorize]
        //public async Task<ActionResult> TrackOrder(string id)
        //{
        //    var status = Constants.OrderStatuses.;
        //    var orders = db.OrderMasters.Where(x => x.OrderStatus == status).OrderBy(x => x.OrderDate);
        //    if (string.IsNullOrEmpty(id))
        //    var orderId = await orders.ToListAsync();
        //    return View(order);
        //}

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