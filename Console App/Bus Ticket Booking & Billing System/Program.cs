class Program
{
    public static void Main(string[] args)
    {
        Utility.Welcome();

        while (true)
        {
            int option;
            while (true)
            {
                MainMenu.Menu();
                Console.Write("\nChoose option (1 to 12): ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out option))
                {
                    if (option < 1 || option > 12)
                    {
                        Utility.PrintMessage("Invalid option. Try again.", false);
                    }
                    else break;
                }
                else Utility.PrintMessage("Invalid input. Try again.", false);
            }

            bool End = false;
            switch (option)
            {
                case 1:
                    UserService.CreateUser();
                    break;
                case 2:
                    UserService.ShowUsers();
                    break;
                case 3:
                    BusService.CreateBus();
                    break;
                case 4:
                    BusService.ShowBuses();
                    break;
                case 5:
                    ScheduleService.CreateSchedule();
                    break;
                case 6:
                    ScheduleService.ShowSchedules();
                    break;
                case 7:
                    ScheduleService.ShowScheduleDetails();
                    break;
                case 8:
                    TicketService.BookTicket();
                    break;
                case 9:
                    InvoiceService.ShowUserInvoices();
                    break;
                case 10:
                    InvoiceService.PayInvoice();
                    break;
                case 11:
                    TicketService.ShowUserTickets();
                    break;
                case 12:
                    End = true;
                    break;
            }

            if (End) break;
        }

        Utility.Exit();
    }
}