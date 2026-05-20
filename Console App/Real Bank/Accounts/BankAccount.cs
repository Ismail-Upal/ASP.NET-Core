using System.Globalization;
using System.Security.AccessControl;
using Models;
using Transactions;

namespace Accounts;

public abstract class BankAccount
{
    public string AccountNumber { get; protected set; }
    public decimal Balance { get; protected set; }
    public Customer Owner { get; protected set; }
    public List<Transaction> Transactions { get; } = new();

    protected BankAccount(string accountNumber, Customer owner, decimal startingBalance = 0)
    {
        AccountNumber = accountNumber;
        Owner = owner;
        Balance = startingBalance;
    }

    public void Deposit(decimal amount, string note = "Deposit")
    {
        if(amount <= 0) throw new ArgumentException("amount must be positive.");
        Balance += amount;
        AddTransaction(TransactionType.Deposit, amount, note);
    }
    
    public abstract bool Withdraw(decimal amount, string note = "Withdrawal");
    protected void AddTransaction(TransactionType type, decimal amount, string note)
    {
        Transactions.Add(new Transaction(type, amount, Balance, note));
    }
}