using Bible;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WorkApplications.Controllers
{
    public class BibleController : Controller
    {
        private readonly IBibleDataSource _db;

        public BibleController(IBibleDataSource db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            var db = _db;
            var chapters = new List<SelectListItem>();

            ViewBag.Translations = new SelectList(db.Translations.Select(c => new { c.Id, c.Name })
                .OrderBy(c => c.Id), "Id", "Name");
            ViewBag.Books = new SelectList(db.Books.Select(c => new { c.Id, c.Name })
                .OrderBy(c => c.Id), "Id", "Name");

            for (int i = 1; i <= 50; i++)
                chapters.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            ViewBag.Chapters = new SelectList(chapters, "Text", "Value");
            ViewBag.Message = "The Bible in many translations";

            return View();
        }

        public ActionResult Chapters(string book = "Genesis")
        {
            var db = _db;
            var chapters = new List<SelectListItem>();

            for (int i = 1; i <= db.Books.Single(c => c.Name == book).Chapters; i++)
                chapters.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            return Json(chapters, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _Text(int translationID = 1, string book = "Genesis", int chapter = 1)
        {
            ViewBag.Title = _db.Translations.First(t => t.Id == translationID).Name.ToUpper();
            ViewBag.Message = string.Format("{0}, Chapter {1}", book, chapter);

            var db = _db;

            return PartialView(db.Verses.Where(e => e.TranslationId == db.Translations.FirstOrDefault(t => t.Id == translationID).Id)
                .Where(e => e.BookId == db.Books.FirstOrDefault(b => b.Name == book).Id)
                .Where(e => e.ChapterNumber == chapter)
                .OrderBy(e => e.Id));
        }

        public ActionResult _Search(string searchtext, int translationID = 1, int page = 1, int perPage = 50)
        {
            ViewBag.Message = "Search results for: ";
            ViewBag.Search = searchtext;
            ViewBag.Version = "from the:";
            ViewBag.Translation = _db.Translations.First(t => t.Id == translationID).Name.ToUpper();

            return PartialView(_db.Verses.Where(e => e.Text.Contains(searchtext))
                .Where(v => v.TranslationId == translationID)
                .OrderBy(v => v.Id)
                .Skip((page - 1) * perPage)
                .Take(perPage));
        }

        public ActionResult _Paging(string searchtext, int translationID = 1, int page = 1, int perPage = 50)
        {
            ViewBag.PageIndex = page;
            ViewBag.Pages = _db.Verses.Where(e => e.Text.Contains(searchtext))
                .Where(v => v.TranslationId == translationID)
                .OrderBy(v => v.Id)
                .Count()/perPage;
            ViewBag.IsElipsis1 = (ViewBag.PageIndex - 2) > 2;
            ViewBag.IsElipsis2 = (ViewBag.PageIndex + 2) < ViewBag.Pages;

            return PartialView();
        }

        public ActionResult _Translations(string searchtext)
        {
            ViewBag.ASV = _db.Verses.Where(e => e.Text.Contains(searchtext))
                .Count(e => e.TranslationId == 1);
            ViewBag.ESV = _db.Verses.Where(e => e.Text.Contains(searchtext))
                .Count(e => e.TranslationId == 2);
            ViewBag.KJV = _db.Verses.Where(e => e.Text.Contains(searchtext))
                .Count(e => e.TranslationId == 3);
            ViewBag.NASB = _db.Verses.Where(e => e.Text.Contains(searchtext))
                .Count(e => e.TranslationId == 4);
            ViewBag.NIV = _db.Verses.Where(e => e.Text.Contains(searchtext))
                .Count(e => e.TranslationId == 5);
            ViewBag.NKJV = _db.Verses.Where(e => e.Text.Contains(searchtext))
                .Count(e => e.TranslationId == 6);
            ViewBag.NLT = _db.Verses.Where(e => e.Text.Contains(searchtext))
                .Count(e => e.TranslationId == 7);

            return PartialView();
        }
    }
}