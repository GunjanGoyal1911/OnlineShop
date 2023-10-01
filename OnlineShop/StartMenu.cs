using OnlineShop.MenuPages;
using OnlineShop.Pages;

namespace OnlineShop
{
    class StartMenu : EasyConsole.Program
    {
        public StartMenu() 
            : base("Online Shop", breadcrumbHeader : true)
        {
            AddPage(new MainMenuPage(this));
            AddPage(new LoginPage(this));
            AddPage(new RegisterPage(this));
            AddPage(new ExitPage(this));
            AddPage(new WelcomeToShopMenuPage(this));
            AddPage(new ProductPage(this));
            AddPage(new CustomerCartPage(this));
            AddPage(new CheckoutPage(this));
            SetPage<MainMenuPage>();
        }
    }
}
