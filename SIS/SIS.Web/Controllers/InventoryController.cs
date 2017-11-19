using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIS.Domain;

namespace SIS.Web.Controllers
{
    public class InventoryController : Controller
    {
        private SisDbContext db = new SisDbContext();

        // GET: Inventory
        public ActionResult Index()
        {
            var itemLocations = db.ItemLocations.Include(i => i.Item).Include(i => i.Location).ToList();
            return View(itemLocations);
        }

        // GET: Inventory/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemLocation itemLocation = db.ItemLocations.Find(id);
            if (itemLocation == null)
            {
                return HttpNotFound();
            }
            return View(itemLocation);
        }

        // GET: Inventory/Create
        public ActionResult Create()
        {
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name");
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Type");
            return View();
        }

        // POST: Inventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,LocationId,QuantityOnHand")] ItemLocation itemLocation)
        {
            if (ModelState.IsValid)
            {
                db.ItemLocations.Add(itemLocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", itemLocation.ItemId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Type", itemLocation.LocationId);
            return View(itemLocation);
        }

        // GET: Inventory/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemLocation itemLocation = db.ItemLocations.Find(id);
            if (itemLocation == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", itemLocation.ItemId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Type", itemLocation.LocationId);
            return View(itemLocation);
        }

        // POST: Inventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,LocationId,QuantityOnHand")] ItemLocation itemLocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemLocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", itemLocation.ItemId);
            ViewBag.LocationId = new SelectList(db.Locations, "Id", "Type", itemLocation.LocationId);
            return View(itemLocation);
        }

        // GET: Inventory/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemLocation itemLocation = db.ItemLocations.Find(id);
            if (itemLocation == null)
            {
                return HttpNotFound();
            }
            return View(itemLocation);
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ItemLocation itemLocation = db.ItemLocations.Find(id);
            db.ItemLocations.Remove(itemLocation);
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
