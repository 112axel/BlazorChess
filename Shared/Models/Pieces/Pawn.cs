using BlazorChess.Shared.Models;

namespace BlazorChess.Shared.Models.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(bool isBlack) : base("pawn", isBlack)
        {
        }

        //TODO add a way to check if pawn has moved
        public override List<MoveOption> AllowedMoves(Board board, int x, int y)
        {

            var allAlowedMoves = PawnMove(board, x, y);
            allAlowedMoves.AddRange(KillingMove(board, x, y));
            return allAlowedMoves;
        }

        private List<MoveOption> PawnMove(Board board, int x, int y)
        {
            List<MoveOption> outputMoves = new List<MoveOption>();
            int directionMod = ForwardMultiplier(board, x, y);

            int iterations = (board.HasMoved(this) ? 1 : 2);

            for(int distance = 1; distance<=iterations; distance++)
            {

                int moveX = x;
                int moveY = y + distance * directionMod;

                if (!board.IsValidSquare(moveX, moveY))
                {
                    break;
                }
                if (board.Tiles[moveX, moveY].OccupyingPiece == null)
                {
                    outputMoves.Add(new MoveOption(moveX, moveY));
                }
                else
                {
                    break;
                }
            }

            return outputMoves;
        }

        private List<MoveOption> KillingMove(Board board, int x, int y)
        {
            List<MoveOption> outputMoves = new List<MoveOption>();
            int distance = 1;
            int directionMod = ForwardMultiplier(board, x, y);


            for (int rightLeftMod = 0; rightLeftMod <= 1; rightLeftMod++)
            {
                int moveX = x + distance + rightLeftMod * -2;
                int moveY = y + distance * directionMod;

                if (!board.IsValidSquare(moveX, moveY))
                {
                    continue;
                }
                if (board.Tiles[moveX, moveY].OccupyingPiece == null)
                {
                    continue;
                }
                if (board.Tiles[moveX, moveY].OccupyingPiece.IsBlack != IsBlack)
                {
                    outputMoves.Add(new MoveOption(moveX, moveY));
                }

            }
            return outputMoves;
        }


        private static int ForwardMultiplier(Board board, int x, int y)
        {
            return board.Tiles[x, y].OccupyingPiece.IsBlack ? 1 : -1;
        }
    }
}
