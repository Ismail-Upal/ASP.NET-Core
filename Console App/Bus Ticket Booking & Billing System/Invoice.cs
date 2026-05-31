public class Invoice
{
    private static int _nextId = 1;
    public int InvoiceId { get; set; }
    public int TicketId { get; set; }
    public int UserId { get; set; }
    public int ScheduleId { get; set; }
    public int SeatId { get; set; }
    public decimal Amount { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public bool IsPaid { get; set; }

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