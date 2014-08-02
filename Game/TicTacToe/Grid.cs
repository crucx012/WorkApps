using System.Collections.Generic;

namespace Game
{
    public class Grid
    {
        public int SideLength { get; set; }
        public Piece Winner { get; set; }
        public Cell[,] Cells { get; set; }

        public Grid(Cell[,] cells)
        {
            Cells = cells;
        }

        public Grid Clone()
        {
            var newGrid = new Grid(Cells){Winner = Winner,SideLength = SideLength,Cells = new Cell[SideLength, SideLength]};

            for (var i = 0; i < SideLength; i++)
                for (var j = 0; j < SideLength; j++)
                    newGrid.Cells[i, j] = new Cell{CurrentValue = Cells[i, j].CurrentValue,Rank = Cells[i, j].Rank,X = i,Y = j};

            return newGrid;
        }

        public List<Cell> EmptyCells
        {
            get
            {
                var emptyCells = new List<Cell>();
                foreach (Cell c in Cells)
                    if (c.CurrentValue == Piece.E)
                        emptyCells.Add(c);
                return emptyCells;
            }
        }

        public Piece GetWinner()
        {
            CheckRowForWinner();
            CheckColumnForWinner();
            CheckDiagonalsForWinner();

            return Winner;
        }

        private void CheckRowForWinner()
        {
            for (var n = 0; n < SideLength; n++)
                if (IsRowMatchingCells(n)
                    && IsCellClaimed(n, 0))
                    Winner = Cells[n, 0].CurrentValue;
        }

        private bool IsRowMatchingCells(int n)
        {
            for (int i = 1; i < SideLength; i++)
                if (Cells[n, 0].CurrentValue != Cells[n, i].CurrentValue)
                    return false;

            return true;
        }

        private void CheckColumnForWinner()
        {
            for (var n = 0; n < SideLength; n++)
                if (IsColumnMatchingCells(n)
                    && IsCellClaimed(0, n))
                    Winner = Cells[0, n].CurrentValue;
        }

        private bool IsColumnMatchingCells(int n)
        {
            for (int i = 1; i < SideLength; i++)
                if (Cells[0, n].CurrentValue != Cells[i, n].CurrentValue)
                    return false;

            return true;
        }

        private void CheckDiagonalsForWinner()
        {
            if (IsDiagonalMatching()
                && IsCellClaimed(0, 0))
                Winner = Cells[0, 0].CurrentValue;
            else if (IsReverseDiagonalMatching()
                     && IsCellClaimed(0, SideLength - 1))
                Winner = Cells[0, SideLength - 1].CurrentValue;
        }

        private bool IsDiagonalMatching()
        {
            for (int n = 1; n < SideLength; n++)
                if (Cells[0, 0].CurrentValue != Cells[n, n].CurrentValue)
                    return false;

            return true;
        }

        private bool IsReverseDiagonalMatching()
        {
            for (int n = 1; n < SideLength; n++)
                if (Cells[0, SideLength - 1].CurrentValue != Cells[n, SideLength - 1 - n].CurrentValue)
                    return false;

            return true;
        }

        private bool IsCellClaimed(int row, int column)
        {
            return Cells[row, column].CurrentValue != Piece.E;
        }
    }
}
