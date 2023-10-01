using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using EasyConsole;
using OnlineShop.Entities;

namespace OnlineShop.Pages
{
    public class CheckoutPage : Page
    {
        public CheckoutPage(EasyConsole.Program program)
            : base("Check out", program)
        {

        }
        public override void Display()
        {            
            var loginUser = GetLoginCustomer();
            var members = GetMembers();

            var cartForDisplay = MemberCart(loginUser, members);
            var finalAmount = CalculateFinalAmount(cartForDisplay, loginUser);
            if (cartForDisplay.Count > 0)
            {
                ClearCartForUser(loginUser, members);
                Console.WriteLine($"Final amount  after discount is |{finalAmount}kr| Thank you\n");
            }
            else
            {
               Console.WriteLine($"Your cart is empty. Kindly do the shopping. Thank you\n");
            }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }

        public void ClearCartForUser(Member loginUser, List<Member>members)
        {
            if (members.Count > 0)
            {
                foreach (var member in members)
                {
                    if (member.Name == loginUser.Name)
                    {
                        member.Cart = new List<Product>();
                        break;
                    }
                }
            }

            string json = JsonSerializer.Serialize(members, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true
            });


            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\users.json");
            File.WriteAllText(path, json);
        }

        private double CalculateFinalAmount(List<Product>products, Member member)
        {
            if(products.Count >0)
            {
                double finalAmount = 0;
                double discount = 0;
                foreach (var item in products)
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

        private List<Product> MemberCart(Member loginUser, List<Member> members)
        {
            if (members.Count > 0)
            {
                foreach (var member in members)
                {
                    if (member.Name == loginUser.Name)
                    {
                        return member.Cart;
                    }
                }
            }
            return new List<Product>();
        }

        private Member GetLoginCustomer()
        {
            string existedUsers = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\loginUser.json");
            string JSON = File.ReadAllText(existedUsers).Trim();

            if (string.IsNullOrWhiteSpace(JSON.Trim())) return null;

            var member = JsonSerializer.Deserialize<Member>(JSON);
            return member;
        }

        private List<Member> GetMembers()
        {
            string existedUsers = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\users.json");

            string JSON = File.ReadAllText(existedUsers).Trim();

            if (string.IsNullOrWhiteSpace(JSON.Trim())) return new List<Member>();

            var members = JsonSerializer.Deserialize<List<Member>>(JSON);
            return members;
        }
    }
}
