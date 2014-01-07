using Bowling;
using System;
using System.Web.Mvc;

namespace WorkApplications.Controllers
{
    public class GamesController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Some games I made.";

            return View();
        }

        public ActionResult Bowling()
        {
            ViewBag.Message = "A bowling scorecard";

            return View();
        }

        public ActionResult Roll(string rolls)
        {
            var game = new Game();

            var rols = rolls.Trim('[', '\"', '\\', ']').Split(',');

            foreach (string pins in rols)
                game.Roll(Convert.ToInt32(pins));

            int[] score = game.ScoreByFrames();

            return Json(score, JsonRequestBehavior.AllowGet);
        }
    }
}