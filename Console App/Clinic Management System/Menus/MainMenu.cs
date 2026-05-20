
using App.Models;

namespace App.Menus;

public class MainMenu
{
    public void Menu()
    {
        var adminMenu = new AdminMenu();
        var receptionistMenu = new ReceptionistMenu();
        var doctorMenu = new DoctorMenu();
        var labTechMenu = new LabTechMenu();



        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n\t==== Welcome to Curex ====");
            Console.WriteLine("\t1. Login");
            Console.WriteLine("\t0. Exit");
            Console.Write("Select: ");
            var choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {
                Console.WriteLine("\n\t== Select Your Role ==");
                Console.WriteLine("\t1. Admin");
                Console.WriteLine("\t2. Receptionist");
                Console.WriteLine("\t3. Doctor");
                Console.WriteLine("\t4. LabTech");
                Console.WriteLine("\t0. Exit");
                Console.WriteLine("Select: ");
                var opt = int.Parse(Console.ReadLine());

                switch (opt)
                {
                    case 1:
                        adminMenu.Menu();
                        break;
                    case 2:
                        // receptionistMenu.Menu();
                        break;
                    case 3:
                        // doctorMenu.Menu();
                        break;
                    case 4:
                        // labTechMenu.Menu();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Invalid role.");
                        Console.ReadKey();
                        break;
                }
            }
            else if (choice == 0)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid role.");
                Console.ReadKey();
            }
        }
    }
}