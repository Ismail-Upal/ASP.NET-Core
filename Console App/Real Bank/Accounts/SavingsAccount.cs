using Models;
using Transactions;

namespace Accounts;

public class SavingsAccount : BankAccount
{
    public SavingsAccount(string accountNumber, Customer owner, decimal startingBalance = 0)
        : base(accountNumber, owner, startingBalance){}

    public override bool Withdraw(decimal amount, string note = "Withdrawal")
    {
        if(amount <= 0 || amount > Balance) return false;
        Balance -= amount;
        AddTransaction(TransactionType.Withdrawal, -amount, note);
        return true;
    }
}