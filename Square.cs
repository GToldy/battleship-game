namespace BattleshipGame

{

    public class Square
    {
        public (int x, int y) Position { get; set; }
        public SquareStatus Status { get; set; }

        public Square((int x, int y) coordinates)
        {
            Position = coordinates;
            Status = SquareStatus.Empty;

        }

        public enum SquareStatus
        {
            Empty,
            Ship,
            Hit,
            Missed
        }
    }


}