using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EasyConsole;

namespace OnlineShop.MenuPages
{
    public class Login : Page
    {
        public Login(EasyConsole.Program program) 
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
                string Name = Console.ReadLine();
                Console.WriteLine("Enter your password:");
                string password = Console.ReadLine();

                foreach (var member in members)
                {
                    if (member.Name == Name && member.Password == password)
                    {
                        Console.WriteLine("Login successfull");
                        AddLoginCustomerInDb(member);
                        Program.NavigateTo<WelcomeToShop>();
                        break;
                    }
                    else if (member.Name == Name && member.Password != password)
                    {
                        Console.WriteLine("Password is incorrect. Please try again!");
                        Display();
                        break;
                    }
                    else if (member.Name != Name)
                    {
                        Console.WriteLine("Customer does not exist and please register first");
                        //Registration();
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


        static List<Member> GetMembers()
        {
            string existedUsers = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\users.json");
            string JSON = File.ReadAllText(existedUsers).Trim();

            if (string.IsNullOrWhiteSpace(JSON.Trim())) return null;

            List<Member> users = JsonSerializer.Deserialize<List<Member>>(JSON);           
            return users;
        }
    }
}
