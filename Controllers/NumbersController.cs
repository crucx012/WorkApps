using Bible;
using System.Linq;
using System.Web.Mvc;
using WorkApplications.Models;

namespace WorkApplications.Controllers
{
    public class NumbersController : Controller
    {

        public ActionResult Index()
        {

            ViewBag.Message = "Some number based applications";

            return View();
        }
    }
}