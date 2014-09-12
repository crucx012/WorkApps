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

        public ActionResult ClaimCell(Piece playerPiece, Boolean isSecondPlayerFirst, int[] board, int claimedSquare)
        {
            var game = new TicTacToe(3, playerPiece, 1);

            if (isSecondPlayerFirst) 
                game.Turn++;

            if (board != null && board[0] != 0) 
                game.ManuallyPopulateCells(board);

            game.TakeTurn(claimedSquare);

            return Json(GetReturnBoard(board,game), JsonRequestBehavior.AllowGet);
        }

        private static List<int> GetReturnBoard(int[] board, TicTacToe game)
        {
            var returnBoard = new List<int>();

            for (int i = 1; i <= 9; i++)
                if ((board == null || board.All(v => v != i)) && game.GetCellValue(i) != Piece.E)
                    returnBoard.Add(i);

            return returnBoard;
        }
    }
}