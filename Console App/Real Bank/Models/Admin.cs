

namespace Models;

public class Admin : User
{
    public string EmployeeId { get; private set; }
    public Admin(string fullName, string userName, string password, string employeeId)
        : base(fullName, userName, password)
    {
        EmployeeId = employeeId;
    }
}