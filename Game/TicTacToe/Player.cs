using System.Collections.Generic;
using System.Linq;

namespace Game
{
    public class Player
    {
        public Piece Piece { get; set; }
        public bool IsCpu { get; set; }

        public void SetPiece(Cell c)
        {
            c.SetCellToPlayerPiece(Piece);
        }

        public Cell AI(Grid g, Piece p)
        {
            Cell bestCell = null;
            List<Cell> emptyCells = g.EmptyCells;

            for (int i = 0; i < emptyCells.Count(); i++)
            {
                var newGrid = g.Clone();
                Cell newCell = emptyCells[i];
                newGrid.Cells[newCell.X, newCell.Y].SetCellToPlayerPiece(p);

                if (newGrid.GetWinner() == Piece.E
                    && newGrid.EmptyCells.Count > 0)
                {
                    var tempCell = AI(newGrid, (p == Piece.X ? Piece.O : Piece.X));
                    newCell.Rank = tempCell.Rank;
                }
                else
                {
                    if (newGrid.GetWinner() == Piece)
                        newCell.Rank = 1;
                    else if (newGrid.GetWinner() == Piece.E)
                        newCell.Rank = 0;
                    else if (newGrid.GetWinner() != Piece)
                        newCell.Rank = -1;
                }
                if (bestCell == null
                    || p == Piece
                    && newCell.Rank > bestCell.Rank
                    || p != Piece
                    && newCell.Rank < bestCell.Rank)
                    bestCell = newCell;
            }
            return bestCell;
        }
    }
}
