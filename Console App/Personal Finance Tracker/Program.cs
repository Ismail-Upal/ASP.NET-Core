using System;
using System.Collections.Generic;
using FinanceTracker.Models;
using FinanceTracker.Services;

class Program
{
    static void RestoreNextUserId(List<User> users)
    {
        int maxId = 1000;

        foreach (var user in users)
        {
            if (!string.IsNullOrWhiteSpace(user.AccountNumber) &&
                user.AccountNumber.StartsWith("AC"))
            {
                string numberPart = user.AccountNumber.Substring(2);

                if (int.TryParse(numberPart, out int id))
                {
                    if (id > maxId)
                    {
                        maxId = id;
                    }
                }
            }
        }

        User.SetNextId(maxId + 1);
    }
    static string ReadRequiredString(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                return input.Trim();
            }

            Console.WriteLine("This field cannot be empty.");
        }
    }

    static double ReadRequiredDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out double value))
            {
                return value;
            }

            Console.WriteLine("Invalid amount.");
        }
    }

    static void UserMenu(User user, UserService userService)
    {
        while (true)
        {
            Console.WriteLine("Choose option 1..6");
            Console.WriteLine("\t1. Add Income");
            Console.WriteLine("\t2. Add Expense");
            Console.WriteLine("\t3. Show Balance");
            Console.WriteLine("\t4. Transaction History");
            Console.WriteLine("\t5. Show Summary");
            Console.WriteLine("\t6. Exit");

            Console.Write("Enter option: ");
            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Invalid option");
                continue;
            }

            if (option == 1)
            {
                double amount = ReadRequiredDouble("Amount: ");
                string category = ReadRequiredString("Category: ");
                string description = ReadRequiredString("Description: ");

                userService.AddIncome(user, amount, category, description, out string message);
                Console.WriteLine(message);
            }
            else if (option == 2)
            {
                double amount = ReadRequiredDouble("Amount: ");
                string category = ReadRequiredString("Category: ");
                string description = ReadRequiredString("Description: ");

                userService.AddExpense(user, amount, category, description, out string message);
                Console.WriteLine(message);
            }
            else if (option == 3)
            {
                Console.WriteLine($"Your current Balance is: {user.Balance}");
            }
            else if (option == 4)
            {
                if (user.Transactions.Count == 0)
                {
                    Console.WriteLine("No transactions yet.");
                }
                else
                {
                    foreach (var t in user.Transactions)
                    {
                        Console.WriteLine($"\t{t}");
                    }
                }
            }
            else if (option == 5)
            {
                Console.WriteLine("----- SUMMARY -----");
                Console.WriteLine($"Total Income: {userService.GetTotalIncome(user)}");
                Console.WriteLine($"Total Expense: {userService.GetTotalExpense(user)}");
                Console.WriteLine($"Balance: {user.Balance}");
                Console.WriteLine($"Transaction Count: {user.Transactions.Count}");

                Transaction latest = userService.GetLatestTransaction(user);
                if (latest != null)
                {
                    Console.WriteLine($"Latest Transaction: {latest}");
                }
                else
                {
                    Console.WriteLine("Latest Transaction: None");
                }

                Console.WriteLine("-------------------");
            }
            else if (option == 6)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid option");
            }

            Console.WriteLine();
        }
    }

    static void MainMenu(UserService userService)
    {
        while (true)
        {
            Console.WriteLine("WELCOME");
            Console.WriteLine("Choose option 1..3");
            Console.WriteLine("\t1. Register");
            Console.WriteLine("\t2. Login");
            Console.WriteLine("\t3. Exit");

            Console.Write("Enter option: ");
            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Invalid option");
                continue;
            }

            if (option == 1)
            {
                string name = ReadRequiredString("Your Name: ");
                string email = ReadRequiredString("Your Email: ");
                string pin = ReadRequiredString("Your PIN: ");

                if (userService.RegisterUser(name, email, pin, out string message))
                {
                    Console.WriteLine(message);
                    Console.WriteLine($"Your AccountNumber is: {userService.FindUserByEmail(email).AccountNumber}");
                }
                else
                {
                    Console.WriteLine(message);
                }
            }
            else if (option == 2)
            {
                string email = ReadRequiredString("Your Email: ");
                string pin = ReadRequiredString("Your PIN: ");

                if (userService.Login(email, pin, out User user, out string message))
                {
                    Console.WriteLine(message);
                    UserMenu(user, userService);
                }
                else
                {
                    Console.WriteLine(message);
                }
            }
            else if (option == 3)
            {
                Console.WriteLine("Thank You");
                break;
            }
            else
            {
                Console.WriteLine("Invalid option");
            }

            Console.WriteLine();
        }
    }

    static void Main(string[] args)
    {
        var storage = new FileStorageService("users.json");
        List<User> users = storage.LoadUsers();

        RestoreNextUserId(users);

        var userService = new UserService(users, storage);

        MainMenu(userService);
    }
}