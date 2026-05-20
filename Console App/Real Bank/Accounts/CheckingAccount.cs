using Models;
using Transactions;

namespace Accounts;

public class CheckingAccount : BankAccount
{
    public decimal OverdraftLimit { get; protected set; } = 500m;
    public decimal OverdraftFee { get; protected set; } = 35m;

    public CheckingAccount(string accountNumber, Customer owner, decimal startingBalance = 0)
        : base(accountNumber, owner, startingBalance) { }

    public override bool Withdraw(decimal amount, string note = "Withdrawal")
    {
        if (amount <= 0) return false;

        if (Balance - amount < -OverdraftLimit) return false;

        Balance -= amount;
        AddTransaction(TransactionType.Withdrawal, -amount, note);

        if (Balance < 0)
        {
            Balance -= OverdraftFee;
            AddTransaction(TransactionType.Fee, -OverdraftFee, "Overdraft Fee");
        }
        return true;
    }
}