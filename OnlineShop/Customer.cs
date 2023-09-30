using System.Collections.Generic;
using OnlineShop.MenuPages;

namespace OnlineShop
{
    public class Customer
    {
        private string _name;
        private string _password;
       
        public Customer(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public string Name
        {
            get { return _name; }
            private set { _name = value; }
        }

        public string Password
        {
            get { return _password; }
            private set { _password = value; }
        }      

    }
}
