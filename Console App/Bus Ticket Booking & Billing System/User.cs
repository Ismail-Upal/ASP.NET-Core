public class User
{
    private static int _nextId = 0;
    public int UserId { get; set; }
    public string? FullName { get; set; }
    public string? Mobile { get; set; }
    public string? Email { get; set; }

    public User(string fullName, string mobile, string email)
    {
        UserId = _nextId++;
        FullName = fullName;
        Mobile = mobile;
        Email = email;
    }
}
