using System.Data;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Microsoft.VisualBasic;

public static class BusSystem
{
    private static List<User> Users = new List<User>();
    private static List<Bus> Buses = new List<Bus>();
    private static List<Schedule> Schedules = new List<Schedule>();
    // private static List<Ticket> Tickets = new List<Ticket>();
    // private static List<Invoice> Invoices = new List<Invoice>();


    public static void CreateUser()
    {
        string? fullName;
        while (true)
        {
            Console.Write("Name: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Utility.PrintMessage("Name is required. Try again.\n", false);
                continue;
            }
            fullName = input;
            break;
        }

        string? mobile;
        while (true)
        {
            Console.Write("Mobile (11 digits): ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Utility.PrintMessage("Mobile is required. Try again.\n", false);
                continue;
            }

            if (input.Length != 11 || !IsAllDigits(input))
            {
                Utility.PrintMessage("Invalid Mobile. Try again.\n", false);
                continue;
            }

            if (Users.FirstOrDefault(u => u.Mobile == input) != null)
            {
                Utility.PrintMessage("This Mobile is already registered. Try again.\n", false);
                continue;
            }

            mobile = input;
            break;

        }

        string? email;
        while (true)
        {
            Console.Write("Email: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Utility.PrintMessage("Email is requird. Try again.\n", false);
                continue;
            }

            if (!input.Contains("@") || !input.Contains("."))
            {
                Utility.PrintMessage("Invalid Email. Try again.\n", false);
                continue;
            }

            if (Users.FirstOrDefault(u => u.Email == input) != null)
            {
                Utility.PrintMessage("This Email is already registered. Try again.\n", false);
                continue;
            }

            email = input;
            break;

        }

        var newUser = new User(fullName, mobile, email);
        Users.Add(newUser);
        Utility.PrintMessage($"\nUser created successfully.\nWelcom Mr/Ms. {fullName}", true);
    }

    private static bool IsAllDigits(string s)
    {
        foreach (var c in s)
        {
            if (!char.IsDigit(c)) return false;
        }
        return true;
    }

    public static void ShowUsers()
    {
        Console.WriteLine("----------- Users -----------");
        Console.WriteLine("{0, -5} {1, -15} {2, -15} {3, -20}", "Id", "Name", "Mobile", "Email");
        foreach (var user in Users)
        {
            Console.WriteLine(
                "{0, -5} {1, -15} {2, -15} {3, -20}",
                user.UserId,
                user.FullName,
                user.Mobile,
                user.Email
            );
        }
        Console.WriteLine();
    }

    public static void CreateBus()
    {
        string? coachNo;
        while (true)
        {
            Console.Write("Coach No: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Utility.PrintMessage("Coach No. is required. Try again.", false);
                continue;
            }

            if (Buses.FirstOrDefault(b => b.CoachNo == input) != null)
            {
                Utility.PrintMessage("This Coach NO. is already registerd. Try again.", false);
                continue;
            }

            coachNo = input;
            break;
        }

        BusClasses busClass;
        int seats = 0;




        int option = 0;
        while (true)
        {
            Console.WriteLine("1. Economic");
            Console.WriteLine("2. Business");
            Console.Write("Select BusClass (1 to 2): ");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out option))
            {
                if (option < 0 || option > 2)
                {
                    Utility.PrintMessage("Invalid Option. Try again", false);
                }
                else break;
            }
            else
            {
                Utility.PrintMessage("Invalid Option. Try again", false);
            }
        }

        if (option == 1)
        {
            busClass = BusClasses.Economy;
            seats = 35;
        }
        else
        {
            busClass = BusClasses.Business;
            seats = 28;
        }

        var bus = new Bus(coachNo, busClass, seats);
        Buses.Add(bus);
        Utility.PrintMessage($"\nBus created successfully.", true);

    }

    public static void ShowBuses()
    {
        Console.WriteLine("-------- Buses --------");
        Console.WriteLine("{0,-5} {1,-15} {2,-12} {3,-8}", "Id", "Coach No.", "Class", "Seats");
        Console.WriteLine(new string('-', 45));

        foreach (var bus in Buses)
        {
            Console.WriteLine(
                "{0,-5} {1,-15} {2,-12} {3,-8}",
                bus.BusId,
                bus.CoachNo,
                bus.BusClass,
                bus.Seats
            );
        }

        Console.WriteLine();
    }


    public static void CreateSchedule()
    {
        int row = 0, col = 0;
        if (Buses.Count == 0)
        {
            Utility.PrintMessage("No Bus in the fleet. Create Bus First.", false);
            return;
        }

        int BusId;
        while (true)
        {
            Console.Write("Bus Id: ");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out BusId))
            {
                Bus? bus = Buses.FirstOrDefault(b => b.BusId == BusId);
                if (bus != null)
                {
                    if(bus.BusClass == BusClasses.Economy){
                        row = 9;col = 4;
                    }
                    else
                    {
                        row = 9; col = 3;
                    }
                    break;
                }
                else
                {
                    Utility.PrintMessage("Invalid Bus Id. It does'nt exist. Try again.", false);
                }
            }
            else
            {
                Utility.PrintMessage("Invalid input", false);
            }
        }

        string? DepartureCity, ArrivalCity;
        while (true)
        {
            Console.Write("Departure City: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Utility.PrintMessage("this section is required. try again.", false);
            }
            else
            {
                DepartureCity = input;
                break;
            }
        }
        while (true)
        {
            Console.Write("Arrival City: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Utility.PrintMessage("this section is required. try again.", false);
            }
            else
            {
                ArrivalCity = input;
                break;
            }
        }

        DateOnly DepartureDate;
        while (true)
        {
            Console.Write("Departure Date: ");
            string? input = Console.ReadLine();
            if(DateOnly.TryParse(input, out DepartureDate))
            {
                break;
            }
            else Utility.PrintMessage("invalid date. try again", false);
        }
        TimeOnly DepartureTime;
        while (true)
        {
            Console.Write("Departure Time: ");
            string? input = Console.ReadLine();
            if(TimeOnly.TryParse(input, out DepartureTime))
            {
                break;
            }
            else Utility.PrintMessage("invalid date. try again", false);
        }

        decimal Fare;
        while (true)
        {
            Console.Write("Fare: ");
            string? input = Console.ReadLine();
            if(decimal.TryParse(input, out Fare))
            {
                break;
            }
            else Utility.PrintMessage("invalid fare. try again.", false);
        }
        
        Schedule NewSchedule = new Schedule(BusId, DepartureCity, ArrivalCity, DepartureDate, DepartureTime, Fare);
        NewSchedule.GenerateSeat(row, col);
        Schedules.Add(NewSchedule);
        Utility.PrintMessage("Schedule added successfully", true);
    }

    
    public static void ShowSchedules()
    {
        Console.WriteLine("------- Schedules -------");
        Console.WriteLine(
            "{0, -5} {1, -5} {2, -15} {3, -15} {4, -10} {5, -10} {6, -10}",
            "Id", "BusId", "From", "To", "Date", "Time", "Price"
        );
        Console.WriteLine(new string('-', 65));

        foreach (var schedule in Schedules)
        {   
            Console.WriteLine(
                "{0, -5} {1, -5} {2, -15} {3, -15} {4, -10} {5, -10} {6, -10}",
                schedule.ScheduleId,
                schedule.BusId,
                schedule.DepartureCity,
                schedule.ArrivalCity,
                schedule.DepartureDate,
                schedule.DepartureTime,
                schedule.Fare
            );
        }
        Console.WriteLine();
    }

    public static void ShowScheduleDetails()
    {
        Schedule? schedule;
        Bus? bus;

        int scheduleId;

        while (true)
        {
            Console.Write("Schedule Id: ");
            string? input = Console.ReadLine();

            if (!int.TryParse(input, out scheduleId))
            {
                Utility.PrintMessage("Invalid id. Try again.", false);
                continue;
            }

            schedule = Schedules.FirstOrDefault(s => s.ScheduleId == scheduleId);

            if (schedule == null)
            {
                Utility.PrintMessage("Invalid id. Try again.", false);
                continue;
            }

            bus = Buses.FirstOrDefault(b => b.BusId == schedule.BusId);

            if (bus == null)
            {
                Utility.PrintMessage("Bus not found for this schedule.", false);
                return;
            }

            break;
        }

        Console.WriteLine("---- Schedule Details -----");
        Console.WriteLine($"Schedule Id : {schedule.ScheduleId}");
        Console.WriteLine($"Bus Id : {bus.BusId} | Coach No: {bus.CoachNo} | Type: {bus.BusClass}");
        Console.WriteLine($"From: {schedule.DepartureCity} To: {schedule.ArrivalCity}");
        Console.WriteLine($"Date: {schedule.DepartureDate} Time: {schedule.DepartureTime}");
        Console.WriteLine($"Fare: {schedule.Fare}");
        Console.WriteLine($"Total Seats: {schedule.Seats?.Length ?? 0}");

        if (schedule.Seats == null)
        {
            Console.WriteLine("Seats not generated.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Seat Layout (X = Booked, [ ] = available):");

        int row = schedule.Seats.GetLength(0);
        int col = schedule.Seats.GetLength(1);

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                var seat = schedule.Seats[i, j];
                char status = seat.IsBooked ? 'X' : ' ';

                Console.Write($"[{status}:{seat.SeatNo}]");
                if(bus.BusClass == BusClasses.Economy && j == 1) Console.Write("\t\t");
                if(bus.BusClass == BusClasses.Business && j == 0) Console.Write("\t\t");
            }
            Console.WriteLine();
        }
    }
    public static void BookTicket()
    {
        int UserId;
        while (true)
        {
            Console.Write("Enter user ID: ");
            string? input = Console.ReadLine();
            if(int.TryParse(input, out UserId))
            {
                if(Users.FirstOrDefault(u => u.UserId == UserId) != null)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("User Id dont exist. try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input.try again.");
            }
        }
        Schedule? schedule;
        int row, col;
        int ScheduleId;
        while (true)
        {
            Console.Write("Enter schedule ID: "); 
            string? input = Console.ReadLine();
            if(int.TryParse(input, out ScheduleId))
            {
                schedule = Schedules.FirstOrDefault(s => s.ScheduleId == ScheduleId);
                if(schedule != null)
                {
                    row = schedule.Seats.GetLength(0);
                    col = schedule.Seats.GetLength(1);
                    break;
                }
                else
                {
                    Console.WriteLine("Schedule Id dont exixt. try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. try again.");
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
            if(seats.Contains(input) == true)
            {
                SeatNo = input;   
            }
            else
            {
                Console.WriteLine("invalid input. try again.");
                continue;
            }

            int i = (int)(SeatNo[0] - '0') - 1;
            int j = SeatNo[1] - 'A';

            if(schedule.Seats[i, j].IsBooked)
            {
                Console.WriteLine("Seat is already booked. choose another seat.");
                continue;
            }
               
        }
    }
    // public static ShowUserInvoice() { }
    // public static PayInvoice() { }
    // public static ShowUserTicket() { }

}

