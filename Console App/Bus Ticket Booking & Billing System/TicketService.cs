public static class TicketService
{
    public static List<Ticket> Tickets = new List<Ticket>();

    public static void BookTicket()
    {
        int UserId;
        while (true)
        {
            Console.Write("Enter user ID : ");
            string? input = Console.ReadLine();
            if (input == null) return;

            if (int.TryParse(input, out UserId))
            {
                if (UserService.Users.FirstOrDefault(u => u.UserId == UserId) != null)
                {
                    break;
                }
                else
                {
                    Utility.PrintMessage("User Id doesn't exist. Try again.", false);
                }
            }
            else
            {
                Utility.PrintMessage("Invalid input. Try again.", false);
            }
        }

        Schedule? schedule;
        int row, col;
        int ScheduleId;
        while (true)
        {
            Console.Write("Enter schedule ID : ");
            string? input = Console.ReadLine();
            if (input == null) return;

            if (int.TryParse(input, out ScheduleId))
            {
                schedule = ScheduleService.Schedules.FirstOrDefault(s => s.ScheduleId == ScheduleId);
                if (schedule != null)
                {
                    row = schedule.Seats.GetLength(0);
                    col = schedule.Seats.GetLength(1);
                    break;
                }
                else
                {
                    Utility.PrintMessage("Schedule Id doesn't exist. Try again.", false);
                }
            }
            else
            {
                Utility.PrintMessage("Invalid input. Try again.", false);
            }
        }

        List<string> seats = new List<string>();
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                seats.Add($"{i + 1}{(char)('A' + j)}");
            }
        }

        string? SeatNo;
        while (true)
        {
            Console.Write("Enter seat number (e.g. 1A, 3C): ");
            string? input = Console.ReadLine();
            if (input == null) return;

            if (seats.Contains(input))
            {
                SeatNo = input;
            }
            else
            {
                Utility.PrintMessage("Invalid input. Try again.", false);
                continue;
            }

            row = int.Parse(SeatNo.Substring(0, SeatNo.Length - 1)) - 1;
            col = SeatNo[SeatNo.Length - 1] - 'A';

            if (schedule.Seats[row, col].IsBooked)
            {
                Utility.PrintMessage("Seat is already booked. Choose another seat.", false);
                continue;
            }
            break;
        }

        Bus? bus = BusService.Buses.FirstOrDefault(b => b.BusId == schedule.BusId);

        int seatId = schedule.Seats[row, col].SeatId;
        Invoice invoice = new Invoice(UserId, ScheduleId, schedule.Fare, seatId);
        Ticket ticket = new Ticket(invoice.InvoiceId, UserId, ScheduleId, seatId, bus.CoachNo, SeatNo);
        invoice.TicketId = ticket.TicketId;

        // Mark seat as booked immediately
        schedule.Seats[row, col].IsBooked = true;

        Tickets.Add(ticket);
        InvoiceService.Invoices.Add(invoice);
        Utility.PrintMessage($"Ticket Booked successfully\nTicket Id: {ticket.TicketId} | Seat: {schedule.Seats[row, col].SeatNo}\nInvoice Id: {invoice.InvoiceId} | Amount: {schedule.Fare}", true);
    }

    public static void ShowUserTickets()
    {
        int UserId;
        while (true)
        {
            Console.Write("Enter User Id : ");
            string? input = Console.ReadLine();
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

        var paidTickets = Tickets.Where(t => t.UserId == UserId)
            .Where(t => InvoiceService.Invoices.Any(i => i.TicketId == t.TicketId && i.IsPaid))
            .ToList();

        if (paidTickets.Count == 0)
        {
            Utility.PrintMessage("No Paid Tickets found for this user.", false);
            return;
        }

        Console.WriteLine("------ Tickets List -----");
        Console.WriteLine("{0, -10} {1, -10} {2, -12} {3, -12} {4, -10}", "ID", "Coach No.", "Date", "Time", "Seat");

        foreach (var ticket in paidTickets)
        {
            Schedule? schedule = ScheduleService.Schedules.FirstOrDefault(s => s.ScheduleId == ticket.ScheduleId);
            if (schedule == null)
            {
                Console.WriteLine("Invalid ticket");
                return;
            }
            Console.WriteLine("{0, -10} {1, -10} {2, -12} {3, -12} {4, -10}",
                ticket.TicketId,
                ticket.CoachNo,
                schedule.DepartureDate,
                schedule.DepartureTime,
                ticket.SeatNo
            );
        }
    }
}