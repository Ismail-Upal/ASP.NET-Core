namespace App.Models;

public class Admin : User
{
    public Admin(string id, string name, string email, string username, string password)
        : base(id, name, email, username, password, Role.Admin)
    {
    }
}