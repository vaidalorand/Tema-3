using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniStore.Models;

namespace MiniStore.Controllers
{
    public class CartController : Controller
    {
        CartDBContext db = new CartDBContext();
        ProductDBContext dbproduct = new ProductDBContext();

        //
        // GET: /Cart/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Cart/Details/5

        public ActionResult Details(int id = 0)
        {
            Cart cartproduct = db.Carts.Find(id);
            if (cartproduct == null)
            {
                return HttpNotFound();
            }
            return View(cartproduct);
        }

        //
        // GET: /Cart/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Cart/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cart cartproduct)
        {
            if (ModelState.IsValid)
            {
                db.Carts.Add(cartproduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cartproduct);
        }

        //
        // GET: /Cart/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Cart cartproduct = db.Carts.Find(id);
            if (cartproduct == null)
            {
                return HttpNotFound();
            }
            return View(cartproduct);
        }

        //
        // POST: /Cart/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cart cartproduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartproduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cartproduct);
        }

        //
        // GET: /Cart/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Cart cartproduct = db.Carts.Find(id);
            if (cartproduct == null)
            {
                return HttpNotFound();
            }
            return View(cartproduct);
        }

        //
        // POST: /Cart/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cart cartproduct = db.Carts.Find(id);
            db.Carts.Remove(cartproduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult AddProductToCart(int id)
        {
            var addedProduct = dbproduct.Products.Single(product => product.ProductID == id);

            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedProduct);

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult RemoveProductFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            int itemCount = cart.RemoveFromCart(id);

            var results = new CartRemoveView
            {
                ItemId = id,
                ItemCount = itemCount,
                CartTotal = cart.GetTotal()
            };

            return Json(results);
        }

    }
}