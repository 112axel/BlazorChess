using BlazorChess.Models.Pieces;

namespace BlazorChess.Models
{
    public class Board
    {
        private static readonly int XSize = 8;
        private static readonly int YSize = 8;
        public Tile[,] Tiles = new Tile[XSize,YSize];

        public Board()
        {
            for(int x = 0; x < XSize; x++)
            {
                for(int y = 0; y < YSize; y++)
                {
                    Tiles[x, y] = new Tile();
                }
            }

            Tiles[5, 5].OccupyingPrice = new Bishop(true);
            Tiles[2, 2].OccupyingPrice = new King(false);
            Tiles[4, 4].OccupyingPrice = new Knight(false);
            Tiles[3, 3].OccupyingPrice = new Pawn(false);

        }

        public bool IsValidSquare(int x, int y)
        {
            if(x < 0 || y < 0)
            {
                return false;
            }
            if(x>=XSize || y >= YSize)
            {
                return false;
            }
            return true;
        }

        public bool Move(int fromX, int fromY, int toX, int toY)
        {
            var toMove = Tiles[fromX, fromY].OccupyingPrice;
            if(toMove == null)
            {
                return false;
            }
            if(!toMove.AllowedMoves(this, fromX, fromY).Any(z => z.YDestination == toY && z.XDestination == toX))
            {
                return false;
            }
            
            Tiles[toX, toY].OccupyingPrice = toMove;

            Tiles[fromX, fromY].OccupyingPrice = null;
            return true;
        }


    }
}
