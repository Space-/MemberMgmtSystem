using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MemberMgmtSystem.Models;

namespace MemberMgmtSystem.Controllers
{
    public class CustomersController : Controller
    {
        private Northwind db = new Northwind();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.OrderByDescending(c => c.CustomerID).ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,ContactName,Address,Phone,Age")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customers);
        }

        // GET: Customers/Edit/5
        //        public ActionResult Edit(int? id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            Customers customer = db.Customers.Find(id);
        //            if (customer == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View(customer);
        //        }

        // POST: Customers/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //        [HttpPost]
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public PartialViewResult Edit([Bind(Include = "CustomerID,ContactName,Address,Phone,Age")] Customers customer)
        {
            if (ModelState.IsValid)
            {
                //                db.Entry(customer).State = EntityState.Modified;
                //                db.SaveChanges();
                //                return RedirectToAction("Index");
            }
            return PartialView("_EditCustomer", customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customers customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            //            return View(customer);
            //            return PartialView("_DeleteCustomer", customer);
            return View("Delete", customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customers customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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

        //        [HttpPost]
        public PartialViewResult PartialViewDelCustomer(int id)
        {
            var customer = db.Customers.Find(id);
            return PartialView("_DeleteCustomer", customer);
        }

        public PartialViewResult PartialViewEditCustomer(int id)
        {
            var customer = db.Customers.Find(id);
            return PartialView("_EditCustomer", customer);
        }
    }
}