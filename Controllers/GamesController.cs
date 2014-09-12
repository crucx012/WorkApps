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

        public ActionResult Roll(int[] rolls)
        {
            var game = new Bowling();

            foreach (int pins in rolls)
                game.Roll(pins);

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

        public ActionResult TicTacToe()
        {
            ViewBag.Title = "TicTacToe";
            ViewBag.Message = "A Tic Tac Toe game";

            return View();
        }

        public ActionResult ChooseStartingPlayer(Piece playerPiece)
        {
            var game = new TicTacToe(3, playerPiece, 1);

            return Json(game.SecondPlayerStart(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClaimCell(Piece playerPiece, Boolean isSecondPlayerFirst, string board, int claimedSquare)
        {
            var game = new TicTacToe(3, playerPiece, 1);
            var newBoard = ToIntArray(board, ',');

            if (isSecondPlayerFirst) game.Turn++;

            if (newBoard[0] != 0) game.ManuallyPopulateCells(newBoard);
            game.TakeTurn(claimedSquare);

            var returnBoard = newBoard.ToList();

            for (int i = 1; i <= 9; i++)
                if (returnBoard.All(v => v != i)
                    && game.GetCellValue(i) != Piece.E)
                    returnBoard.Add(i);

            return Json(returnBoard, JsonRequestBehavior.AllowGet);
        }

        private int[] ToIntArray(string value, char sep)
        {
            string[] sa = value.Split(sep);
            var ia = new int[sa.Length];

            for (int i = 0; i < ia.Length; ++i)
                PopulateIntArray(sa, i, ia);

            return ia;
        }

        private static void PopulateIntArray(string[] sa, int i, int[] ia)
        {
            int j;
            string s = sa[i];

            if (int.TryParse(s, out j))
                ia[i] = j;
        }
    }
}