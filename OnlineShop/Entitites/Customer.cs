using System.Collections.Generic;
using OnlineShop.MenuPages;

namespace OnlineShop.Entities
{
    public class Customer
    {
        private string _name;
        private string _password;
        private List<Product> _cart;

        public Customer(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public List<Product> Cart 
        {
            get { return _cart; }
            set { _cart = value; }
        }
    }
}
