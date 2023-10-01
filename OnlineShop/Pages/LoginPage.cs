using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using EasyConsole;
using OnlineShop.Entities;
using OnlineShop.MenuPages;

namespace OnlineShop.Pages
{
    public class LoginPage : Page
    {
        public LoginPage(EasyConsole.Program program) 
            : base("Login", program)
        {
        }     

        public override void Display()
        {
            var members = GetMembers();
            if(members == null) 
            { 
                Console.WriteLine("No customer found please start with registration");
            }
            else
            {
                Console.WriteLine("Enter your username:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter your password:");
                string password = Console.ReadLine();

               
               if(members.Any(mem => mem.Name == name && mem.Password != password))
                {
                    Console.WriteLine("Password is incorrect. Please try again!");
                    Display();
                }
                else if (!members.Any(mem => mem.Name == name && mem.Password == password))
                {
                    Console.WriteLine("Customer does not exist and please register first");
                }
                else
                {
                    foreach (var member in members)
                    {
                        if (member.Name == name && member.Password == password)
                        {
                            Console.WriteLine("Login successfull");
                            AddLoginCustomerInDb(member);
                            Program.NavigateTo<WelcomeToShopMenuPage>();
                            break;
                        }                       
                    }
                }               
            }          

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }

        private void AddLoginCustomerInDb(Member memeber)
        {
            string json = JsonSerializer.Serialize(memeber, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true
            });

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\loginUser.json");

            File.WriteAllText(path, json);
        }


        public List<Member> GetMembers()
        {
            string existedUsers = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\users.json");
            string JSON = File.ReadAllText(existedUsers).Trim();

            if (string.IsNullOrWhiteSpace(JSON.Trim())) return null;

            List<Member> users = JsonSerializer.Deserialize<List<Member>>(JSON);           
            return users;
        }
    }
}
