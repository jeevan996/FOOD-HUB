using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ROS.Models;

namespace ROS.Controllers
{
    public class SalesOrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SalesOrders
        public ActionResult Index()
        {
            var salesOrders = db.SalesOrders.Include(s => s.shippingAddress);
            return View(salesOrders.ToList());
        }

        // GET: SalesOrders/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = db.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }
            return View(salesOrder);
        }

        // GET: SalesOrders/Create
        public ActionResult Create()
        {
            ViewBag.shippingId = new SelectList(db.ShippingAddresses, "Id", "location");
            return View();
        }

        public ActionResult CreateOrder()
        {
            string addressId = Request.QueryString["addressId"];
            int shipId = Convert.ToInt32(addressId);
            var user = User.Identity;
            Guid userId = Guid.Parse(user.GetUserId());
            Cart cart = db.Carts.FirstOrDefault(s => s.userId == userId);

            List<CartItem> cartItems = db.CartItems.Where(c => c.cartId == cart.Id).ToList();
            SalesOrder so = new SalesOrder();
            so.Id = Guid.NewGuid();
            foreach (CartItem c in cartItems)
            {
                Recipe pd = db.Recipes.FirstOrDefault(p => p.Id == c.productId);
                so.orderCost += pd.Price;
            }
            so.customerId = userId;
            so.shippingId = shipId;
            so.shippingCost = 100;
            so.status = "Order Created";
            so.totalCost = so.orderCost + so.shippingCost;
            db.SalesOrders.Add(so);
            db.SaveChanges();

            foreach (CartItem c in cartItems)
            {
                OrderDetails od = new OrderDetails();
                od.orderId = so.Id;
                od.productId = c.productId;
                od.quantity = c.quantity;
                od.specialOfferId = 1;
                db.OrderDetails.Add(od);
                db.SaveChanges();
            }

            return RedirectToAction("index");
        }

        // POST: SalesOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,customerId,shippingId,totalCost,orderCost,shippingCost,status,orderDate,dueDate,shippingDate,deliveryDate")] SalesOrder salesOrder)
        {
            if (ModelState.IsValid)
            {
                salesOrder.Id = Guid.NewGuid();
                db.SalesOrders.Add(salesOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.shippingId = new SelectList(db.ShippingAddresses, "Id", "location", salesOrder.shippingId);
            return View(salesOrder);
        }

        // GET: SalesOrders/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = db.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.shippingId = new SelectList(db.ShippingAddresses, "Id", "location", salesOrder.shippingId);
            return View(salesOrder);
        }

        // POST: SalesOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,customerId,shippingId,totalCost,orderCost,shippingCost,status,orderDate,dueDate,shippingDate,deliveryDate")] SalesOrder salesOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.shippingId = new SelectList(db.ShippingAddresses, "Id", "location", salesOrder.shippingId);
            return View(salesOrder);
        }

        // GET: SalesOrders/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesOrder salesOrder = db.SalesOrders.Find(id);
            if (salesOrder == null)
            {
                return HttpNotFound();
            }
            return View(salesOrder);
        }

        // POST: SalesOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SalesOrder salesOrder = db.SalesOrders.Find(id);
            db.SalesOrders.Remove(salesOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
