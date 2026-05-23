using ATMApp.Domain.Entities;

namespace ATMApp.UI;

public class AppScreen
{

    internal const string cur = "$ ";
    internal static void Welcome()
    {
        Console.Clear();
        Console.Title = "My ATM App";
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("\n\n---------------- Welcome to My ATM App ----------------\n\n");
        Console.WriteLine("Please insert your ATM card");
        Console.WriteLine("Note: Actual ATM machine will accept and validate a physical ATM card, read the card number and validate it.");
        Utility.PressEnterToContinue();
    }

    internal static UserAccount UserLoginForm()
    {
        UserAccount tempUserAccount = new UserAccount();
        tempUserAccount.cardNumber = Validator.Convert<long>("your card number.");
        tempUserAccount.CardPin = Convert.ToInt32(Utility.GetUserInput("Enter your card PIN"));
        return tempUserAccount;
    }
    
}
