using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Wblack;

namespace WorkApplications.Controllers
{
    public class WblackController : Controller
    {
        private readonly IWblackDataContext _db;

        public WblackController(IWblackDataContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BankAccounts()
        {
            ViewBag.Title = "Bank Accounts";
            ViewBag.Accounts = new SelectList(_db.Accounts.Select(a => new { a.AccountID, a.Name }).OrderBy(a => a.AccountID), "AccountId", "Name");

            return View();
        }

        public ActionResult _Transactions(int accountID, int skip, int take)
        {
            var max = _db.Transactions.Count(t => t.AccountID == accountID);

            if (skip + take >= max + take)
                return null;

            ViewBag.Balance = skip == 0 ? 0 : _db.Transactions.Where(t => t.AccountID == accountID).OrderBy(t => t.TransactionID).Take(skip).Sum(t => t.TransactionAmt + t.CashBack);

            return PartialView(_db.Transactions.Where(t => t.AccountID == accountID).Include(t => t.Entity).OrderBy(t => t.TransactionID).Skip(skip).Take(take));
        }

        public ActionResult _Transactions_Descending(int accountID, int skip, int take)
        {
            var max = _db.Transactions.Count(t => t.AccountID == accountID);

            if (skip + take >= max + take)
                return null;

            ViewBag.Balance = _db.Transactions.Where(t => t.AccountID == accountID).OrderByDescending(t => t.TransactionID).Skip(skip).Sum(t => t.TransactionAmt + t.CashBack);

            return PartialView(_db.Transactions.Where(t => t.AccountID == accountID).Include(t => t.Entity).OrderByDescending(t => t.TransactionID).Skip(skip).Take(take));
        }

        public ActionResult _Transactions_NotPosted(int accountID)
        {
            return PartialView(_db.Transactions.Where(t => !t.BankRecorded && t.AccountID == accountID).Include(t => t.Entity).OrderBy(t => t.TransactionID));
        }

        public ActionResult _Transactions_NotPostedDescending(int accountID)
        {
            return PartialView(_db.Transactions.Where(t => !t.BankRecorded && t.AccountID == accountID).Include(t => t.Entity).OrderByDescending(t => t.TransactionID));
        }

        public ActionResult _Transactions_Posted(int accountID, int skip, int take)
        {
            var max = _db.Transactions.Count(t => t.AccountID == accountID && t.BankRecorded);

            if (skip + take >= max + take)
                return null;

            ViewBag.Balance = skip == 0 ? 0 : _db.Transactions.Where(t => t.AccountID == accountID && t.BankRecorded).OrderBy(t => t.TransactionID).Take(skip).Sum(t => t.TransactionAmt + t.CashBack);

            return PartialView(_db.Transactions.Where(t => t.AccountID == accountID && t.BankRecorded).Include(t => t.Entity).Include(t => t.Category).OrderBy(t => t.TransactionID).Skip(skip).Take(take));
        }

        public ActionResult _Transactions_PostedDescending(int accountID, int skip, int take)
        {
            var max = _db.Transactions.Count(t => t.AccountID == accountID && t.BankRecorded);

            if (skip + take >= max + take)
                return null;

            ViewBag.Balance = _db.Transactions.Where(t => t.AccountID == accountID && t.BankRecorded).OrderByDescending(t => t.TransactionID).Skip(skip).Sum(t => t.TransactionAmt + t.CashBack);

            return PartialView(_db.Transactions.Where(t => t.AccountID == accountID && t.BankRecorded).Include(t => t.Entity).Include(t => t.Category).OrderByDescending(t => t.TransactionID).Skip(skip).Take(take));
        }

        public ActionResult _NewTransaction()
        {
            ViewBag.Accounts = new SelectList(_db.Accounts.Select(a => new { a.AccountID, a.Name }).OrderBy(a => a.AccountID), "AccountId", "Name");
            ViewBag.Entities = new SelectList(_db.Entities.Select(e => new { e.EntityID, e.Description }).OrderBy(e => e.EntityID), "EntityID", "Description");
            ViewBag.Categories = new SelectList(_db.Categories.Select(c => new { c.CategoryID, c.Description }).OrderBy(c => c.CategoryID), "CategoryID", "Description");
            ViewBag.Tenders = new SelectList(_db.Tenders.Select(t => new { t.TenderID, t.Description }).OrderBy(t => t.TenderID), "TenderID", "Description");
            ViewBag.TransAccs = new SelectList(_db.Accounts.Select(a => new { a.AccountID, a.Name }).OrderBy(a => a.AccountID), "AccountID", "Description");

            List<SelectListItem> items = new SelectList(_db.Accounts.Select(a => new { a.AccountID, a.Name }).OrderBy(a => a.AccountID), "AccountId", "Name").ToList();
            items.Insert(0, (new SelectListItem { Text = "[None]", Value = null }));
            ViewBag.TransAccs = new SelectList(items, "Value", "Text", null);

            return PartialView();
        }

        public int InsertTransaction(Transaction newTransaction)
        {
            return _db.InsertTransaction(newTransaction);
        }

        public ActionResult _PostTransaction(int transID)
        {
            return PartialView(_db.Transactions.SingleOrDefault(t => t.TransactionID == transID));
        }

        public int PostTransaction(Transaction postTransaction)
        {
            return _db.PostTransaction(postTransaction);
        }

        public ActionResult GasReceipts()
        {
            return View();
        }

        public ActionResult Sleep()
        {
            return View();
        }
    }
}
