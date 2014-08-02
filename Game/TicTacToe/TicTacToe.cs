using System;

namespace Game
{
    public enum Piece { E = 0, O = 1, X = 2 }

    public class TicTacToe
    {
        private Cell ClaimedCell { get; set; }
        public int Turn { get; set; }
        public Player Player1 = new Player();
        public Player Player2 = new Player();
        public readonly Grid Grid;
        public readonly Cell[,] Cells;
        public readonly int[] Score = { 0, 0, 0 };

        public TicTacToe(int sideLength, Piece firstPlayer, int numberOfNonCpus)
        {
            Cells = new Cell[sideLength, sideLength];
            Grid = new Grid(Cells) { SideLength = sideLength };

            SetCollectionToValue(sideLength);
            SetPlayerPieces(firstPlayer);
            SetPlayerCpuFlags(numberOfNonCpus);
        }

        private void SetPlayerPieces(Piece firstPlayer)
        {
            Player1.Piece = firstPlayer;
            Player2.Piece = firstPlayer == Piece.X ? Piece.O : Piece.X;
        }

        private void SetCollectionToValue(int side)
        {
            for (var i = 0; i < side; i++)
                for (var j = 0; j < side; j++)
                    Cells[i, j] = new Cell { CurrentValue = Piece.E, Rank = 0, X = i, Y = j };
        }

        private void SetPlayerCpuFlags(int players)
        {
            if (players == 1)
                Player2.IsCpu = true;
            if (players == 0)
                Player1.IsCpu = true;
        }

        public bool SecondPlayerStart()
        {
            var random = new Random();
            var randomNumber = random.Next(0, 2);

            if (randomNumber == 1)
                Turn++;

            return randomNumber == 1;
        }

        public void TakeTurn(int index)
        {
            if (IsCpuTurn())
                CpuChooseCell();
            else
                ConvertIndexToCell(index);

            if (IsCellClaimed()) return;
            ClaimCell();
            Turn++;
        }

        private bool IsCpuTurn()
        {
            return IsFirstPlayersTurn()
                   && Player1.IsCpu
                   || !IsFirstPlayersTurn()
                   && Player2.IsCpu;
        }

        private void CpuChooseCell()
        {
            Cell chosenCell = IsFirstPlayersTurn() ? Player1.AI(Grid, Player1.Piece) : Player2.AI(Grid, Player2.Piece);
            ClaimedCell = Grid.Cells[chosenCell.X, chosenCell.Y];
        }

        private bool IsCellClaimed()
        {
            return ClaimedCell.CurrentValue != Piece.E;
        }

        public void ManuallyPopulateCells(params int[] indexs)
        {
            foreach (int i in indexs)
            {
                ConvertIndexToCell(i);
                ClaimCell();
                Turn++;
            }
        }

        private void ClaimCell()
        {
            if (IsFirstPlayersTurn())
                Player1.SetPiece(ClaimedCell);
            else
                Player2.SetPiece(ClaimedCell);
        }

        public Piece GetCellValue(int i)
        {
            ConvertIndexToCell(i);
            return ClaimedCell.CurrentValue;
        }

        private bool IsFirstPlayersTurn()
        {
            return Turn % 2 == 0;
        }

        private void ConvertIndexToCell(int index)
        {
            var row = 0;

            for (; index > Grid.SideLength; index -= Grid.SideLength)
                row++;

            var column = index - 1;

            ClaimedCell = Cells[row, column];
        }

        public Piece? GetWinner()
        {
            var winner = Grid.GetWinner();
            if (winner == Piece.E && !IsGridFull()) return null;
            Score[(int)winner] += 1;
            return winner;
        }

        private bool IsGridFull()
        {
            return Grid.EmptyCells.Count == 0;
        }

        public int GetScore(Piece playerPiece)
        {
            return Score[(int)playerPiece];
        }

        public void NewGame()
        {
            Turn = 0;
            Grid.Winner = Piece.E;

            foreach (Cell c in Cells)
            {
                c.CurrentValue = Piece.E;
                c.Rank = 0;
            }
        }
    }
}
