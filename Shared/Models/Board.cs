using BlazorChess.Shared.Models.Pieces;

namespace BlazorChess.Shared.Models
{
    public class Board
    {
        private static readonly int XSize = 8;
        private static readonly int YSize = 8;
        public Tile[,] Tiles = new Tile[XSize, YSize];

        public Point WhiteKingPos { get; set; }
        public Point BlackKingPos { get; set; }

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

        private void CreateTiles()
        {
            for (int x = 0; x < XSize; x++)
            {
                for (int y = 0; y < YSize; y++)
                {
                    Tiles[x, y] = new Tile();
                }
            }
        }


        public Board()
        {
            CreateTiles();
            for (int i = 0; i < 2; i++)
            {
                Tiles[0, 0 + 7 * i].OccupyingPiece = new Rook(i == 0);
                Tiles[1, 0 + 7 * i].OccupyingPiece = new Knight(i == 0);
                Tiles[2, 0 + 7 * i].OccupyingPiece = new Bishop(i == 0);
                Tiles[3, 0 + 7 * i].OccupyingPiece = new Queen(i == 0);
                Tiles[4, 0 + 7 * i].OccupyingPiece = new King(i == 0);
                //easy reference for check
                if(i != 0)
                {
                    WhiteKingPos = new Point(4, 7 * i);
                }
                else
                {
                    BlackKingPos = new Point(4, 7 * i);
                }

                Tiles[5, 0 + 7 * i].OccupyingPiece = new Bishop(i == 0);
                Tiles[6, 0 + 7 * i].OccupyingPiece = new Knight(i == 0);
                Tiles[7, 0 + 7 * i].OccupyingPiece = new Rook(i == 0);

                for (int j = 0; j < 8; j++)
                {
                    Tiles[j, 1 + 5 * i].OccupyingPiece = new Pawn(i == 0);
                }
            }

        }

        private Board(Tile[,] tiles, Point whiteKingPos, Point blackKingPos)
        {
            CreateTiles();

            //deep copy
            for (int x = 0; x < XSize; x++)
            {
                for (int y = 0; y < YSize; y++)
                {
                    Tiles[x, y].OccupyingPiece = tiles[x,y].OccupyingPiece;
                }
            }

            WhiteKingPos = whiteKingPos.Clone();
            BlackKingPos = blackKingPos.Clone();
        }

        public Board Clone()
        {
            return new Board(Tiles, WhiteKingPos,BlackKingPos);
        }

        public bool IsInCheck(bool isBlackTurn)
        {
            //TODO replace this feels bad
            Point target = !isBlackTurn ? WhiteKingPos : BlackKingPos;
            var x = 1;
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 7;j++)
                {
                    if (Tiles[i, j].OccupyingPiece == null)
                    {
                        continue;
                    }
                    if (Tiles[i,j].OccupyingPiece.IsBlack != isBlackTurn)
                    {
                        var piece = Tiles[i, j].OccupyingPiece;
                        var allowedMoves = piece.AllowedMoves(this, i, j);
                        var result = allowedMoves.FirstOrDefault(a=> a.XDestination == target.X && a.YDestination == target.Y);
                        if(result != null)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
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

        public bool Move(int fromX, int fromY, int toX, int toY, PromotionChoise promotionChoise)
        {
            var toMove = Tiles[fromX, fromY].OccupyingPiece;
            if (toMove == null)
            {
                return false;
            }
            if (!toMove.AllowedMoves(this, fromX, fromY).Any(z => z.YDestination == toY && z.XDestination == toX))
            {
                return false;
            }

            //not allowed to move inte check
            if (!NextMoveAllowed(fromX, fromY, toX, toY))
            {
                return false;
            }

            AddMoved(toMove);//TODO try to replace

            //premotion code
            //TODO chose premotion type
            if(toMove is Pawn)
            {
                int promotionY = toMove.IsBlack ? 7:0;
                if(toY == promotionY)
                {
                    if(promotionChoise == PromotionChoise.Queen)
                    {
                        toMove = new Queen(toMove.IsBlack);
                    }
                    else if(promotionChoise == PromotionChoise.Rook)
                    {
                        toMove = new Rook(toMove.IsBlack);
                    }
                    else if(promotionChoise == PromotionChoise.Knight)
                    {
                        toMove = new Knight(toMove.IsBlack);
                    }
                    else if(promotionChoise == PromotionChoise.Bishop)
                    {
                        toMove = new Bishop(toMove.IsBlack);
                    }
                }
            }
            //Update pos of king
            if(toMove is King)
            {
                if(toMove.IsBlack)
                {
                    BlackKingPos = new Point(toX, toY);
                }
                else
                {
                    WhiteKingPos = new Point(toX, toY);
                }
            }

         
            Tiles[toX, toY].OccupyingPiece = toMove;

            Tiles[fromX, fromY].OccupyingPiece = null;




            return true;
        }

        public bool NextMoveAllowed(int fromX, int fromY, int toX, int toY)
        {
            var toMove = Tiles[fromX, fromY].OccupyingPiece;
            if (toMove == null)
            {
                return false;
            }
            //make sure you can not move into check

            var nextMove = Clone();

            nextMove.Tiles[toX, toY].OccupyingPiece = toMove;
            if(toMove is King)
            {
                if (toMove.IsBlack)
                {
                    nextMove.BlackKingPos = new Point(toX, toY);
                }
                else
                {
                    nextMove.WhiteKingPos = new Point(toX, toY);
                }
            }
            nextMove.Tiles[fromX, fromY].OccupyingPiece = null;

            if (nextMove.IsInCheck(toMove.IsBlack))
            {
                return false;
            }

            return true;

        }


    }
}
