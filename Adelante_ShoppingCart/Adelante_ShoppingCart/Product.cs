using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adelante_ShoppingCart
{
    internal class Product
    {
        // This class is for every drink chilling in the cooler
        public int Id;
        public string Name;
        public double Price;
        public int RemainingStock;

        // When we create a new drink item, we fill in its info here
        public Product(int id, string name, double price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            RemainingStock = stock;
        }

        // Additional methods

        // Quick total: price x quantity
        public double GetItemTotal(int quantity)
        {
            return Price * quantity;
        }

        // Checks if there's enough stock left
        public bool HasEnoughStock(int quantity)
        {
            return quantity <= RemainingStock;
        }

        // Updates stock after buying
        public void DeductStock(int quantity)
        {
            RemainingStock -= quantity;
        }

        // Must have method

        // To Show the drinks
        public void DisplayProduct()
        {
            Console.WriteLine($"{Id}. {Name,-12} Price: {Price,8:F2} Stock: {RemainingStock}");
        }

    }
}
