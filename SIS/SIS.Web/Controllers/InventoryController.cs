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
    // Handles Inventory requests that are made by the use
    // NOTE - Uncomment the Authorize attribute and modify the role
    //      to control access to this controller.
    //      use the [AllowAnonymous] attribute on any method to override

    //[Authorize(Roles = @"domain\group")]
    public class InventoryController : Controller
    {
        private SisDbContext db = new SisDbContext();

        public ActionResult Maintenance()
        {
            return View();
        }

        //// GET: Inventory
        //public ActionResult Index()
        //{
        //    var itemLocations = db.ItemLocations.Include(i => i.Item).Include(i => i.Location).ToList();
        //    return View(itemLocations);
        //}

        public JsonResult Transaction(string id)
        {
            var itemLocations = db.ItemLocations.Where(il => il.ItemId == id).Select(il => il.LocationId).ToList();
            return Json(itemLocations, JsonRequestBehavior.AllowGet);
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
