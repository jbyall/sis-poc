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
    //[Authorize(Roles = @"domain\group")]
    public class ItemsController : Controller
    {
        private SisDbContext db = new SisDbContext();

        public ActionResult HandoutIndex(string message)
        {
            var items = db.Items.Include(i => i.Supplier);
            if (!string.IsNullOrWhiteSpace(message))
            {
                ViewBag.Message = "Hand Out complete";
            }
            return View();
        }

        public ActionResult ReceiveIndex()
        {
            return View();
        }

        // GET: Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Supplier);
            return View(items.ToList());
        }

        // GET: Items/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Include(i => i.Supplier).Include(i => i.ItemLocations).Single(i => i.Id == id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name");
            ViewBag.Unit = new SelectList(db.Units, "Code", "Description");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Unit,Price,ReorderPoint,SupplierId,Comment")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", item.SupplierId);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Include(i => i.Supplier).Include(i => i.ItemLocations).Single(i => i.Id == id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", item.SupplierId);
            ViewBag.Unit = new SelectList(db.Units, "Code", "Description", item.Unit);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Unit,Price,ReorderPoint,SupplierId,Comment")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = item.Id });
            }
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "Name", item.SupplierId);
            return View(item);
        }

        public ActionResult EditLocations(string id)
        {
            var item = db.Items
                .Where(i => i.Id == id)
                .Include(i => i.Supplier)
                .Include(i => i.ItemLocations)
                .Single();
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Item item = db.Items.Find(id);
            var itemTransactions = db.Transactions.Where(t => t.ItemId == item.Id);
            db.Transactions.RemoveRange(itemTransactions);
            db.SaveChanges();

            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region DataGridAjaxMethods
        [HttpGet]
        public JsonResult HandoutData()
        {
            var items = db.Items
                .Include(i => i.Supplier)
                .Include(i => i.ItemLocations)
                .Where(i => i.ItemLocations.Any(l => l.QuantityOnHand > 0))
                .ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LocationsData(string id)
        {
            var itemLocations = db.ItemLocations.Where(il => il.ItemId == id).ToList();
            return Json(itemLocations, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateLocation(string id, ItemLocation location)
        {
            // Get the item
            var item = db.Items
                .Where(i => i.Id == location.ItemId)
                .Include(i => i.ItemLocations).Single();

            // Prepare for delete existing item location record
            // Find the existing item location
            var updateLocation = item.ItemLocations.Single(l => l.LocationId == id);
            var quantityForUpdate = location.QuantityOnHand == null ? updateLocation.QuantityOnHand : location.QuantityOnHand;

            // Remove the existing location
            item.ItemLocations.Remove(updateLocation);
            db.SaveChanges();

            // Add the new location
            item.ItemLocations.Add(new ItemLocation
            {
                LocationId = location.LocationId,
                QuantityOnHand = quantityForUpdate
            });

            // Commit new location to database
            db.SaveChanges();
            return Json(location, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddLocation(ItemLocation location)
        {
            // Get the item
            var item = db.Items
                .Where(i => i.Id == location.ItemId)
                .Include(i => i.ItemLocations).Single();

            // Add the new location to the item
            item.ItemLocations.Add(location);

            // Commit updates to the database
            db.SaveChanges();
            return Json(location, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveLocation(ItemLocation location)
        {
            // Get the item
            var item = db.Items
                .Where(i => i.Id == location.ItemId)
                .Include(i => i.ItemLocations).Single();

            // Get the location to remove
            var removeLocation = item.ItemLocations
                .Where(l => l.LocationId == location.LocationId)
                .ToList();
            if (removeLocation.Count > 0)
            {
                // Remove the location
                item.ItemLocations.Remove(removeLocation.First());

                // Commit changes to the database
                db.SaveChanges();
            }
            return Json("success");

        }
        #endregion

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
