namespace OnlineShop.Entities
{
    public class Member : Customer
    {        
        public enum MembershipLevel
        {
            Gold,
            Silver,
            Bronze,
            None
        }

        private string _level;

        public Member(string name, string password, string level) : base(name, password)
        {
            Level = level;
        }

        
        public string Level
        {
            get { return _level; }
            private set { _level = value; }
        }
    }
}
