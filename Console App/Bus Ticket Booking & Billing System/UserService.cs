public static class UserService
{
    public static List<User> Users = new List<User>();

    public static void CreateUser()
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

            if (Users.FirstOrDefault(u => u.Mobile == input) != null)
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

            if (Users.FirstOrDefault(u => u.Email == input) != null)
            {
                Utility.PrintMessage("This Email is already registered. Try again.\n", false);
                continue;
            }

            email = input;
            break;
        }

        var newUser = new User(fullName, mobile, email);
        Users.Add(newUser);
        Utility.PrintMessage($"\nUser created successfully.\nWelcome Mr/Ms. {fullName}", true);
    }

    public static void ShowUsers()
    {
        Console.WriteLine("----------- Users -----------");
        Console.WriteLine("{0, -5} {1, -15} {2, -15} {3, -20}", "Id", "Name", "Mobile", "Email");
        foreach (var user in Users)
        {
            Console.WriteLine(
                "{0, -5} {1, -15} {2, -15} {3, -20}",
                user.UserId,
                user.FullName,
                user.Mobile,
                user.Email
            );
        }
        Console.WriteLine();
    }
}