using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyConsole;

namespace OnlineShop.MenuPages
{
    class WelcomeToShop : MenuPage
    {
        public WelcomeToShop(EasyConsole.Program program) 
            : base($"Welcome", program,
                   new Option("Products", () => program.NavigateTo<Products>()),
                  new Option("Cart", () => program.NavigateTo<CustomerCart>()),
                  new Option("Check out", () => program.NavigateTo<Chaeckout>()))
        {

        }
    }
}
