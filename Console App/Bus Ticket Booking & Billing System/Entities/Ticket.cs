public class Ticket
{
    private static int _nextId = 1;
    public int TicketId { get; set; }
    public int UserId { get; set; }
    public int ScheduleId { get; set; }
    public int SeatId { get; set; }
    public string SeatNo { get; set; }
    public string CoachNo { get; set; }
    public int InvoiceId { get; set; }

    public Ticket(int invoiceId, int userId, int scheduleId, int seatId, string coachNo, string seatNo)
    {
        TicketId = _nextId++;
        UserId = userId;
        ScheduleId = scheduleId;
        SeatId = seatId;
        CoachNo = coachNo;
        SeatNo = seatNo;
        InvoiceId = invoiceId;
    }
}