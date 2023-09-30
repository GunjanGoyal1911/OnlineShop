using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using EasyConsole;
using OnlineShop.Util;

namespace OnlineShop.MenuPages
{
    public class CustomerCart : Page
    {
        public CustomerCart(EasyConsole.Program program)
            : base("Customer Cart", program)
        {
        }

        public override void Display()
        {
            var loginUser = GetLoginMember();
            var carts = GetCarts();
            var cartToDisplay = CartForDisplay(loginUser, carts);
            var table = new TablePrinter("Id", "Name", "Price", "Quantity", "Total");
            foreach (var item in cartToDisplay.Products)
            {
                table.AddRow(item.Id,item.Name,item.Price,item.Quantity, item.Price*item.Quantity);                
            }
            table.Print();

            var finalAmount = GetFinalAmount(cartToDisplay.Products);
            Output.WriteLine($"You need to pay |Final Amount| : | {finalAmount} |");

        }

        private int GetFinalAmount(List<Items> products)
        {
            int finalAmount = 0;
            foreach (var item in products)
            {
                finalAmount += item.Quantity * item.Price;
            }
            return finalAmount;
        }

        public Cart CartForDisplay(Customer loginUser, List<Cart> carts)
        {
            if (carts.Count > 0)
            {
                foreach (var item in carts)
                {
                    if (item.Owner.Name == loginUser.Name)
                    {
                        return item;  
                    }                    
                }
            }
            return new Cart();
        }

        public Member GetLoginMember()
        {
            string existedUsers = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\loginUser.json");
            string JSON = File.ReadAllText(existedUsers).Trim();

            if (string.IsNullOrWhiteSpace(JSON.Trim())) return null;

            var users = JsonSerializer.Deserialize<Member>(JSON);
            return users;
        }

        public List<Cart> GetCarts()
        {
            string existedCartItems = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\cart.json");
            string JSON = File.ReadAllText(existedCartItems).Trim();

            if (string.IsNullOrWhiteSpace(JSON.Trim())) return new List<Cart>();

            var carts = JsonSerializer.Deserialize<List<Cart>>(JSON);
            return carts;
        }
    }
}
