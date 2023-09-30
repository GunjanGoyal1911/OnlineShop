using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
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
           
            //Console.WriteLine("Enter your membership points:");
            string membershipPoints = Input.ReadString("Enter your membership points:");
            int points = Convert.ToInt32(membershipPoints);
            var level = DecideMembershipLevel(points);


            Member member = new Member(name, password, level);              

            var members = GetMembers();
            members.Add(member);
            Console.WriteLine($"{member.Name} has registered successfully\n");
          
            string json = JsonSerializer.Serialize(members, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true
            });
            
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\users.json");


            File.WriteAllText(path, json);

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }

        private string DecideMembershipLevel(int points)
        {
           
            if(points > 50 || points < 100)
            {               
                return Enum.GetName(typeof(MembershipLevel), MembershipLevel.Bronze);
            }
            else if (points > 100 || points < 200)
            {
                return Enum.GetName(typeof(MembershipLevel), MembershipLevel.Silver);
            }
            else if (points > 200 || points < 300)
            {
                return Enum.GetName(typeof(MembershipLevel), MembershipLevel.Gold);
            }
            else
            {
                return string.Empty;
            }            
        }

        static List<Member> GetMembers()
        {
            string existedUsers = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\users.json");

            string JSON = File.ReadAllText(existedUsers).Trim();

            if (string.IsNullOrWhiteSpace(JSON.Trim())) return new List<Member>();

            var members = JsonSerializer.Deserialize<List<Member>>(JSON);
            return members;
        }
    }
}
