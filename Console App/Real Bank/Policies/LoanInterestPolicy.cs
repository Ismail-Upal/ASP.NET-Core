namespace Policies;

public class LoanInterestPolicy : IInterestPolicy
{
    private readonly decimal _rate;
    public LoanInterestPolicy(decimal rate)
    {
        _rate = rate;
    }
    public decimal ApplyInterest(decimal currentBalance)
    {
        return currentBalance - (Math.Abs(currentBalance) * _rate);
    }
}