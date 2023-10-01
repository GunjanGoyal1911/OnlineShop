using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using EasyConsole;
using OnlineShop.Entities;
using OnlineShop.MenuPages;
using OnlineShop.Util;

namespace OnlineShop.Pages
{
    public class CustomerCartPage : Page
    {
        public CustomerCartPage(EasyConsole.Program program)
            : base("Customer Cart", program)
        {
        }

        public override void Display()
        {
            var loginUser = GetLoginMember();
            var members = GetMembers();          
            var cartForDisplay = CartForDisplay(loginUser, members);
           
            var table = new TablePrinter("Id", "Name", "Price", "Quantity", "Total");

            foreach (var member in members)
            {
                if (member.Name == loginUser.Name)
                {
                   if(member.Cart == null || member.Cart.Count == 0)
                    {
                        Output.WriteLine("Cart is empty. Please select the product.");
                    }
                    else
                    {
                        foreach (var item in cartForDisplay)
                        {
                            table.AddRow(item.Id, item.Name, item.Price, item.Quantity, item.Price * item.Quantity);
                        }
                        table.Print();

                        var finalAmount = GetFinalAmount(cartForDisplay);
                        Output.WriteLine($"You need to pay |Final Amount| : | {finalAmount} |");                        
                    }
                    break;
                }               
            }           
            
            Input.ReadString("Press [Enter] to shop");
            Program.NavigateTo<WelcomeToShopMenuPage>();
        }

        private int GetFinalAmount(List<Product> products)
        {
            int finalAmount = 0;
            foreach (var item in products)
            {
                finalAmount += item.Quantity * item.Price;
            }
            return finalAmount;
        }

        public List<Product> CartForDisplay(Member loginUser, List<Member> members)
        {
            if (members.Count > 0)
            {
                foreach (var member in members)
                {
                    if(member.Name == loginUser.Name)
                    {
                        return member.Cart;
                    }
                }
            }
            return new List<Product>();
        }

        public Member GetLoginMember()
        {
            string existedUsers = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\loginUser.json");
            string JSON = File.ReadAllText(existedUsers).Trim();

            if (string.IsNullOrWhiteSpace(JSON.Trim())) return null;

            var users = JsonSerializer.Deserialize<Member>(JSON);
            return users;
        }

        public List<Member> GetMembers()
        {
            string existedUsers = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\users.json");

            string JSON = File.ReadAllText(existedUsers).Trim();

            if (string.IsNullOrWhiteSpace(JSON.Trim())) return new List<Member>();

            var members = JsonSerializer.Deserialize<List<Member>>(JSON);
            return members;
        }
    }
}
