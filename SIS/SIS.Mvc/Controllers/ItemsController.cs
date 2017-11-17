using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SIS.Data;

namespace SIS.Mvc.Controllers
{
    public class ItemsController : Controller
    {
        private SISDbContext db = new SISDbContext();

        // GET: Item_Master
        public ActionResult Index()
        {
            return View(db.Item_Master.ToList());
        }

        // GET: Item_Master/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item_Master item_Master = db.Item_Master.Find(id);
            if (item_Master == null)
            {
                return HttpNotFound();
            }
            return View(item_Master);
        }

        // GET: Item_Master/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Item_Master/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Item_Number,Item_Name,Unit,Price,Reorder_point,Supplier_ID,Comment")] Item_Master item_Master)
        {
            if (ModelState.IsValid)
            {
                db.Item_Master.Add(item_Master);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item_Master);
        }

        // GET: Item_Master/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item_Master item_Master = db.Item_Master.Find(id);
            if (item_Master == null)
            {
                return HttpNotFound();
            }
            return View(item_Master);
        }

        // POST: Item_Master/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Item_Number,Item_Name,Unit,Price,Reorder_point,Supplier_ID,Comment")] Item_Master item_Master)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item_Master).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item_Master);
        }

        // GET: Item_Master/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item_Master item_Master = db.Item_Master.Find(id);
            if (item_Master == null)
            {
                return HttpNotFound();
            }
            return View(item_Master);
        }

        // POST: Item_Master/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Item_Master item_Master = db.Item_Master.Find(id);
            db.Item_Master.Remove(item_Master);
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
