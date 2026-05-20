namespace Policies;

public interface IFeePolicy
{
    decimal ApplyFee(decimal currentBalance);
}