using Interfaces;
using Repositories;

public class InvoiceService : IInvoiceService
{
    private readonly InvoiceRepository _invoiceRepo;
    private readonly ScheduleRepository _scheduleRepo;
    private readonly UserRepository _userRepo;
    public InvoiceService(InvoiceRepository invoiceRepo, ScheduleRepository scheduleRepo, UserRepository userRepo)
    {
        _invoiceRepo = invoiceRepo;
        _scheduleRepo = scheduleRepo;
        _userRepo = userRepo;
    }

    public void ShowUserInvoices()
    {
        int UserId;
        while (true)
        {
            Console.Write("Enter User Id : ");
            string? input = Console.ReadLine();
             

            if (int.TryParse(input, out UserId))
            {
                if (_userRepo.Users.FirstOrDefault(u => u.UserId == UserId) != null)
                {
                    break;
                }
                else
                {
                    Utility.PrintMessage("Invalid User id. Try again.", false);
                }
            }
            else
            {
                Utility.PrintMessage("Invalid Input. Try again.", false);
            }
        }

        var userInvoices = _invoiceRepo.Invoices.Where(i => i.UserId == UserId).ToList();

        if (userInvoices.Count == 0)
        {
            Utility.PrintMessage("No invoices found for this user.", false);
            return;
        }

        Console.WriteLine("\n------ Invoices List -----");
        Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -5}", "InvoiceId", "TicketId", "Amount", "Date", "Time", "Paid");

        foreach (var invoice in userInvoices)
        {
            string stat = invoice.IsPaid ? "Yes" : "No";
            Console.WriteLine("{0, -10} {1, -10} {2, -10} {3, -10} {4, -10} {5, -5}",
                invoice.InvoiceId,
                invoice.TicketId,
                invoice.Amount,
                invoice.Date,
                invoice.Time,
                stat
            );
        }
    }

    public void PayInvoice()
    {
        int InvoiceId;
        Invoice? invoice;
        while (true)
        {
            Console.Write("Enter invoice id to pay : ");
            string? input = Console.ReadLine();
             

            if (int.TryParse(input, out InvoiceId))
            {
                invoice = _invoiceRepo.Invoices.FirstOrDefault(i => i.InvoiceId == InvoiceId);
                if (invoice != null)
                {
                    if (invoice.IsPaid)
                    {
                        Utility.PrintMessage("Invoice is already paid", false);
                    }
                    else break;
                }
                else
                {
                    Utility.PrintMessage("Invalid invoice id. Try again.", false);
                }
            }
            else
            {
                Utility.PrintMessage("Invalid input.", false);
            }
        }

        Schedule? schedule = _scheduleRepo.Schedules.FirstOrDefault(s => s.ScheduleId == invoice.ScheduleId);
        if (schedule == null)
        {
            Utility.PrintMessage("Schedule not found. Cannot mark seat.", false);
            return;
        }

        schedule.MarkSeat(invoice.SeatId);
        invoice.IsPaid = true;
        Utility.PrintMessage("Invoice Paid successfully", true);
    }
}