namespace App.Models;

public class Receptionist : User
{
    public Receptionist(string id, string name, string email, string username, string password)
        : base(id, name, email, username, password, Role.Receptionist)
    {
    }
}