public class Seat
{
    private static int _nextId = 1;

    public int SeatId { get; set; }
    public string SeatNo { get; set; } = "";
    public bool IsBooked { get; set; }
    public bool IsPaid { get; set; }

    public Seat(int row, int col)
    {
        SeatId = _nextId++;
        SeatNo = $"{row}{(char)('A' + col - 1)}";
        IsBooked = false;
        IsPaid = false;
    }
}