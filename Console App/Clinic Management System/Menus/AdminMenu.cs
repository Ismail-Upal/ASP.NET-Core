using System;
using App.Models;

namespace App.Menus;

public class AdminMenu
{
    public void Menu()
    {
        while (true)
        {
            Console.WriteLine("\n\t==== Admin Menu ====");
            Console.WriteLine("\t1. Login");
            Console.WriteLine("\t0. Exit");
            Console.Write("Select: ");
            var choice = int.Parse(Console.ReadLine());

            if(choice == 1)
            {
                var auth = new AuthService();
            }
            else if(choice == 0)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid Key");
            }
        }
    }
}