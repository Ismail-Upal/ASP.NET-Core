namespace Policies;

public class SavingsInterestPolicy : IInterestPolicy
{
    private readonly decimal _rate;
    public SavingsInterestPolicy(decimal rate)
    {
        _rate = rate;
    }
    public decimal ApplyInterest(decimal currentBalance)
    {
        return currentBalance + (currentBalance * _rate);
    }
}