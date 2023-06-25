using BlazorChess.Shared.Models;

namespace BlazorChess.Shared.Models.Pieces
{
    public class Queen : Piece
    {
        public Queen(bool isBlack) : base("queen", isBlack)
        {
        }

        public override List<MoveOption> AllowedMoves(Board board, int x, int y)
        {

            var allAlowedMoves = DiagonalMove(board, x, y);
            allAlowedMoves.AddRange(LinearMove(board, x, y));
            return allAlowedMoves;
        }
    }
}
