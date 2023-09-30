using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
    public class Member : Customer
    {
        //public enum MembershipLevel
        //{
        //    Gold = 85,
        //    Silver = 90,
        //    Bronze = 95,
        //    None = 0,
        //}

        public enum MembershipLevel
        {
            Gold,
            Silver,
            Bronze,
            None,
        }

        public Member(string name, string password, string level) : base(name, password)
        {
            Level = level;
        }

        private string _level;
        public string Level
        {
            get { return _level; }
            private set { _level = value; }
        }
    }
}
