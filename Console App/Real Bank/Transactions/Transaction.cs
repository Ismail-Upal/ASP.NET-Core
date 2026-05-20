namespace Transactions;

public class Transaction
{
    public Guid TransactionId { get; } = Guid.NewGuid();
    public TransactionType Type { get; }
    public decimal Amount { get; }
    public DateTime Timestamp { get; } = DateTime.Now;
    public decimal BalanceAfter { get; }
    public string Note { get; }

    public Transaction(TransactionType type, decimal amount, decimal balanceAfter, string note)
    {
        Type = type;
        Amount = amount;
        BalanceAfter = balanceAfter;
        Note = note;
    }
}