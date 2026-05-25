
using System.Net.WebSockets;

class Program
{
    public static void Main(string[] args)
    {
        Utility.Welcome();

        while (true)
        {
            MainMenu.Menu();
            int option;
            while (true)
            {
                Console.Write("\nChoose option: ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out option))
                {
                    if (option > 12)
                    {
                        Utility.PrintMessage("Invalid option. Try again.", false);
                    }
                    else break;
                }
                else Utility.PrintMessage("Invalid input. Try again.", false);
            }

            bool End = false;
            switch (option)
            {
                case 1:
                    BusSystem.CreateUser();
                    break;

                case 2:
                    BusSystem.ShowUsers();
                    break;

                case 3:
                    BusSystem.CreateBus();
                    break;

                case 4:
                    BusSystem.ShowBuses();
                    break;

                case 5:
                    break;

                case 6:
                    break;

                case 7:
                    break;

                case 8:
                    break;

                case 9:
                    break;

                case 10:
                    break;

                case 11:
                    break;

                case 12:
                    End = true;
                    break;
            }

            if(End) break;
        }

        Utility.Exit();
    }


}