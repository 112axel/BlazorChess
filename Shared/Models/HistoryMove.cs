namespace BlazorChess.Shared.Models
{
    public enum PromotionChoise
    {
        None,
        Queen,
        Rook,
        Bishop,
        Knight
    }
    public class HistoryMove
    {
        public int Id { get; set; }
        public int FromX { get; set; }
        public int FromY { get; set; }
        public int ToX { get; set; }
        public int ToY { get; set; }
        public PromotionChoise PromotionChoise {get; set;}

        public HistoryMove(int fromX, int fromY, int toX, int toY,PromotionChoise promotionChoise = PromotionChoise.None)
        {
            FromX = fromX;
            FromY = fromY;
            ToX = toX;
            ToY = toY;
            PromotionChoise = promotionChoise;
        }
    }
}
