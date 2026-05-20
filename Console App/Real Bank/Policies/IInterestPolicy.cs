namespace Policies;

public interface IInterestPolicy
{
    decimal ApplyInterest(decimal currentBalance);
}