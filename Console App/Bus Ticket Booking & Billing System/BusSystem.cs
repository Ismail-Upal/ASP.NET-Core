using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
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
        if (Buses.Count == 0)
        {
            Utility.PrintMessage("No Bus in the fleet. Create Bus First.", false);
            return;
        }

        int BusId = 0;
        while (true)
        {
            Console.Write("Bus Id: ");
            string? input = Console.ReadLine();
            if (int.TryParse(input, out BusId))
            {
                if (Buses.FirstOrDefault(b => b.BusId == BusId) != null)
                {
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
            string input = Console.ReadLine();
            if(decimal.TryParse(input, out Fare))
            {
                break;
            }
            else Utility.PrintMessage("invalid fare. try again.", false);
        }

        Schedule NewSchedule = new Schedule(BusId, DepartureCity, ArrivalCity, DepartureDate, DepartureTime, Fare);
        Schedules.Add(NewSchedule);
        Utility.PrintMessage("Schedule added successfully", true);
    }

    
    public static void ShowSchedules()
    {
        Console.WriteLine("\t------- Schedules -------:\n\t");
        Console.WriteLine(
            "{0, -5} {0, -5} {1, -15} {2, -15} {3, -10} {4, -10} {5, -10}",
            "Id", "BusId", "From", "To", "Date", "Time", "Price"
        );
        Console.WriteLine(new string('-', 65));

        foreach (var schedule in Schedules)
        {   
            Console.WriteLine(
                "{0, -5} {0, -5} {1, -15} {2, -15} {3, -10} {4, -10} {5, -10}",
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
        Bus? Bus;

        int ScheduleId;
        while (true)
        {
            Console.Write("Schedule Id: ");
            string? input = Console.ReadLine();
            if(int.TryParse(input, out ScheduleId))
            {
                schedule = Schedules.FirstOrDefault(s => s.ScheduleId == ScheduleId);
            
                if(schedule != null)
                {
                    Bus = Buses.FirstOrDefault(b => b.BusId == schedule.BusId);
                    break;
                }
                else
                {
                    Utility.PrintMessage("Invalid id. try again.", false);
                }
            }
            else
            {
                Utility.PrintMessage("Invalid id. try again.", false);
            }
        }

        Console.WriteLine("---- Schedule Details -----");
        Console.WriteLine($"Schedule Id : {ScheduleId}");
        Console.WriteLine($"Bus Id : {Bus.BusId} | Coach No: {Bus.CoachNo} | Type: {Bus.BusClass}");
        Console.WriteLine($"From: {schedule.DepartureCity} To: {schedule.ArrivalCity}"); 
        Console.WriteLine($"Date: {schedule.DepartureDate} Time: {schedule.DepartureTime}"); 
        Console.WriteLine($"Taka: {schedule.Fare} Total Seats: {Bus.Seats}"); 

        Console.WriteLine("Seat Layout (X = Booked, [ ] = available):");
        if(Bus.BusClass == BusClasses.Economy)
        {
            for(int )
        }
        else
        {
            
        }
    }

    // }
    // public static BookTicket()
    // {

    // }
    // public static ShowUserInvoice() { }
    // public static PayInvoice() { }
    // public static ShowUserTicket() { }

}

