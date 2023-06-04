namespace BlazorChess.Models.Pieces
{
    public class Knight : Piece
    {
        public Knight(bool isBlack) : base("knight", isBlack)
        {
        }

        public override List<Move> AllowedMoves(Board board, int x, int y)
        {
            return KnightMove(board,x,y);
        }

        private List<Move> KnightMove(Board board,int x, int y)
        {
            Tuple<int, int>[] DirectionModifierTable = { Tuple.Create(2, 1), Tuple.Create(-1, 2), Tuple.Create(-2, 1), Tuple.Create(1, 2) };
            List<Move> outputMoves = new List<Move>();
            int distance = 1;

            foreach (var DirectionModifier in DirectionModifierTable)
            {
                for(int number = 0; number <= 1 ; number++)
                {
                    int moveX = x + distance * DirectionModifier.Item1*(number == 0?-1:1);
                    int moveY = y + distance * DirectionModifier.Item2*(number == 0?-1:1);

                    if (!board.IsValidSquare(moveX, moveY))
                    {
                        continue;
                    }
                    if (board.Tiles[moveX, moveY].OccupyingPrice == null)
                    {
                        outputMoves.Add(new Move(moveX, moveY));
                    }
                    else if (board.Tiles[moveX, moveY].OccupyingPrice.IsBlack != IsBlack)
                    {
                        outputMoves.Add(new Move(moveX, moveY));
                        continue;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return outputMoves;
        }
    }
}
