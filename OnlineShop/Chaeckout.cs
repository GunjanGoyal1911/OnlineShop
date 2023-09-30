using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EasyConsole;
using OnlineShop.MenuPages;

namespace OnlineShop
{
    public class Checkout : Page
    {
        public Checkout(EasyConsole.Program program)
            : base("Check out", program)
        {

        }
        public override void Display()
        {
            var loginUser = GetLoginCustomer();
            var carts = GetCarts();

            var cartToDisplay = UsersCart(loginUser, carts);
            var finalAmount = CalculateFinalAmount(cartToDisplay, loginUser);
            ClearCartForUser(carts, loginUser);
            Console.WriteLine($"Final amount to pay |{finalAmount}| Thank you");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }

        public void ClearCartForUser(List<Cart> carts, Member loginUser)
        {
            if (carts.Count > 0)
            {
                foreach (var item in carts)
                {
                    if (item.Owner.Name == loginUser.Name)
                    {
                        carts.Remove(item);
                        break;
                    }
                }
            }

            string json = JsonSerializer.Serialize(carts, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true
            });


            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\cart.json");
            File.WriteAllText(path, json);
        }

        private double CalculateFinalAmount(Cart cart, Member member)
        {
            if(cart.Products.Count >0)
            {
                double finalAmount = 0;
                double discount = 0;
                foreach (var item in cart.Products)
                {
                    finalAmount += item.Quantity * item.Price;
                }
                if(member.Level == "Bronze")
                {
                    discount = finalAmount*0.20;
                }
                else if(member.Level == "Silver")
                {
                    discount = finalAmount*0.40;
                }
                else if (member.Level == "Gold")
                {
                    discount = finalAmount * 0.60;
                }               
                return finalAmount -= discount;
            }
            return 0;
        }

        private Cart UsersCart(Member loginUser, List<Cart> carts)
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

        public Member GetLoginCustomer()
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
