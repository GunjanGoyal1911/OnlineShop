using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EasyConsole;

namespace OnlineShop.MenuPages
{
    class Products : Page
    {      

        public Products(EasyConsole.Program program) 
            : base("Select Products", program)
        {

        }   

        public override void Display()
        {
            base.Display();
            int TotalPriceOfProduct = 0;

            //Fruit input = Input.ReadEnum<Fruit>("Select a fruit");
            //Output.WriteLine(ConsoleColor.Green, "You selected {0}", input);
            //string productsinString = String.Join(" ",GetProducts().ToArray());
            var products = GetProducts();
            foreach (var product in products)
            {
                Output.WriteLine("{0}. {1} {2}", product.Id, product.Name, product.Price);
            }
            var cart = ShoopingLoop(products);
            //string productSelected = Input.ReadString("Please select an option:");

            //var selectedProduct = ReturnProducts(products, productSelected);
            //Output.WriteLine($"You selected: {selectedProduct.Name}");            

            //Console.Write("Please select the quantity of product:");
            //int selectQuantity = Convert.ToInt32(Console.ReadLine());
            //selectedProduct.Quantity = selectQuantity;
            // var finalCostOfProduct = selectQuantity * selectedProduct.Price;
            //Output.WriteLine($"Cost have to pay for {selectedProduct.Name} is {finalCostOfProduct}");
            //string yesOrNo = Input.ReadString("Do you want to shop more product write 'yes' or 'no' :");

            //if(yesOrNo.Equals("yes"))
            //{
            //    string selectedItem = Input.ReadString("Please select a product number:");

            //}
            //else
            //{

            //}
            //var cart = new Cart(products);

            //Console.Write("Please write 4 to go back : ");
            //int goBack = Convert.ToInt32(Console.ReadLine());

            //if(goBack == 4)
            //{
            Input.ReadString("Press [Enter] to shop");
            Program.NavigateTo<WelcomeToShop>();
            //}           
            //Input.ReadString("Press [Enter] to navigate home");
            //Program.NavigateHome();
        }

        static Cart ShoopingLoop(List<Items> products)
        {
            var cart = new Cart();
            var purchasedProducts = new List<Items>();
            bool isRun = true;
            int displayFinalCost = 0;
            while (isRun)
            {
                string productSelected = Input.ReadString("Please select an option:");

                var selectedProduct = ReturnProducts(products, productSelected);
                Output.WriteLine($"You selected: {selectedProduct.Name}");

                Console.Write("Please select the quantity of product:");
                int selectQuantity = Convert.ToInt32(Console.ReadLine());

                selectedProduct.Quantity = selectQuantity;
                var finalCostOfProduct = selectQuantity * selectedProduct.Price;
                displayFinalCost += finalCostOfProduct;
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
                $"| or in dollars {ConvertToDollar(displayFinalCost)} " +
                $"| or in euros {ConvertToEuro(displayFinalCost)}");

            cart.AddProducts(purchasedProducts);
            return cart;
        }

        static double ConvertToDollar(int amount)
        {
            double exchangeRateSEKToUSD = 0.11;
            return exchangeRateSEKToUSD * amount;
        }

        static double ConvertToEuro(int amount)
        {
            double exchangeRateSEKToEuro = 0.094;
            return exchangeRateSEKToEuro * amount;
        }

        static Items ReturnProducts(List<Items> products, string productId)
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

        static List<Items> GetProducts()
        {
            string products = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\products.json");
            string JsonProducts = File.ReadAllText(products).Trim();

            if (string.IsNullOrWhiteSpace(JsonProducts.Trim())) return null;

            List<Items> items = JsonSerializer.Deserialize<List<Items>>(JsonProducts);
            return items;
        }
    }
}
