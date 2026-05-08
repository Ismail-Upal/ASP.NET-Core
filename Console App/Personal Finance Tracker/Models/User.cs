using System.Collections.Generic;

namespace FinanceTracker.Models;

public class User
{
    private static int nextId = 1001;

    public string AccountNumber { get; set; }
    public string Pin { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public double Balance { get; set; }
    public List<Transaction> Transactions { get; set; }

    public User()
    {
        Transactions = new List<Transaction>();
    }

    public User(string name, string email, string pin)
    {
        Name = name;
        Email = email;
        Pin = pin;
        Balance = 0;
        Transactions = new List<Transaction>();

        AccountNumber = $"AC{nextId}";
        nextId++;
    }

    public static void SetNextId(int next)
    {
        nextId = next;
    }
}