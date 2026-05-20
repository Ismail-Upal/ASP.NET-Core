using Accounts;

namespace Models;

public class Customer : User
{
    public List<BankAccount> Accounts { get; } = new();

    public Customer(string fullName, string username, string password)
        : base(fullName, username, password) { }
}