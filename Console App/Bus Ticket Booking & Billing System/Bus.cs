public class Bus
{
    private static int _nextId = 0;
    public int BusId;
    public string? CoachNo;
    public BusClasses BusClass;
    public int Seats;
   
    public Bus(string coachNo, BusClasses busClass, int seats){
        BusId = _nextId++;
        CoachNo = coachNo;
        BusClass = busClass;
        Seats = seats;
    }

}