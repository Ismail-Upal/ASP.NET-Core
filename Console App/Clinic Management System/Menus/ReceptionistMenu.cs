namespace App.Menus;

public class ReceptionistMenu
{
    public void Menu()
    {
        while (true)
        {
            Console.WriteLine("\n\t==== Receptionist Menu ====");
            Console.WriteLine("\t1. Login");
            Console.WriteLine("\t0. Exit");
            Console.ReadKey();
        }
    }
}