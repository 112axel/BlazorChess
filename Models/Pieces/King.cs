namespace BlazorChess.Models.Pieces
{
    public class King:Piece
    {
        bool HasMoved = false;

        public override List<Move> AllowedMoves(Board board ,int x, int y)
        {
            throw new NotImplementedException();
        }
    }


}
