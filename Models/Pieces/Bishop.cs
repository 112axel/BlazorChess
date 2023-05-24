﻿namespace BlazorChess.Models.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(bool isBlack) : base("bishop",isBlack)
        {
        }

        public override List<Move> AllowedMoves(Board board, int x, int y)
        {
            return this.DiagonalMove(board,x, y);
        }
    }
}