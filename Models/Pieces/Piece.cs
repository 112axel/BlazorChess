namespace BlazorChess.Models.Pieces
{
    public abstract class Piece
    {
        bool IsBlack { get; set; }

        public abstract List<Move> AllowedMoves(Board board, int x, int y);

        public string AssetPath { get; }

        public Piece(string pieceName, bool isBlack)
        {
            IsBlack = isBlack;

            const string basePath = "Asset/Pieces/";

           pieceName = (IsBlack ? "b_" : "w_") + pieceName;
            pieceName += "_svg_NoShadow.svg";
            AssetPath = basePath + pieceName;
        }


        public List<Move> DiagonalMove(Board board, int x, int y, int maxMoveLength = 1000)
        {
            //TODO consider making a tuple alternative class
            Tuple<int, int>[] DirectionModifierTable = { Tuple.Create(1, 1), Tuple.Create(1, -1), Tuple.Create(-1, 1), Tuple.Create(-1, -1) };
            List<Move> outputMoves = new List<Move>();
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
                    if (board.Tiles[moveX, moveY].OccupyingPrice == null)
                    {
                        outputMoves.Add(new Move(moveX, moveY));
                    }
                    else if (board.Tiles[moveX, moveY].OccupyingPrice.IsBlack != IsBlack)
                    {
                        outputMoves.Add(new Move(moveX, moveY));
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

        public List<Move> LinearMove(Board board, int x, int y, int maxMoveLength = 1000)
        {
            //TODO consider making a tuple alternative class
            Tuple<int, int>[] DirectionModifierTable = { Tuple.Create(1, 0), Tuple.Create(0, 1), Tuple.Create(-1, 0), Tuple.Create(0, -1) };
            List<Move> outputMoves = new List<Move>();
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
                    if (board.Tiles[moveX, moveY].OccupyingPrice == null)
                    {
                        outputMoves.Add(new Move(moveX, moveY));
                    }
                    else if (board.Tiles[moveX, moveY].OccupyingPrice.IsBlack != IsBlack)
                    {
                        outputMoves.Add(new Move(moveX, moveY));
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
