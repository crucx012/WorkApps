using Bible;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WorkApplications.Controllers
{
    public class BibleController : Controller
    {
        private IBibleDataSource _db;

        public BibleController(IBibleDataSource db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            var db = _db;
            var chapters = new List<SelectListItem>();

            ViewBag.Translations = new SelectList(db.Translations.Select(c => new { c.Id, c.Name }), "Id", "Name");
            ViewBag.Books = new SelectList(db.Books.Select(c => new { c.Id, c.Name }), "Id", "Name");

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

        public ActionResult _Text(string translation = "ESV", string book = "Genesis", int chapter = 1)
        {
            ViewBag.Title = translation.ToUpper();
            ViewBag.Message = string.Format("{0}, Chapter {1}", book, chapter);
    
            var db = _db;

            return PartialView(db.Verses.Where(e => e.TranslationId == db.Translations.FirstOrDefault(t => t.Name == translation).Id)
                .Where(e => e.BookId == db.Books.FirstOrDefault(b => b.Name == book).Id)
                .Where(e => e.ChapterNumber == chapter));
        }

        public ActionResult _TextGen(string translation, string book, int chapter)
        {
            var db = _db;
            ViewBag.translation = db.Translations.Single(t => t.Name == translation).Id;
            ViewBag.book = db.Books.Single(b => b.Name == book).Id;
            ViewBag.chapter = chapter;

            return PartialView();
        }
    }
}