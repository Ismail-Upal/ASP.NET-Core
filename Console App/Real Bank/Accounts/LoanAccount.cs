using Models;

namespace Accounts;

public class LoanAccount : BankAccount
{
    public decimal InterestRate { get; protected set; } = 0.05m;

    public LoanAccount(string accountNumber, Customer owner, decimal loanAmount)
        : base(accountNumber, owner, -loanAmount) { }

    public override bool Withdraw(decimal amount, string note = "Withdrawal")
    {
        return false; 
    }
}