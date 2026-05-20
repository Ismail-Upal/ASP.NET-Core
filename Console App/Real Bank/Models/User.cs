

namespace Models;

public abstract class User
{
    public Guid Id { get; } = Guid.NewGuid();
    public string FullName { get; private set; }
    public string Username { get; private set; }
    private string _password;

    protected User(string fullName, string userName, string password)
    {
        FullName = fullName;
        Username = userName;
        _password = password;
    }
    
    public bool VerifyPassword(string password)
    {
        return _password == password;
    }
    public void ChangePassword(string oldPass, string newPass)
    {
        if(oldPass != _password)
            throw new InvalidOperationException("Wrong password.");
        _password = newPass;
    }
}