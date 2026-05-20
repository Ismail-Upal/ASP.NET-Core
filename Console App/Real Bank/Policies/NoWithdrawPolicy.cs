namespace Policies;

public class NoWithdrawPolicy : IWithdrawPolicy
{
    public bool CanWithdraw(decimal currentBalance, decimal amount)
    {
        return false;
    }
    public decimal ApplyWithdrawal(decimal currentBalance, decimal amount)
    {
        return currentBalance;
    }
}