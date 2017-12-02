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
    public class ReportsController : Controller
    {
        private SisDbContext db = new SisDbContext();
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult NegativeQuantity()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Reorder()
        {
            return View();
        }

        #region DataGridAjaxMethods
        public JsonResult NegativeQuantityData()
        {
            var result = db.ItemLocations.Where(l => l.QuantityOnHand < 0).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReorderData()
        {
            var items = db.Items.Include(i => i.ItemLocations);
            var result = items.Where(i => i.ReorderPoint >= i.ItemLocations.Sum(l => l.QuantityOnHand)).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}