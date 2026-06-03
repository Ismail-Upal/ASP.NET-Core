public static class BusService
{
    public static List<Bus> Buses = new List<Bus>();

    public static void CreateBus()
    {
        string? coachNo;
        while (true)
        {
            Console.Write("Coach No: ");
            string? input = Console.ReadLine();
            if (input == null) return;

            if (string.IsNullOrWhiteSpace(input))
            {
                Utility.PrintMessage("Coach No. is required. Try again.", false);
                continue;
            }

            if (Buses.FirstOrDefault(b => b.CoachNo == input) != null)
            {
                Utility.PrintMessage("This Coach No. is already registered. Try again.", false);
                continue;
            }

            coachNo = input;
            break;
        }

        BusClasses busClass;
        int option = 0;
        while (true)
        {
            Console.WriteLine("1. Economic");
            Console.WriteLine("2. Business");
            Console.Write("Select BusClass (1 to 2): ");
            string? input = Console.ReadLine();
            if (input == null) return;

            if (int.TryParse(input, out option))
            {
                if (option < 1 || option > 2)
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

        busClass = (option == 1) ? BusClasses.Economy : BusClasses.Business;

        var bus = new Bus(coachNo, busClass);
        Buses.Add(bus);
        Utility.PrintMessage($"\nBus created successfully.", true);
    }

    public static void ShowBuses()
    {
        Console.WriteLine("-------- Buses --------");
        Console.WriteLine("{0,-5} {1,-15} {2,-12} {3,-8}", "Id", "Coach No.", "Class", "Seats");

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
}