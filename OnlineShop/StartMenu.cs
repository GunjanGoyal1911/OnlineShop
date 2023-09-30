using System;
using EasyConsole;
using OnlineShop.MenuPages;

namespace OnlineShop
{
    class StartMenu : EasyConsole.Program
    {
        public StartMenu() 
            : base("Online Shop", breadcrumbHeader : true)
        {
            AddPage(new MainMenu(this));
            AddPage(new Login(this));
            AddPage(new Register(this));
            AddPage(new Exit(this));
            AddPage(new WelcomeToShop(this));
            AddPage(new Products(this));
            AddPage(new CustomerCart(this));
            AddPage(new Checkout(this));
            SetPage<MainMenu>();
        }
    }
}
