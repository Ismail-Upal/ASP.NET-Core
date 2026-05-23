using ATMApp.UI;

namespace ATMApp.App;

public class Entry
{
    public static void Main(string[] args)
    {
        AppScreen.Welcome();
        long cardNumber = Validator.Convert<long>("Your card number");
        Console.WriteLine($"Your name is {cardNumber}");

        Utility.PressEnterToContinue();
    }
}