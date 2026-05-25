using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

public static class BusSystem
{
    private static readonly List<User> Users = new List<User>();
    private static readonly List<Bus> Buses = new List<Bus>();
    // private List<Schedule> _schedules = new List<Schedule>();

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
        Console.WriteLine("Users Table:\nId\tName\tMobile\tEmail");
        foreach (var user in Users)
        {
            Console.WriteLine($"{user.UserId}\t{user.FullName}\t{user.Mobile}\t{user.Email}");
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

        Classes? busClass;
        while (true)
        {
            var values = Enum.GetValues<Classes>();
            int n = values.Length;
            for(int i = 0; i < n; i++)
            {
                Console.WriteLine($"{i + 1}. {values[i]}");
            }

            Console.Write($"Choose an option (1 - {n}): ");
            int option;

            string? input = Console.ReadLine();
            if (int.TryParse(input, out option))
            {
                if (option < 1 || option > n)
                {
                    Utility.PrintMessage("Invalid option. Try again.", false);
                }
                else
                {
                    busClass = values[option - 1];
                    break;
                }
            }
            else
            {
                Utility.PrintMessage("Invalid option. Try again.", false);
            }
        }

        int seats = 0, price = 0;
        if(busClass is Classes.Economy)
        {
            seats = 35;
            price = 600;
        }
        if(busClass is Classes.Business)
        {
            seats = 28;
            price = 1000;
        }

        var bus = new Bus(coachNo, busClass.ToString(), seats, price);
        Buses.Add(bus);
        Utility.PrintMessage($"\nBus created successfully.", true);
    }

    public static void ShowBuses()
    {
        Console.WriteLine("Buses Table:\nId\tCouch No.\tClass\tSeats\tPrice");
        foreach (var bus in Buses)
        {
            Console.WriteLine($"{bus.BusId}\t{bus.CoachNo}\t{bus.Class}\t{bus.Seats}\ttk: {bus.Price}");
        }
        Console.WriteLine();
    }
}

public enum Classes
{
    Economy,
    Business
}