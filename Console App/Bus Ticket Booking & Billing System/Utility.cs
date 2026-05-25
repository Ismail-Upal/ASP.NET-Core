using System.Runtime.InteropServices;

public static class Utility
{
    public static void Welcome()
    {
        Console.Clear();
        Console.Title = "My Bus Booking & Billing System";
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\n\n----------- Welcome to My Bus Ticket Booking App -----------");
        PressEnterToContinue();
    }
    public static void PressEnterToContinue()
    {
        Console.Write("\n\nPress Enter to continue...");
        Console.ReadLine();
        Console.ReadLine();
    }

    public static void PrintMessage(string msg, bool success)
    {
        if (success)
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        Console.WriteLine(msg);
        Console.ForegroundColor = ConsoleColor.White;
        PressEnterToContinue();
    }

    public static void Exit()
    {
        Console.WriteLine("\n\nThanks for using our services");
        PressEnterToContinue();
    }
}