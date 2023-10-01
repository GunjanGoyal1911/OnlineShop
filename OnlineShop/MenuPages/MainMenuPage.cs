using EasyConsole;
using OnlineShop.Pages;

namespace OnlineShop.MenuPages
{
    class MainMenuPage : MenuPage
    {
        public MainMenuPage(EasyConsole.Program program) 
            : base("Main menu", program,
                  new Option("Login", () => program.NavigateTo<LoginPage>()),
                  new Option("Register", () => program.NavigateTo<RegisterPage>()),
                  new Option("Exit", () => program.NavigateTo<ExitPage>()))
        {
        }
    }
}
