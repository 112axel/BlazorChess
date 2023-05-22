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


    }
}
