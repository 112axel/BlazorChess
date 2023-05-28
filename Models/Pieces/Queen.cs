namespace BlazorChess.Models.Pieces
{
    public class Queen : Piece
    {
        public Queen(bool isBlack) : base("queen", isBlack)
        {
        }

        public override List<Move> AllowedMoves(Board board, int x, int y)
        {

            var allAlowedMoves = DiagonalMove(board, x, y); 
            allAlowedMoves.AddRange(LinearMove(board,x,y));
            return allAlowedMoves;
        }
    }
}
