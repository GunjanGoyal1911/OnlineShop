using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineShop
{
    public class Cart
    {
        public Customer Owner { get; set; }
        public List<Items> Products { get; set; }

        //public Cart(List<Items> products = null)
        //{          

        //    if (products != null && products.Count > 0)
        //    {
        //        Products = products;
        //    }
        //    else Products = new List<Items>();
        //}

        public Cart()
        {           
        }

        public void AddProducts(List<Items> products)
        {
            //productName = productName.Trim();

            //foreach (var product in Products)
            //{
            //    if (product.Name == productName)
            //    {
                    //product.Quantity +=  1 ;
                    
                    //product.Price = product.Price * product.Quantity;             
                    UpdateCart(products);
                    return;
            //    }
            //}          
        }

        public void UpdateCart(List<Items> selectedItems)
        {
            //Cart carts = new Cart();
            var cart = new Cart();
            var carts = GetCarts();
            

            //carts.Products.Add(item);
            var loginUser = GetLoginCustomer();

            if (carts.Count > 0)
            {
               
                foreach (var item in carts)
                {
                    
                    if (item.Owner.Name == loginUser.Name)
                    {
                        foreach (var selectedItem in selectedItems)
                        {
                            item.Products.Add(selectedItem);
                        }
                        break;

                    }
                    else
                    {
                        cart.Owner = loginUser;
                        cart.Products = selectedItems;
                        carts.Add(cart);
                        break;
                    }                    
                }
            }
            else
            {
               
                cart.Owner = loginUser;
                cart.Products = selectedItems;
                carts.Add(cart);
            }

            

            string json = JsonSerializer.Serialize(carts, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IncludeFields = true
            });
          

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\cart.json");           
            File.WriteAllText(path, json);
        }

        public Customer GetLoginCustomer()
        {
            string existedUsers = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\loginUser.json");
            string JSON = File.ReadAllText(existedUsers).Trim();

            if (string.IsNullOrWhiteSpace(JSON.Trim())) return null;

            var users = JsonSerializer.Deserialize<Customer>(JSON);
            return users;
        }

        public List<Cart> GetCarts()
        {
            string existedCartItems = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "\\Data\\cart.json");
            string JSON = File.ReadAllText(existedCartItems).Trim();

            if (string.IsNullOrWhiteSpace(JSON.Trim())) return new List<Cart>();

            var carts = JsonSerializer.Deserialize<List<Cart>>(JSON);
            return carts;
        }
    }
}
