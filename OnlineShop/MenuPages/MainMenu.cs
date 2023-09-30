using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyConsole;

namespace OnlineShop.MenuPages
{
    class MainMenu : MenuPage
    {
        public MainMenu(EasyConsole.Program program) 
            : base("Main menu", program,
                  new Option("Login", () => program.NavigateTo<Login>()),
                  new Option("Register", () => program.NavigateTo<Register>()),
                  new Option("Exit", () => program.NavigateTo<Exit>()))
        {
        }
    }
}
