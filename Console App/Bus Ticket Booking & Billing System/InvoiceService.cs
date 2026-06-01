public static class InvoiceService
{
    public static List<Invoice> Invoices = new List<Invoice>();

    public static void ShowUserInvoices()
    {
        int UserId;
        while (true)
        {
            Console.Write("Enter User Id (or 0 to cancel): ");
            string? input = Utility.ReadLineOrCancel();
            if (input == null) return;

            if (int.TryParse(input, out UserId))
            {
                if (UserService.Users.FirstOrDefault(u => u.UserId == UserId) != null)
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

        var userInvoices = Invoices.Where(i => i.UserId == UserId).ToList();

        if (userInvoices.Count == 0)
        {
            Utility.PrintMessage("No invoices found for this user.", false);
            return;
        }

        Console.WriteLine("------ Invoices List -----");
        Console.WriteLine("{0, -5} {1, -10} {2, -10} {3, -10} {4, -10} {5, -5}", "ID", "Ticket Id", "Amount", "Date", "Time", "Paid");

        foreach (var invoice in userInvoices)
        {
            string stat = invoice.IsPaid ? "Yes" : "No";
            Console.WriteLine("{0, -5} {1, -10} {2, -10} {3, -10} {4, -10} {5, -5}",
                invoice.InvoiceId,
                invoice.TicketId,
                invoice.Amount,
                invoice.Date,
                invoice.Time,
                stat
            );
        }
    }

    public static void PayInvoice()
    {
        int InvoiceId;
        Invoice? invoice;
        while (true)
        {
            Console.Write("Enter invoice id to pay (or 0 to cancel): ");
            string? input = Utility.ReadLineOrCancel();
            if (input == null) return;

            if (int.TryParse(input, out InvoiceId))
            {
                invoice = Invoices.FirstOrDefault(i => i.InvoiceId == InvoiceId);
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

        Schedule? schedule = ScheduleService.Schedules.FirstOrDefault(s => s.ScheduleId == invoice.ScheduleId);
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