namespace BlazorChess.Shared.Models
{
    public record MoveOption
    {
        public int XDestination { get; set; }
        public int YDestination { get; set; }

        public MoveOption(int x, int y)
        {
            XDestination = x;
            YDestination = y;
        }
    }
}
