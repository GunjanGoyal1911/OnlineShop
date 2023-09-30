using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop
{
    public class Member : Customer
    {
        public enum MembershipLevel
        {
            Gold = 85,
            Silver = 90,
            Bronze = 95,
            None = 0,
        }

        public Member(string name, string password, MembershipLevel level) : base(name, password)
        {
            Level = level;
        }

        private MembershipLevel _level;
        public MembershipLevel Level
        {
            get { return _level; }
            private set { _level = value; }
        }
    }
}
