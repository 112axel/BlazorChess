﻿namespace BlazorChess.Models.Pieces
{
    public class King:Piece
    {
        public King(string assetPath) : base(assetPath)
        {
        }

        public override List<Move> AllowedMoves(Board board ,int x, int y)
        {
            throw new NotImplementedException();
        }
    }


}
