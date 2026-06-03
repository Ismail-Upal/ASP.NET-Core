using Interfaces;
using Repositories;

public class ScheduleService : IScheduleService
{
    private readonly ScheduleRepository _scheduleRepo;
    private readonly BusRepository _busRepo;
    public ScheduleService(ScheduleRepository scheduleRepo, BusRepository busRepo)
    {
        _scheduleRepo = scheduleRepo;
        _busRepo = busRepo;
    }

    public void CreateSchedule()
    {
        if (_busRepo.Buses.Count == 0)
        {
            Utility.PrintMessage("No Bus in the fleet. Create Bus First.", false);
            return;
        }

        int BusId;
        int row = 0, col = 0;
        while (true)
        {
            Console.Write("Bus Id : ");
            string? input = Console.ReadLine();
            if (input == null) return;

            if (int.TryParse(input, out BusId))
            {
                Bus? bus = _busRepo.Buses.FirstOrDefault(b => b.BusId == BusId);
                if (bus != null)
                {
                    row = bus.Rows;
                    col = bus.Cols;
                    break;
                }
                else
                {
                    Utility.PrintMessage("Invalid Bus Id. It doesn't exist. Try again.", false);
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
            Console.Write("Departure City : ");
            string? input = Console.ReadLine();;
            if (input == null) return;

            if (string.IsNullOrWhiteSpace(input))
            {
                Utility.PrintMessage("This section is required. Try again.", false);
            }
            else
            {
                DepartureCity = input;
                break;
            }
        }
        while (true)
        {
            Console.Write("Arrival City : ");
            string? input = Console.ReadLine();;
            if (input == null) return;

            if (string.IsNullOrWhiteSpace(input))
            {
                Utility.PrintMessage("This section is required. Try again.", false);
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
            Console.Write("Departure Date : ");
            string? input = Console.ReadLine();;
            if (input == null) return;

            if (DateOnly.TryParse(input, out DepartureDate))
            {
                break;
            }
            else Utility.PrintMessage("Invalid date. Try again", false);
        }
        TimeOnly DepartureTime;
        while (true)
        {
            Console.Write("Departure Time : ");
            string? input = Console.ReadLine();;
            if (input == null) return;

            if (TimeOnly.TryParse(input, out DepartureTime))
            {
                break;
            }
            else Utility.PrintMessage("Invalid time. Try again", false);
        }

        decimal Fare;
        while (true)
        {
            Console.Write("Fare : ");
            string? input = Console.ReadLine();
            if (input == null) return;

            if (decimal.TryParse(input, out Fare))
            {
                break;
            }
            else Utility.PrintMessage("Invalid fare. Try again.", false);
        }

        Schedule NewSchedule = new Schedule(BusId, DepartureCity, ArrivalCity, DepartureDate, DepartureTime, Fare);
        NewSchedule.GenerateSeat(row, col);
        _scheduleRepo.Schedules.Add(NewSchedule);
        Utility.PrintMessage("Schedule added successfully", true);
    }

    public void ShowSchedules()
    {
        Console.WriteLine("\n------- Schedules -------");
        Console.WriteLine(
            "{0, -12} {1, -7} {2, -12} {3, -12} {4, -10} {5, -10} {6, -10}",
            "ScheduleId", "BusId", "From", "To", "Date", "Time", "Price"
        );

        foreach (var schedule in _scheduleRepo.Schedules)
        {
            Console.WriteLine(
                "{0, -12} {1, -7} {2, -12} {3, -12} {4, -10} {5, -10} {6, -10}",
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

    public void ShowScheduleDetails()
    {
        Schedule? schedule;
        Bus? bus;

        int scheduleId;
        while (true)
        {
            Console.Write("Schedule Id : ");
            string? input = Console.ReadLine();
            if (input == null) return;

            if (!int.TryParse(input, out scheduleId))
            {
                Utility.PrintMessage("Invalid id. Try again.", false);
                continue;
            }

            schedule = _scheduleRepo.Schedules.FirstOrDefault(s => s.ScheduleId == scheduleId);

            if (schedule == null)
            {
                Utility.PrintMessage("Invalid id. Try again.", false);
                continue;
            }

            bus = _busRepo.Buses.FirstOrDefault(b => b.BusId == schedule.BusId);

            if (bus == null)
            {
                Utility.PrintMessage("Bus not found for this schedule.", false);
                return;
            }

            break;
        }

        Console.WriteLine("\n---- Schedule Details -----");
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
        Console.WriteLine("Seat Layout (X = Paid, B = Booked, [ ] = available):");

        int row = schedule.Seats.GetLength(0);
        int col = schedule.Seats.GetLength(1);

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                var seat = schedule.Seats[i, j];
                char status = seat.IsPaid ? 'X' : (seat.IsBooked ? 'B' : ' ');

                Console.Write($"[{status}:{seat.SeatNo}] ");
                if (bus.BusClass == BusClasses.Economy && j == 1) Console.Write("\t");
                if (bus.BusClass == BusClasses.Business && j == 0) Console.Write("\t");
            }
            Console.WriteLine();
        }
    }
}