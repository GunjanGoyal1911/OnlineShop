using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EasyConsole;
using static OnlineShop.Member;

namespace OnlineShop.MenuPages
{
    class Register : Page
    {
       public Register(EasyConsole.Program program)
            : base("Register", program)
        {

        }

        public override void Display()
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
           
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();

            //Customer customer = new Customer(name, password);
            Console.WriteLine("Enter your membership points:");
            string membershipPoints = Input.ReadString("Enter your membership points:");
            int points = Convert.ToInt32(membershipPoints);
            var level = DecideMembershipLevel(points);


            Member customer = new Member(name, password, level);              

            var customers = GetCustomers();
            customers.Add(customer);
            Console.WriteLine($"{customer.Name} has registered successfully\n");
            
            string json = JsonSerializer.Serialize(customers, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true
            });
            
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\users.json");


            File.WriteAllText(path, json);
         
        }

        private MembershipLevel DecideMembershipLevel(int points)
        {
           
            if(points > 50 || points < 100)
            {
                return MembershipLevel.Bronze;
            }
            else if (points > 50 || points < 100)
            {
                return MembershipLevel.Bronze;
            }
            else if (points > 50 || points < 100)
            {
                return MembershipLevel.Bronze;
            }
            else
            {
                return MembershipLevel.None;
            }
        }

        static List<Member> GetCustomers()
        {
            string existedUsers = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\users.json");

            string JSON = File.ReadAllText(existedUsers).Trim();

            if (string.IsNullOrWhiteSpace(JSON.Trim())) return new List<Member>();

            List<Member> users = JsonSerializer.Deserialize<List<Member>>(JSON);
            return users;
        }
    }
}
