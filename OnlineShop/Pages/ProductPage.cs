using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using EasyConsole;
using OnlineShop.Entities;

namespace OnlineShop.MenuPages
{
    class ProductPage : Page
    {

        public ProductPage(EasyConsole.Program program)
            : base("Select Products", program)
        {

        }

        public override void Display()
        {
            base.Display();
            var products = GetProducts();
            foreach (var product in products)
            {
                Output.WriteLine($"{product.Id}. {product.Name} {product.Price}kr");
            }
            ShoopingLoop(products);

            Input.ReadString("Press [Enter] to shop");
            Program.NavigateTo<WelcomeToShopMenuPage>();
        }

        public void ShoopingLoop(List<Product> products)
        {          
            var loginUser = GetLoginCustomer();
            var members = GetMembers();
            var purchasedProducts = new List<Product>();
            bool isRun = true;
            int displayFinalCost = 0;

            while (isRun)
            {
                string productSelected = Input.ReadString("Please select an option:");

                var selectedProduct = ReturnProducts(products, productSelected);
                Console.WriteLine($"You selected: {selectedProduct.Name}");
                Console.Write("\n##############################################\n");
                Console.Write("Please select the quantity of product:");

                int selectQuantity = Convert.ToInt32(Console.ReadLine());

                selectedProduct.Quantity = selectQuantity;
                var finalCostOfProduct = selectQuantity * selectedProduct.Price;
                displayFinalCost += finalCostOfProduct;
                Output.WriteLine($"You have selected {selectedProduct.ToString()}");
                Output.WriteLine($"Total amount of {selectedProduct.Name} is {finalCostOfProduct} kr");

                string yesOrNo = Input.ReadString("Do you want to shop more product write 'yes' or 'no' :");

                if (yesOrNo.Equals("yes"))
                {
                    isRun = true; ;

                }
                else
                {
                    isRun = false;
                }
                purchasedProducts.Add(selectedProduct);
            }
            Output.WriteLine($"Your total cost is : | {displayFinalCost} Kr " +
                $"| or {ConvertToDollar(displayFinalCost)} USD" +
                $"| or {ConvertToEuro(displayFinalCost)} EUR");

            var memberWithCart = members.FirstOrDefault(user => user.Name == loginUser.Name && user.Password == loginUser.Password);            

            foreach (var member in members)
            {
                if(member.Name == memberWithCart.Name)
                {
                    if (memberWithCart.Cart != null)
                    {
                        foreach (var purchasedProduct in purchasedProducts)
                        {
                            member.Cart.Add(purchasedProduct);
                        }
                    }
                    else
                    {
                        member.Cart = purchasedProducts;
                    }
                }
            }          
            UpdateCustomer(members);
        }

        public double ConvertToDollar(int amount)
        {
            double exchangeRateSEKToUSD = 0.11;
            return exchangeRateSEKToUSD * amount;
        }

        public double ConvertToEuro(int amount)
        {
            double exchangeRateSEKToEuro = 0.094;
            return exchangeRateSEKToEuro * amount;
        }

        public Product ReturnProducts(List<Product> products, string productId)
        {
            foreach (var product in products)
            {
                if (product.Id.ToString() == productId)
                {
                    return product;
                }
            }
            return null;
        }

        public List<Product> GetProducts()
        {
            string productsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\products.json");
            string JsonProducts = File.ReadAllText(productsPath).Trim();

            if (string.IsNullOrWhiteSpace(JsonProducts.Trim())) return null;

            List<Product> products = JsonSerializer.Deserialize<List<Product>>(JsonProducts);
            return products;
        }

        public void UpdateCustomer(List<Member> members)
        {

            string json = JsonSerializer.Serialize(members, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true
            });

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\users.json");
            File.WriteAllText(path, json);
        }

        public Member GetLoginCustomer()
        {
            string existedUsers = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\loginUser.json");
            string JSON = File.ReadAllText(existedUsers).Trim();

            if (string.IsNullOrWhiteSpace(JSON.Trim())) return null;

            var member = JsonSerializer.Deserialize<Member>(JSON);
            return member;
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
