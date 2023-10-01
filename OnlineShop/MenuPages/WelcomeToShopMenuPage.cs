using EasyConsole;
using OnlineShop.Pages;

namespace OnlineShop.MenuPages
{
    class WelcomeToShopMenuPage : MenuPage
    {
        public WelcomeToShopMenuPage(EasyConsole.Program program) 
            : base($"Welcome", program,
                   new Option("Products", () => program.NavigateTo<ProductPage>()),
                  new Option("Cart", () => program.NavigateTo<CustomerCartPage>()),
                  new Option("Check out", () => program.NavigateTo<CheckoutPage>()))
        {

        }
    }
}
