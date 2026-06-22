using System.Collections.Concurrent;
using Interfaces;
using Repositories;

class Program
{
    public static void Main(string[] args)
    {
        Utility.Welcome();

        var userRepo = new UserRepository();
        var busRepo = new BusRepository();
        var scheduleRepo = new ScheduleRepository();
        var ticketRepo = new TicketRepository();
        var invoiceRepo = new InvoiceRepository();

        IUserService userService = new UserService(userRepo);
        IBusService busService = new BusService(busRepo);
        IScheduleService scheduleService = new ScheduleService(scheduleRepo, busRepo);
        ITicketService ticketService = new TicketService(ticketRepo, userRepo, scheduleRepo, busRepo, invoiceRepo);
        IInvoiceService invoiceService = new InvoiceService(invoiceRepo, scheduleRepo, userRepo);



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
                    userService.CreateUser();
                    break;
                case 2:
                    userService.ShowUsers();
                    break;
                case 3:
                    busService.CreateBus();
                    break;
                case 4:
                    busService.ShowBuses();
                    break;
                case 5:
                    scheduleService.CreateSchedule();
                    break;
                case 6:
                    scheduleService.ShowSchedules();
                    break;
                case 7:
                    scheduleService.ShowScheduleDetails();
                    break;
                case 8:
                    ticketService.BookTicket();
                    break;
                case 9:
                    invoiceService.ShowUserInvoices();
                    break;
                case 10:
                    invoiceService.PayInvoice();
                    break;
                case 11:
                    ticketService.ShowUserTickets();
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





public interface ITicketService
{
    void BookTicket(int userId, int scheduleId, int seatNo);
}

public class TicketService : ITicketService
{
    
    public void BookTicket(int userId, int scheduleId, int seatNo)
    {
        
    }
}