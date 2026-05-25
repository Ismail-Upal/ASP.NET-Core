public class Bus
{
    private static int _nextId = 0;
    public int BusId;
    public string? CoachNo;
    public string? Class;
    public int Seats;
    public decimal Price;

    public Bus(string coachNo, string Class, int seats, int price){
        BusId = _nextId++;
        CoachNo = coachNo;
        this.Class = Class;
        Seats = seats;
        Price = price;
    }
}