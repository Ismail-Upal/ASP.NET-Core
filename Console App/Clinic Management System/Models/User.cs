namespace App.Models;

public abstract class User
{
    public string Id { get; }
    public string Name { get; protected set; }
    public string Email { get; protected set; }
    public string Username { get; protected set; }
    private string Password { get; set; }
    public Role Role { get; protected set; }

    protected User(string id, string name, string email, string username, string password, Role role)
    {
        Id = id;
        Name = name;
        Email = email;
        Username = username;
        Password = password;
        Role = role;
    }

    public bool CheckPassword(string input)
    {
        return Password == input;
    }
}