namespace Policies;

public class NoOverdraftPolicy : IWithdrawPolicy
{
    public bool CanWithdraw(decimal currentBalance, decimal amount)
    {
        return amount > 0 && currentBalance >= amount;
    }
    public decimal ApplyWithdrawal(decimal currentBalance, decimal amount)
    {
        return currentBalance - amount;
    }
}