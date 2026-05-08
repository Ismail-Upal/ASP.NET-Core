using System;
using System.Collections.Generic;
using System.Linq;
using FinanceTracker.Models;

namespace FinanceTracker.Services;

public class UserService
{
    private readonly List<User> _users;
    private readonly FileStorageService _storage;

    public UserService(List<User> users, FileStorageService storage)
    {
        _users = users;
        _storage = storage;
    }

    public User FindUserByEmail(string email)
    {
        return _users.FirstOrDefault(u =>
            u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public bool RegisterUser(string name, string email, string pin, out string message)
    {
        if (FindUserByEmail(email) != null)
        {
            message = "A user with this email already exists.";
            return false;
        }

        var user = new User(name, email, pin);
        _users.Add(user);
        _storage.SaveUsers(_users);

        message = $"Congratulation Mr. {name}";
        return true;
    }

    public bool Login(string email, string pin, out User user, out string message)
    {
        user = FindUserByEmail(email);

        if (user == null)
        {
            message = "You have no account";
            return false;
        }

        if (user.Pin != pin)
        {
            message = "Wrong PIN";
            return false;
        }

        message = "Login successful";
        return true;
    }

    public bool AddIncome(User user, double amount, string category, string description, out string message)
    {
        if (amount <= 0)
        {
            message = "Amount must be greater than zero.";
            return false;
        }

        user.Balance += amount;
        user.Transactions.Add(new Transaction
        {
            Type = TransactionType.Income,
            Amount = amount,
            Category = category,
            Description = description,
            Date = DateTime.Now
        });

        _storage.SaveUsers(_users);
        message = "AddIncome successful";
        return true;
    }

    public bool AddExpense(User user, double amount, string category, string description, out string message)
    {
        if (amount <= 0)
        {
            message = "Amount must be greater than zero.";
            return false;
        }

        if (amount > user.Balance)
        {
            message = "You have insufficient money";
            return false;
        }

        user.Balance -= amount;
        user.Transactions.Add(new Transaction
        {
            Type = TransactionType.Expense,
            Amount = amount,
            Category = category,
            Description = description,
            Date = DateTime.Now
        });

        _storage.SaveUsers(_users);
        message = "AddExpense successful";
        return true;
    }

    public double GetTotalIncome(User user)
    {
        return user.Transactions
            .Where(t => t.Type == TransactionType.Income)
            .Sum(t => t.Amount);
    }

    public double GetTotalExpense(User user)
    {
        return user.Transactions
            .Where(t => t.Type == TransactionType.Expense)
            .Sum(t => t.Amount);
    }

    public Transaction GetLatestTransaction(User user)
    {
        return user.Transactions
            .OrderByDescending(t => t.Date)
            .FirstOrDefault();
    }
}