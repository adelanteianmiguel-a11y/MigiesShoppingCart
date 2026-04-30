using System;

namespace Adelante_ShoppingCart
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int RemainingStock { get; set; }

        // constructor
        public Product(int id, string name, double price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            RemainingStock = stock;
        }

        // display product info
        public void DisplayProduct()
        {
            Console.WriteLine($"{Id}. {Name} - Php {Price:F2} | Stock: {RemainingStock}");
        }

        // check if enough stock
        public bool HasEnoughStock(int quantity)
        {
            return quantity <= RemainingStock;
        }

        // compute total price
        public double GetItemTotal(int quantity)
        {
            return Price * quantity;
        }

        // deduct stock after purchase
        public void DeductStock(int quantity)
        {
            if (quantity <= RemainingStock)
            {
                RemainingStock -= quantity;
            }
        }
    }
}
