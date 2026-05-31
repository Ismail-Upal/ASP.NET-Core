using System.Linq.Expressions;

public class Ticket
{
    private static int _nextId = 1;
    public int SeatId;
    public int TicketId;
    public int ScheduleId;
    public int UserId;
    public string SeatNo;
    public string CoachNo;
    public int InvoiceId;

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