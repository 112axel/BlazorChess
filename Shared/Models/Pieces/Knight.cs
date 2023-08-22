using BlazorChess.Shared.Models;

namespace BlazorChess.Shared.Models.Pieces
{
    public class Knight : Piece
    {
        public Knight(bool isBlack) : base("knight", isBlack)
        {
        }

        public override List<MoveOption> AllowedMoves(Board board, int x, int y)
        {
            return KnightMove(board, x, y);
        }

        private List<MoveOption> KnightMove(Board board, int x, int y)
        {
            Tuple<int, int>[] DirectionModifierTable = { Tuple.Create(2, 1), Tuple.Create(-1, 2), Tuple.Create(-2, 1), Tuple.Create(1, 2) };
            List<MoveOption> outputMoves = new List<MoveOption>();
            int distance = 1;

            foreach (var DirectionModifier in DirectionModifierTable)
            {
                for (int number = 0; number <= 1; number++)
                {
                    int moveX = x + distance * DirectionModifier.Item1 * (number == 0 ? -1 : 1);
                    int moveY = y + distance * DirectionModifier.Item2 * (number == 0 ? -1 : 1);

                    if (!board.IsValidSquare(moveX, moveY))
                    {
                        continue;
                    }
                    if (board.Tiles[moveX, moveY].OccupyingPiece == null)
                    {
                        outputMoves.Add(new MoveOption(moveX, moveY));
                    }
                    else if (board.Tiles[moveX, moveY].OccupyingPiece.IsBlack != IsBlack)
                    {
                        outputMoves.Add(new MoveOption(moveX, moveY));
                    }
                }
            }
            return outputMoves;
        }
    }
}
