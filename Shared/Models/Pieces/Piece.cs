using BlazorChess.Shared.Models;

namespace BlazorChess.Shared.Models.Pieces
{
    public abstract class Piece
    {
        public bool IsBlack { get; set; }

        public abstract List<MoveOption> AllowedMoves(Board board, int x, int y);

        public string AssetPath { get; }

        public Piece(string pieceName, bool isBlack)
        {
            IsBlack = isBlack;

            const string basePath = "Asset/Pieces/";

            pieceName = (IsBlack ? "b_" : "w_") + pieceName;
            pieceName += "_svg_NoShadow.svg";
            AssetPath = basePath + pieceName;
        }
        //TODO move validation to a new method

        public List<MoveOption> DiagonalMove(Board board, int x, int y, int maxMoveLength = 1000)
        {
            //TODO consider making a tuple alternative class
            Tuple<int, int>[] DirectionModifierTable = { Tuple.Create(1, 1), Tuple.Create(1, -1), Tuple.Create(-1, 1), Tuple.Create(-1, -1) };
            List<MoveOption> outputMoves = new List<MoveOption>();
            foreach (var DirectionModifier in DirectionModifierTable)
            {
                for (int distance = 1; distance <= maxMoveLength; distance++)
                {
                    int moveX = x + distance * DirectionModifier.Item1;
                    int moveY = y + distance * DirectionModifier.Item2;

                    if (!board.IsValidSquare(moveX, moveY))
                    {
                        break;
                    }
                    if (board.Tiles[moveX, moveY].OccupyingPiece == null)
                    {
                        outputMoves.Add(new MoveOption(moveX, moveY));
                    }
                    else if (board.Tiles[moveX, moveY].OccupyingPiece.IsBlack != IsBlack)
                    {
                        outputMoves.Add(new MoveOption(moveX, moveY));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return outputMoves;
        }

        public List<MoveOption> LinearMove(Board board, int x, int y, int maxMoveLength = 1000)
        {
            //TODO consider making a tuple alternative class
            Tuple<int, int>[] DirectionModifierTable = { Tuple.Create(1, 0), Tuple.Create(0, 1), Tuple.Create(-1, 0), Tuple.Create(0, -1) };
            List<MoveOption> outputMoves = new List<MoveOption>();
            foreach (var DirectionModifier in DirectionModifierTable)
            {
                for (int distance = 1; distance <= maxMoveLength; distance++)
                {
                    int moveX = x + distance * DirectionModifier.Item1;
                    int moveY = y + distance * DirectionModifier.Item2;

                    if (!board.IsValidSquare(moveX, moveY))
                    {
                        break;
                    }
                    if (board.Tiles[moveX, moveY].OccupyingPiece == null)
                    {
                        outputMoves.Add(new MoveOption(moveX, moveY));
                    }
                    else if (board.Tiles[moveX, moveY].OccupyingPiece.IsBlack != IsBlack)
                    {
                        outputMoves.Add(new MoveOption(moveX, moveY));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return outputMoves;
        }
    }
}
