using BlazorChess.Shared.Models.Pieces;

namespace BlazorChess.Shared.Models
{
    public class Board
    {
        private static readonly int XSize = 8;
        private static readonly int YSize = 8;
        public Tile[,] Tiles = new Tile[XSize, YSize];

        private List<Piece> PiecesMoved = new List<Piece> { };

        public bool HasMoved(Piece pieceToFind)
        {
            if (PiecesMoved.Contains(pieceToFind))
            {
                return true;
            }
            return false;
        }

        public void AddMoved(Piece pieceToAdd)
        {
            if (!PiecesMoved.Contains(pieceToAdd))
            {
                PiecesMoved.Add(pieceToAdd);
            }
        }


        public Board()
        {
            for (int x = 0; x < XSize; x++)
            {
                for (int y = 0; y < YSize; y++)
                {
                    Tiles[x, y] = new Tile();
                }
            }
            for (int i = 0; i < 2; i++)
            {
                Tiles[0, 0 + 7 * i].OccupyingPrice = new Rook(i == 0);
                Tiles[1, 0 + 7 * i].OccupyingPrice = new Knight(i == 0);
                Tiles[2, 0 + 7 * i].OccupyingPrice = new Bishop(i == 0);
                Tiles[3, 0 + 7 * i].OccupyingPrice = new Queen(i == 0);
                Tiles[4, 0 + 7 * i].OccupyingPrice = new King(i == 0);
                Tiles[5, 0 + 7 * i].OccupyingPrice = new Bishop(i == 0);
                Tiles[6, 0 + 7 * i].OccupyingPrice = new Knight(i == 0);
                Tiles[7, 0 + 7 * i].OccupyingPrice = new Rook(i == 0);

                for (int j = 0; j < 8; j++)
                {
                    Tiles[j, 1 + 5 * i].OccupyingPrice = new Pawn(i == 0);
                }
            }

        }

        public bool IsValidSquare(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                return false;
            }
            if (x >= XSize || y >= YSize)
            {
                return false;
            }
            return true;
        }

        public bool Move(int fromX, int fromY, int toX, int toY)
        {
            var toMove = Tiles[fromX, fromY].OccupyingPrice;
            if (toMove == null)
            {
                return false;
            }
            if (!toMove.AllowedMoves(this, fromX, fromY).Any(z => z.YDestination == toY && z.XDestination == toX))
            {
                return false;
            }
            AddMoved(toMove);//TODO try to replace

            //premotion code
            if(toMove is Pawn)
            {
                int promotionY = toMove.IsBlack ? 7:0;
                if(toY == promotionY)
                {
                    toMove = new Queen(toMove.IsBlack);
                }
            }

            Tiles[toX, toY].OccupyingPrice = toMove;

            Tiles[fromX, fromY].OccupyingPrice = null;
            return true;
        }


    }
}
