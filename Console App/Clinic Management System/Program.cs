using App.Menus;
using App.Models;
using App.Services;

namespace App;

class Program
{
    public static void Main(string[] args)
    {
        var admin = new Admin("A1", "Super Admin", "admin@gmail.com", "admin", "admin123");
        var auth = new AuthService(admin);

        MainMenu app = new MainMenu(auth);
        app.Menu();
    }
}