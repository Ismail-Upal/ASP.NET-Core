namespace App.Models;

public class LabTechnician : User
{
    public LabTechnician(string id, string name, string email, string username, string password)
        : base(id, name, email, username, password, Role.LabTechnician)
    {
    }
}