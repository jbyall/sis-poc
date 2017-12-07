﻿using System;
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
    // Handles Inventory requests that are made by the use
    // NOTE - Uncomment the Authorize attribute and modify the role
    //      to control access to this controller.
    //      use the [AllowAnonymous] attribute on any method to override

    //[Authorize(Roles = @"domain\group")]
    public class ReportsController : Controller
    {
        private SisDbContext db = new SisDbContext();
        // Returns the reports menu
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

        [HttpGet]
        public ActionResult ZeroQuantity()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MissingPrice()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UsageByDept()
        {
            return View();
        }

        #region DataGridAjaxMethods
        // Data for negative quantity report
        public JsonResult NegativeQuantityData()
        {
            var result = db.ItemLocations
                .Where(l => l.QuantityOnHand < 0)
                .Include(l => l.Item)
                .Select(l => new
                {
                    l.ItemId,
                    l.LocationId,
                    l.QuantityOnHand,
                    Name = l.Item.Name,
                    UnitPrice = l.Item.Price,
                    Unit = l.Item.Unit,
                })
                .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // Data for reorder report
        public JsonResult ReorderData()
        {
            var items = db.Items.Include(i => i.ItemLocations);
            var result = items.Where(i => i.ReorderPoint > 0 && i.ReorderPoint > i.ItemLocations.Sum(l => l.QuantityOnHand)).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // Data for zero quantity report
        public JsonResult ZeroQuantityData()
        {
            var items = db.Items.Include(i => i.ItemLocations);
            var result = items
                .Where(i => i.ItemLocations.Sum(l => l.QuantityOnHand) == 0)
                .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // Data 
        public JsonResult MissingPriceData()
        {
            var result = db.Items
                .Where(i => !i.Price.HasValue || i.Price == 0)
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);

        }
        public JsonResult UsagebyDeptData()
        {
            var results = db.Transactions.Include(t => t.Item)
                    .Where(t => t.QuantityChange < 0)
                    .Select(t => new {
                        Id = t.Id,
                        TransactionDate = t.Date,
                        Dept = t.DepartmentId,
                        ItemId = t.ItemId,
                        ItemName = t.Item.Name,
                        ItemPrice = t.ItemPrice,
                        ItemUnits = t.Item.Unit,
                        HandedOut = Math.Abs(t.QuantityChange),
                        TValue = Math.Abs(t.TransactionValue)
                    })
                    .OrderBy(t => t.Dept)
                    .ToList();

            return Json(results, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}