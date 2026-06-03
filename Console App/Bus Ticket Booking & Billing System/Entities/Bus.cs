public class Bus
{
    private static int _nextId = 1;
    public int BusId { get; set; }
    public string CoachNo { get; set; }
    public BusClasses BusClass { get; set; }
    public int Seats { get; set; }
    public int Rows { get; set; }
    public int Cols { get; set; }

    public Bus(string coachNo, BusClasses busClass)
    {
        BusId = _nextId++;
        CoachNo = coachNo;
        BusClass = busClass;

        if (busClass == BusClasses.Economy)
        {
            Seats = 37;
            Rows = 9;
            Cols = 4;
        }
        else 
        {
            Seats = 28;
            Rows = 9;
            Cols = 3;
        }
    }
}