namespace Policies;

public class OverdraftPolicy : IWithdrawPolicy
{
    private readonly decimal _limit;
    public OverdraftPolicy(decimal limit)
    {
        _limit = limit;
    }
    public bool CanWithdraw(decimal currentBalance, decimal amount)
    {
        return amount > 0 && currentBalance - amount >= -_limit;
    }
    public decimal ApplyWithdrawal(decimal currentBalance, decimal amount)
    {
        return currentBalance - amount;
    }
}