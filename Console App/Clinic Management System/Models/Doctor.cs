using System.Collections.Generic;

namespace App.Models;

public class Doctor : User
{
    public string Specialization { get; private set; }
    public List<TimeSpan> AvailableSlots { get; } = new();

    public Doctor(string id, string name, string email, string username, string password, string specialization)
        : base(id, name, email, username, password, Role.Doctor)
    {
        Specialization = specialization;
    }
}