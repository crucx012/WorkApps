using Game;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var game = new Bowling();

            var rols = rolls.Trim('[', '\"', '\\', ']').Split(',');

            foreach (string pins in rols)
                game.Roll(Convert.ToInt32(pins));

            int[] score = game.ScoreByFrames();

            return Json(score, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Lights()
        {
            var colors = Colors();

            ViewBag.Colors = new SelectList(colors, "Value", "Text");

            return View();
        }

        private static IEnumerable<SelectListItem> Colors()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "black", Value = "black"},
                new SelectListItem {Text = "blue", Value = "blue"},
                new SelectListItem {Text = "green", Value = "green"},
                new SelectListItem {Text = "orange", Value = "orange"},
                new SelectListItem {Text = "purple", Value = "purple"},
                new SelectListItem {Text = "red", Value = "red"},
                new SelectListItem {Text = "white", Value = "white"},
                new SelectListItem {Text = "yellow", Value = "yellow"}
            };
        }
    }
}