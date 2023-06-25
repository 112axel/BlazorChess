using BlazorChess.Shared.Models;

namespace BlazorChess.Shared.Models.Pieces
{
    public class Rook : Piece
    {
        public Rook(bool isBlack) : base("rook", isBlack)
        {
        }

        public override List<MoveOption> AllowedMoves(Board board, int x, int y)
        {
            return LinearMove(board, x, y);
        }
    }
}
