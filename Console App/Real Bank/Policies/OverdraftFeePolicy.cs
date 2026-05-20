namespace Policies;

public class OverdraftFeePolicy : IFeePolicy
{
    private readonly decimal _fee;
    public OverdraftFeePolicy(decimal fee)
    {
        _fee = fee;
    }
    public decimal ApplyFee(decimal currentBalance)
    {
        return currentBalance < 0 ? currentBalance - _fee : currentBalance;
    }
}