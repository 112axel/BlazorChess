namespace BlazorChess.Models.Pieces
{
    public class King:Piece
    {
        public King(bool isBlack) : base("king",isBlack)
        {
        }

        public override List<Move> AllowedMoves(Board board ,int x, int y)
        {
            var allAlowedMoves = DiagonalMove(board, x, y, 1); 
            allAlowedMoves.AddRange(LinearMove(board,x,y,1));
            return allAlowedMoves;
        }
    }


}
