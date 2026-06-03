using Interfaces;
using Repositories;

public class BusService : IBusService
{
    private readonly BusRepository _busRepo;
    public BusService(BusRepository busRepo)
    {
        _busRepo = busRepo;
    }

    public void CreateBus()
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

            if (_busRepo.Buses.FirstOrDefault(b => b.CoachNo == input) != null)
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
        _busRepo.Buses.Add(bus);
        Utility.PrintMessage($"\nBus created successfully.", true);
    }

    public void ShowBuses()
    {
        Console.WriteLine("\n-------- Buses --------");
        Console.WriteLine("{0,-8} {1,-10} {2,-12} {3,-8}", "BusId", "CoachNo.", "Class", "Seats");

        foreach (var bus in _busRepo.Buses)
        {
            Console.WriteLine(
                "{0,-8} {1,-10} {2,-12} {3,-8}",
                bus.BusId,
                bus.CoachNo,
                bus.BusClass,
                bus.Seats
            );
        }
        Console.WriteLine();
    }
}