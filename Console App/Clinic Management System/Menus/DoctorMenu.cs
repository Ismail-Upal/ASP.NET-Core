namespace App.Menus;

public class DoctorMenu
{
    public void Menu()
    {
        while (true)
        {
            Console.WriteLine("\n\t==== Doctor Menu ====");
            Console.WriteLine("\t1. Login");
            Console.WriteLine("\t0. Exit");
            Console.ReadKey();
        }
    }
}