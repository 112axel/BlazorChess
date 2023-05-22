namespace BlazorChess.Models
{
    public class Move
    {
        public int XDestination { get; set; }
        public int YDestination { get; set; }
        
        public Move(int x,int y)
        {
            XDestination = x;
            YDestination = y;
        }
    }
}
