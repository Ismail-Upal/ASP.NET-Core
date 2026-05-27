public class Schedule
{
    public string? DepartureCity;
    public string? ArrivalCity;
    public DateOnly DepartureDate;
    public TimeOnly DepartureTime;
    public decimal TicketPrice;

    public Schedule(string departureCity, string arrivalCity, DateOnly departureDate, TimeOnly departureTime, decimal ticketPrice)
    {
        DepartureCity = departureCity;
        ArrivalCity = arrivalCity;
        DepartureDate = departureDate;
        DepartureTime = departureTime;
        TicketPrice = ticketPrice;
    }
    

}