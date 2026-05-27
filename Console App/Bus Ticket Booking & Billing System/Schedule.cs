public class Schedule
{
    private static int _nextId = 0;
    public int ScheduleId;
    public int BusId;
    public string? DepartureCity;
    public string? ArrivalCity;
    public DateOnly DepartureDate;
    public TimeOnly DepartureTime;
    public decimal Fare;

    public Schedule(int busId, string departureCity, string arrivalCity, DateOnly departureDate, TimeOnly departureTime, decimal fare)
    {
        BusId = busId;
        ScheduleId = ++_nextId;
        DepartureCity = departureCity;
        ArrivalCity = arrivalCity;
        DepartureDate = departureDate;
        DepartureTime = departureTime;
        Fare = fare;
    }
}