public class Schedule
{
    private static int _nextId = 1;

    public int ScheduleId { get; set; }
    public int BusId { get; set; }
    public string? DepartureCity { get; set; }
    public string? ArrivalCity { get; set; }
    public DateOnly DepartureDate { get; set; }
    public TimeOnly DepartureTime { get; set; }
    public decimal Fare { get; set; }

    public Seat[,]? Seats { get; set; }

    public Schedule(int busId, string departureCity, string arrivalCity, DateOnly departureDate, TimeOnly departureTime, decimal fare)
    {
        BusId = busId;
        ScheduleId = _nextId++;
        DepartureCity = departureCity;
        ArrivalCity = arrivalCity;
        DepartureDate = departureDate;
        DepartureTime = departureTime;
        Fare = fare;
    }

    public void GenerateSeat(int row, int col)
    {
        Seats = new Seat[row, col];

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                Seats[i, j] = new Seat(i + 1, j + 1);
            }
        }
    }
}