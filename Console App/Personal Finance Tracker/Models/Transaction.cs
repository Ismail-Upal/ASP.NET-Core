using System;

namespace FinanceTracker.Models;

public class Transaction
{
    public TransactionType Type { get; set; }
    public double Amount { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }

    public override string ToString()
    {
        return $"{Type}\t{Amount}\t{Category}\t{Description}\t{Date:dd-MM-yyyy HH:mm:ss}";
    }
}
