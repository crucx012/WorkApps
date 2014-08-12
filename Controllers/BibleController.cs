using Bible;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WorkApplications.ViewModel;

namespace WorkApplications.Controllers
{
    public class BibleController : Controller
    {
        private readonly IBibleDataContext _db;

        public BibleController(IBibleDataContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            var chapters = new List<SelectListItem>();

            ViewBag.Translations = new SelectList(_db.Translations.Select(c => new { c.TranslationId, c.Name })
                .OrderBy(c => c.TranslationId), "TranslationId", "Name");
            ViewBag.Books = new SelectList(_db.Books.Select(c => new { c.BookId, c.Name })
                .OrderBy(c => c.BookId), "BookId", "Name");

            for (int i = 1; i <= 50; i++)
                chapters.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            ViewBag.Chapters = new SelectList(chapters, "Text", "Value");
            ViewBag.Message = "The Bible in many translations";

            return View();
        }

        public ActionResult Chapters(string book = "Genesis")
        {
            var chapters = new List<SelectListItem>();

            for (int i = 1; i <= _db.Books.Single(c => c.Name == book).Chapters; i++)
                chapters.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });

            return Json(chapters, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _Text(int translationID = 1, string book = "Genesis", int chapter = 1)
        {
            ViewBag.Title = _db.Translations.First(t => t.TranslationId == translationID).Name.ToUpper();
            ViewBag.Message = string.Format("{0}, Chapter {1}", book, chapter);

            return PartialView(_db.Verses.Where(e => e.Translation == _db.Translations.FirstOrDefault(t => t.TranslationId == translationID))
                .Where(e => e.Book == _db.Books.FirstOrDefault(b => b.Name == book))
                .Where(e => e.ChapterNumber == chapter)
                .OrderBy(e => e.VerseId));
        }

        public ActionResult _Search(string searchtext, int translationID = 1, int page = 1, int perPage = 50)
        {
            ViewBag.Message = "Search results for: " + searchtext;
            ViewBag.Version = "from the:" + _db.Translations.First(t => t.TranslationId == translationID).Name.ToUpper();

            var Verse = _db.Verses
                .Where(v => v.Text.Contains(searchtext))
                .Where(v => v.Translation.TranslationId == translationID)
                .OrderBy(v => v.VerseId)
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToList();

            var book = _db.Books.ToList();

            var model = (from v in Verse
                select new VerseModel
                {
                    BookName = book.FirstOrDefault(b => b.BookId == v.Book.BookId).Name, 
                    ChapterNumber = v.ChapterNumber, 
                    VerseNumber = v.VerseNumber, 
                    Text = v.Text
                }).ToList();

            return PartialView(model);
        }

        public ActionResult _Paging(string searchtext, int translationID = 1, int page = 1, int perPage = 50)
        {
            var matches = _db.Verses.Where(e => e.Text.Contains(searchtext))
                .Where(v => v.Translation.TranslationId == translationID)
                .OrderBy(v => v.VerseId)
                .Count();

            ViewBag.PageIndex = page;
            ViewBag.Pages = matches%perPage == 0 ? (matches / perPage) - 1 : matches / perPage;
            ViewBag.IsElipsis1 = (ViewBag.PageIndex - 2) > 2;
            ViewBag.IsElipsis2 = (ViewBag.PageIndex + 2) < ViewBag.Pages;

            return PartialView();
        }

        public ActionResult _Translations(string searchtext)
        {
            ViewBag.ASV = _db.Verses.Where(e => e.Text.Contains(searchtext))
                .Count(e => e.Translation.TranslationId == 1);
            ViewBag.ESV = _db.Verses.Where(e => e.Text.Contains(searchtext))
                .Count(e => e.Translation.TranslationId == 2);
            ViewBag.KJV = _db.Verses.Where(e => e.Text.Contains(searchtext))
                .Count(e => e.Translation.TranslationId == 3);
            ViewBag.NASB = _db.Verses.Where(e => e.Text.Contains(searchtext))
                .Count(e => e.Translation.TranslationId == 4);
            ViewBag.NIV = _db.Verses.Where(e => e.Text.Contains(searchtext))
                .Count(e => e.Translation.TranslationId == 5);
            ViewBag.NKJV = _db.Verses.Where(e => e.Text.Contains(searchtext))
                .Count(e => e.Translation.TranslationId == 6);
            ViewBag.NLT = _db.Verses.Where(e => e.Text.Contains(searchtext))
                .Count(e => e.Translation.TranslationId == 7);

            return PartialView();
        }
    }
}