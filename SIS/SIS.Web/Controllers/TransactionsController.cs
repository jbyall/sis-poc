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

        // Serves the Handout transaction view with populated data to the user (Views >> Transactions >> Handout.cshtml)
        [HttpGet]
        public ActionResult Handout(string id = null)
        {
            // If there is no id for some reason, redirect to the HandoutIndex for the user to search for and select an item
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction("HandoutIndex", "Items");
            }

            var item = db.Items.Where(i => i.Id == id).Include(i => i.Supplier).Include(i => i.ItemLocations).SingleOrDefault();
            var model = new TransactionViewModel(item);
            return View(model);
            
        }

        // This is hit after user submits the Handout transaction form (Views >> Transactions >> Handout.cshtml)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Handout(TransactionViewModel model)
        {
            // TODO : Add data validation
            // NOTE - This is almost identical to what happens the Receive post method.
            // See comments in the Receive post method for more details
            if (ModelState.IsValid)
            {
                var transaction = createTransactionsFromViewModel(model, TransactionTypes.Handout);
                foreach (var itemTransaction in model.Transactions)
                {
                    updateLocationQuantities(itemTransaction, model.ItemId, TransactionTypes.Handout);
                }
                db.Transactions.AddRange(transaction);
                db.SaveChanges();
                return RedirectToAction("HandoutIndex", "Items", new { message = "success"});
            }

            // If data is invalid return view with pre-populated data
            //ViewBag.LocationId = new SelectList(db.ItemLocations.Where(l => l.ItemId == model.ItemId).ToList(), "LocationId", "LocationId", model.LocationId);
            //// TODO : 
            //ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", model.DepartmentId);
            //ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", model.ItemId);

            // If we get here, user entered invalid data. Return the same view with validation errors
            return View(model);
        }

        // Serves the Receive transaction view with populated data to the user (Views >> Transactions >> Receive.cshtml)
        [HttpGet]
        public ActionResult Receive(string id)
        {
            // The id is actually a comma separated string to allow the user to receive multiple items at once
            // Split will separate the ids
            var selectedItemIds = id.Split(',').ToList();

            // Get the Item entities from the database with corresponding ids
            var selectedItems = db.Items.Where(i => selectedItemIds.Contains(i.Id))
                .Include(i => i.Supplier)
                .Include(i => i.ItemLocations)
                .ToList();

            // Since the user may receive multiple items, we pass a collection of view models to the view
            var models = new List<TransactionViewModel>();
            foreach (var item in selectedItems)
            {
                // Construct each view model using data from the Item entities
                models.Add(new TransactionViewModel(item));
            }

            // Return view with the collection of view models
            return View(models);
        }

        // This is hit after user submits the Receive transaction form (Views >> Transactions >> Receive.cshtml)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Receive(IList<TransactionViewModel> models)
        {
            // Create an empty list to populate in the foreach loops below
            var transactions = new List<Transaction>();
            foreach (var viewModel in models)
            {
                // Create Transaction db entities in memory (to actually add to the db after all loops are done)
                transactions.AddRange(createTransactionsFromViewModel(viewModel, TransactionTypes.Receive));

                // Update the quantities (actually updates the database)
                foreach (var transaction in viewModel.Transactions)
                {
                    updateLocationQuantities(transaction, viewModel.ItemId, TransactionTypes.Receive);
                }
                
            }

            // Transferring the in-memory list created above to the EntityFramework in-memory list (still no db change yet)
            // NOTE - Another approach would have been to skip the "transactions" list and go straight to the db.Transactions collection,
            // but this method acts more like a database transaction (i.e. add none if an exception occurs)
            db.Transactions.AddRange(transactions);

            // Commit changes (adding Transaction entities to the database)
            db.SaveChanges();

            // Redirect to confirm
            var confirmIds = string.Join(",", transactions.Select(t => t.ItemId));
            return RedirectToAction("ReceiveConfirm", "Transactions", new { id = confirmIds });
        }

        public ActionResult ReceiveConfirm(string id)
        {
            // The id is actually a comma separated string to allow the user to receive multiple items at once
            // Split will separate the ids
            var selectedItemIds = id.Split(',').ToList();

            // Get the Item entities from the database with corresponding ids
            var selectedItems = db.Items.Where(i => selectedItemIds.Contains(i.Id))
                .Include(i => i.Supplier)
                .Include(i => i.ItemLocations)
                .ToList();

            // Since the user may receive multiple items, we pass a collection of view models to the view
            var models = new List<TransactionViewModel>();
            foreach (var item in selectedItems)
            {
                // Construct each view model using data from the Item entities
                models.Add(new TransactionViewModel(item));
            }

            // Return view with the collection of view models
            return View(models);
        }

        [HttpGet]
        public JsonResult IndexData()
        {
            var transactions = db.Transactions.Include(t => t.Item)
                                .Select(r => new {
                                    r.Date,
                                    r.DepartmentId,
                                    r.ItemId,
                                    ItemName = r.Item.Name,
                                    r.ItemPrice,
                                    r.LocationId,
                                    r.QuantityChange,
                                    r.TransactionValue,
                                }).ToList();
            return Json(transactions, JsonRequestBehavior.AllowGet);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            throw new NotImplementedException();
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Transaction transaction = db.Transactions.Find(id);
            //if (transaction == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", transaction.DepartmentId);
            //ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", transaction.ItemId);
            //return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ItemId,Date,DepartmentId,QuantityOnHandDist,QuantityOnHandStor,QuantityOnHandSub,QuantityChangeDist,QuantityChangeStor,QuantityChangeSub,ItemPrice,TransactionValue")] Transaction transaction)
        {
            throw new NotImplementedException();
            //if (ModelState.IsValid)
            //{
            //    db.Entry(transaction).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", transaction.DepartmentId);
            //ViewBag.ItemId = new SelectList(db.Items, "Id", "Name", transaction.ItemId);
            //return View(transaction);
        }

        
        public ActionResult Delete(int? id)
        {
            throw new NotImplementedException();
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Transaction transaction = db.Transactions.Find(id);
            //if (transaction == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(transaction);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            throw new NotImplementedException();
            //Transaction transaction = db.Transactions.Find(id);
            //db.Transactions.Remove(transaction);
            //db.SaveChanges();
            //return RedirectToAction("Index");
        }

        // Scaffold method - this closes the connection to the database
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region helpers
        /// <summary>
        /// Returns Transaction entities created from a view model (NOTE - creates in-memory only, does not update the database)
        /// </summary>
        private List<Transaction> createTransactionsFromViewModel(TransactionViewModel model, string transactionType)
        {
            // Multiplier used to calculate + or - Qty and Value based on handout or receive
            var transactionMultiplier = getTransactionMultiplier(transactionType);

            // This is needed to map the actual database Id field (since the select box uses 
            var department = db.Departments.SingleOrDefault(d => d.Id == model.DepartmentId);

            // Create an emply list for the result
            var result = new List<Transaction>();

            // Populate the result list with non $0 transactions from the model (Note - even $0 transactions are returned from view) 
            foreach (var modelTransaction in model.Transactions)
            {
                if (modelTransaction.QuantityChange != 0)
                {
                    // Create the transaction in memory
                    var addMe = new Transaction
                    {
                        Date = DateTime.Now,
                        ItemId = model.ItemId,
                        DepartmentId = department == null ? "3XX99" : department.Id,
                        ItemPrice = model.ItemPrice ?? 0,
                        LocationId = modelTransaction.Location.LocationId
                    };

                    // Update calculated fields (Note - calculated properties can't be initialized like the ones above
                    addMe.QuantityChange += modelTransaction.QuantityChange * transactionMultiplier;
                    addMe.TransactionValue = addMe.QuantityChange * model.ItemPrice.Value;
                    result.Add(addMe);
                }
                
            }
            // Return the populated list of Transaction entities
            return result;
        }


        /// <summary>
        /// Updates ItemLocation quantities in the database (NOTE - This method will update the database)
        /// </summary>
        private void updateLocationQuantities(ItemTransaction itemTransaction, string itemId, string transactionType)
        {
            // Only updates database if non 0 quantity change
            if (itemTransaction.QuantityChange != 0)
            {
                int transactionMultiplier = getTransactionMultiplier(transactionType);
                var location = db.ItemLocations.Single(l => l.LocationId == itemTransaction.Location.LocationId && l.ItemId == itemId);
                location.QuantityOnHand += (itemTransaction.QuantityChange * transactionMultiplier);

                // This line executes the update in the database
                db.SaveChanges();
            }
            
        }

        /// <summary>
        /// Calculates a +1 or -1 multiplier based on transaction type
        /// </summary>
        /// <param name="transactionType">"Receive" or "Handout" (see TransactionTypes static class)</param>
        /// <returns> +1 for Receive, -1 for Handout</returns>
        private int getTransactionMultiplier(string transactionType)
        {
            // A string is used for "transactionType", but it is controlled by
            // constant string variables in the TransactionTypes static class. When used properly,
            // this approach is as reliable as using Enums. It alleviates the need for string comparisons (e.g. ToLower(), Contains())
            // that are typically needed to account for typos and inconsistent string entry
            switch (transactionType)
            {
                // If receiving, return positive for positive qty change
                case TransactionTypes.Receive:
                    return 1;
                // If distributing, return negative for a negative qty change
                // Note - this will fall through to the default case (i.e. anything that is not "Receive" will result in -1
                case TransactionTypes.Handout:
                default:
                    return -1;
            }
        }
        #endregion
    }
}
