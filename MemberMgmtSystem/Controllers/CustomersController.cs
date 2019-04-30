using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MemberMgmtSystem.Models;

namespace MemberMgmtSystem.Controllers
{
    public class CustomersController : Controller
    {
        private Northwind db = new Northwind();

        public ActionResult Index()
        {
            return View(db.Customers.OrderByDescending(c => c.CustomerID).ToList());
        }

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

        public ActionResult Create()
        {
            return View();
        }

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

        [HttpGet, ActionName("Edit")]
        public ActionResult Edit([Bind(Include = "CustomerID,ContactName,Address,Phone,Age")] Customers customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return Content("RefreshPage");
            }
            return PartialView("_EditCustomer", customer);
        }

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

            return View("Delete", customer);
        }

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