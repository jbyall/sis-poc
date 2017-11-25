using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIS.Domain;
using SIS.Web.Models;

namespace SIS.Web.Controllers
{
    public class TransactionsController : Controller
    {
        private SisDbContext db = new SisDbContext();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Department).Include(t => t.Item);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Handout(string id = null)
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Description");
            ViewBag.LocationId = new List<SelectListItem>();
            if (string.IsNullOrWhiteSpace(id))
            {
                ViewBag.ItemId = new SelectList(db.Items, "Id", "Id");
                return View(new HandoutViewModel());
            }
            //var locationId = this.Request.QueryString["location"];
            //if (!String.IsNullOrWhiteSpace(locationId))
           // {
            //    var location = db.Locations.Where(l => l.Id == locationId).SingleOrDefault();
            //    ViewBag.LocationId = new SelectList(db.ItemLocations.Where(l => l.ItemId == id).ToList(), "LocationId", "LocationId", location.Id);
            //}

            var item = db.Items.Where(i => i.Id == id).Include(i => i.Supplier).Include(i => i.ItemLocations).SingleOrDefault();
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Id", item.Id);
            //var itemLocation = item.ItemLocations.Where(l => l.LocationId == locationId).SingleOrDefault();
            var model = new HandoutViewModel(item);
            return View(model);
            
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Handout(HandoutViewModel model)
        {
            // TODO : Add data validation
            if (ModelState.IsValid)
            {
                var transaction = createTransactionFromViewModel(model);
                updateLocationQuantities(model);

                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("test", "Items");
            }

            // If data is invalid return view with pre-populated data
            ViewBag.LocationId = new SelectList(db.ItemLocations.Where(l => l.ItemId == model.ItemId).ToList(), "LocationId", "LocationId", model.LocationId);
            // TODO : 
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", model.DepartmentId);
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", model.ItemId);
            return View(model);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", transaction.DepartmentId);
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", transaction.ItemId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemId,Date,DepartmentId,QuantityOnHandDist,QuantityOnHandStor,QuantityOnHandSub,QuantityChangeDist,QuantityChangeStor,QuantityChangeSub,ItemPrice,TransactionValue")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", transaction.DepartmentId);
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", transaction.ItemId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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

        #region helpers
        private Transaction createTransactionFromViewModel(HandoutViewModel model)
        {
            var result = new Transaction
            {
                Date = DateTime.Now,
                ItemId = model.ItemId,
                DepartmentId = db.Departments.Single(d => d.Name == model.DepartmentId).Id,
                ItemPrice = model.ItemPrice ?? 0,
                //QuantityChange = model.Quantity * -1
            };

            model.Transactions.ForEach(t => result.QuantityChange += t.HandOutQuantity);

            result.TransactionValue = result.QuantityChange * model.ItemPrice.Value;
            return result;
        }

        private void updateLocationQuantities(HandoutViewModel model)
        {
            foreach (var item in model.Transactions)
            {
                var location = db.ItemLocations.Single(l => l.LocationId == item.Location.LocationId && l.ItemId == model.ItemId);
                location.QuantityOnHand -= item.HandOutQuantity;
            }
            db.SaveChanges();
            // Note
        }
        #endregion
    }
}
