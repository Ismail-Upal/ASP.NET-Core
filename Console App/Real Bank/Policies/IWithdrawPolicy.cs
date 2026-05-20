namespace Policies;

public interface IWithdrawPolicy
{
    bool CanWithdraw(decimal currentBalance, decimal amount);
    decimal ApplyWithdrawal(decimal currentBalance, decimal amount);
}