public static class ScheduleService
{
    public static List<Schedule> Schedules = new List<Schedule>();

    public static void CreateSchedule()
    {
        if (BusService.Buses.Count == 0)
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
                Bus? bus = BusService.Buses.FirstOrDefault(b => b.BusId == BusId);
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
            Console.Write("Schedule Id : ");
            string? input = Console.ReadLine();
            if (input == null) return;

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

            bus = BusService.Buses.FirstOrDefault(b => b.BusId == schedule.BusId);

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
        Console.WriteLine("Seat Layout (X = Paid, B = Booked, [ ] = available):");

        int row = schedule.Seats.GetLength(0);
        int col = schedule.Seats.GetLength(1);

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                var seat = schedule.Seats[i, j];
                char status = seat.IsPaid ? 'X' : (seat.IsBooked ? 'B' : ' ');

                Console.Write($"[{status}:{seat.SeatNo}]");
                if (bus.BusClass == BusClasses.Economy && j == 1) Console.Write("\t\t");
                if (bus.BusClass == BusClasses.Business && j == 0) Console.Write("\t\t");
            }
            Console.WriteLine();
        }
    }
}