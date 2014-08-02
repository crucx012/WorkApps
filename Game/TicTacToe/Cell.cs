namespace Game
{
    public class Cell
    {
        public Piece CurrentValue { get; set; }
        public int Rank { get; set; }
        public int X;
        public int Y;

        public void SetCellToPlayerPiece(Piece playerPiece)
        {
            CurrentValue = playerPiece;
        }
    }
}
