namespace BlazorChess.Models.Pieces
{
    public class Rook : Piece
    {
        public Rook(bool isBlack) : base("rook", isBlack)
        {
        }

        public override List<Move> AllowedMoves(Board board, int x, int y)
        {
            return LinearMove(board, x, y);
        }
    }
}
