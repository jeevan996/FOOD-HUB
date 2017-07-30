using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ROS.Models;

namespace ROS.Controllers
{
    public class CartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Carts
        public ActionResult Index()
        {
            foreach (CartItem c in db.CartItems.ToList())
            {
                Recipe product = db.Recipes.FirstOrDefault(t => t.Id == c.productId);
                c.product = product;
            }
            return View(db.CartItems.ToList());
        }

        public ActionResult Add(string Id)
        {
            int productId = Convert.ToInt32(Id);
            var userId = User.Identity.GetUserId();
            Guid uId = Guid.Parse(userId);
            Cart c = db.Carts.FirstOrDefault(d => d.userId == uId);
            CartItem cartItem = new CartItem();
            cartItem.cartId = c.Id;
            cartItem.cart = c;
            cartItem.productId = productId;
            cartItem.product = db.Recipes.FirstOrDefault(pd => pd.Id  == productId);
            cartItem.quantity = 1;
            db.CartItems.Add(cartItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Buy(string Id)
        {
            int productId = Convert.ToInt32(Id);
            var userId = User.Identity.GetUserId();
            Guid uId = Guid.Parse(userId);
            Cart c = db.Carts.FirstOrDefault(d => d.userId == uId);
            CartItem cartItem = new CartItem();
            cartItem.cartId = c.Id;
            cartItem.cart = c;
            cartItem.productId = productId;
            cartItem.product = db.Recipes.FirstOrDefault(pd => pd.Id == productId);
            cartItem.quantity = 1;
            db.CartItems.Add(cartItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Carts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // GET: Carts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,userId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cart);
        }

        // GET: Carts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,userId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cart);
        }

        // GET: Carts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart cart = db.Carts.Find(id);
            db.Carts.Remove(cart);
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
