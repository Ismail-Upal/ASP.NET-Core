using System.Runtime.CompilerServices;

public class Invoice
{
    private static int _nextId = 1;
    public int InvoiceId;
    public int TicketId;
    public int UserId;
    public int ScheduleId;
    public int SeatId;
    public decimal Amount;
    public DateOnly Date;
    public TimeOnly Time;
    public bool IsPaid;


    public Invoice(int userId, int scheduleId, decimal amount, int seatId)
    {
        InvoiceId = _nextId++;
        UserId = userId;
        ScheduleId = scheduleId;
        Amount = amount;
        SeatId = seatId;
        Date = DateOnly.FromDateTime(DateTime.UtcNow);
        Time = TimeOnly.FromDateTime(DateTime.UtcNow);
        IsPaid = false;
    }
}