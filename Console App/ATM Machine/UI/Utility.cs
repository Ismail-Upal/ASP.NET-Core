namespace ATMApp.UI;

public static class Utility
{
    public static void PrintMessage(string msg, bool success = true)
    {
        if (success)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        Console.WriteLine(msg);
        Console.ForegroundColor = ConsoleColor.White;
        PressEnterToContinue();
    }
    public static string GetUserInput(string prompt)
    {
        Console.Write($"Enter {prompt}: ");
        return Console.ReadLine();
    }
    public static void PressEnterToContinue()
    {
        Console.WriteLine("\nPress Enter to continue...\n");
        Console.ReadLine();
    }
}