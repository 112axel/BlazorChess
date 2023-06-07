namespace BlazorChess.Models.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(bool isBlack) : base("pawn", isBlack)
        {
        }
        //TODO implement movement
        public override List<Move> AllowedMoves(Board board, int x, int y)
        {

            var allAlowedMoves = PawnMove(board, x, y); 
            allAlowedMoves.AddRange(KillingMove(board,x,y));
            return allAlowedMoves;
        }

        private List<Move> PawnMove(Board board, int x, int y)
        {
            List<Move> outputMoves = new List<Move>();
            int distance = 1;
            int directionMod = ForwardMultiplier(board, x, y);

                int moveX = x;
                int moveY = y + distance * directionMod;

                if (board.IsValidSquare(moveX, moveY))
                {
                }
                if (board.Tiles[moveX, moveY].OccupyingPrice == null)
                {
                    outputMoves.Add(new Move(moveX, moveY));
                }
            return outputMoves;
        }

        private List<Move> KillingMove(Board board, int x, int y)
        {
            List<Move> outputMoves = new List<Move>();
            int distance = 1;
            int directionMod = ForwardMultiplier(board, x, y);


            for(int rightLeftMod = 0; rightLeftMod<=1; rightLeftMod++)
            {
                int moveX = x+distance+(rightLeftMod*-2);
                int moveY = y + distance * directionMod;

                if(!board.IsValidSquare(moveX,moveY))
                {
                    continue;
                }
                if (board.Tiles[moveX,moveY].OccupyingPrice == null)
                {
                    continue;
                }
                if (board.Tiles[moveX,moveY].OccupyingPrice.IsBlack != IsBlack) {
                    outputMoves.Add(new Move(moveX, moveY));
                }

            }
            return outputMoves;
        }


        private int ForwardMultiplier(Board board, int x, int y)
        {
            return board.Tiles[x, y].OccupyingPrice.IsBlack? 1 : -1;
        }
    }
}
