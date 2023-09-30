namespace OnlineShop
{
    public class Items
    {
        private int _id;
        private string _name;
        private int _price;
        private int _quantity;
        public Items(int id, string name, int price, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public override string ToString() 
        {
            return $"Product Id : {Id} | Product name : {Name} | Product price : {Price} | Product quantity : {Quantity}";
        }
    }
}
