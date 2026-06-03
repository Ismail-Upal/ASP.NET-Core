using Interfaces;
using Repositories;

public class UserService : IUserService
{
    private readonly UserRepository _userRepo;

    public UserService(UserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public void CreateUser()
    {
        string? fullName;
        while (true)
        {
            Console.Write("Name : ");
            string? input = Console.ReadLine();;
            if (input == null) return;

            if (string.IsNullOrWhiteSpace(input))
            {
                Utility.PrintMessage("Name is required. Try again.\n", false);
                continue;
            }
            fullName = input;
            break;
        }

        string? mobile;
        while (true)
        {
            Console.Write("Mobile (11 digits) : ");
            string? input = Console.ReadLine();
            if (input == null) return;

            if (string.IsNullOrWhiteSpace(input))
            {
                Utility.PrintMessage("Mobile is required. Try again.\n", false);
                continue;
            }

            if (input.Length != 11 || !input.All(char.IsDigit))
            {
                Utility.PrintMessage("Invalid Mobile. Try again.\n", false);
                continue;
            }

            if (_userRepo.Users.FirstOrDefault(u => u.Mobile == input) != null)
            {
                Utility.PrintMessage("This Mobile is already registered. Try again.\n", false);
                continue;
            }

            mobile = input;
            break;
        }

        string? email;
        while (true)
        {
            Console.Write("Email : ");
            string? input = Console.ReadLine();
            if (input == null) return;

            if (string.IsNullOrWhiteSpace(input))
            {
                Utility.PrintMessage("Email is required. Try again.\n", false);
                continue;
            }

            if (!input.Contains("@") || !input.Contains("."))
            {
                Utility.PrintMessage("Invalid Email. Try again.\n", false);
                continue;
            }

            if (_userRepo.Users.FirstOrDefault(u => u.Email == input) != null)
            {
                Utility.PrintMessage("This Email is already registered. Try again.\n", false);
                continue;
            }

            email = input;
            break;
        }

        var newUser = new User(fullName, mobile, email);
        _userRepo.Users.Add(newUser);
        Utility.PrintMessage($"\nUser created successfully.\nWelcome Mr/Ms. {fullName}", true);
    }

    public void ShowUsers()
    {
        Console.WriteLine("\n----------- Users -----------");
        Console.WriteLine("{0, -8} {1, -15} {2, -15} {3, -20}", "UserId", "Name", "Mobile", "Email");
        foreach (var user in _userRepo.Users)
        {
            Console.WriteLine(
                "{0, -8} {1, -15} {2, -15} {3, -20}",
                user.UserId,
                user.FullName,
                user.Mobile,
                user.Email
            );
        }
        Console.WriteLine();
    }
}