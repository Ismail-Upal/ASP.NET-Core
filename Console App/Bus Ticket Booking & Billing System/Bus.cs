public class Bus
{
    private static int _nextId = 0;
    public int BusId;
    public string? CoachNo;
    public string? Class;
    public int Seats;
   
    private List<Schedule> Schedules = new List<Schedule>();

    public Bus(string coachNo, string Class, int seats){
        BusId = _nextId++;
        CoachNo = coachNo;
        this.Class = Class;
        Seats = seats;
    }

    public List<Schedule> GetSchedules()
    {
        return Schedules;
    }
    public List<Schedule> SetSchedules()
    {
        return Schedules;
    }
}