using System;

namespace Adelante_ShoppingCart
{
    class Program
    {
        static int receiptNo = 1;

        static void Main()
        {
            Product[] products =
            {
                new Product(1, "C2 Apple", 35, 20),
                new Product(2, "C2 Lemon", 35, 20),
                new Product(3, "Pocari Sweat", 55, 15),
                new Product(4, "Coke Zero", 45, 18),
                new Product(5, "Sprite", 45, 18),
                new Product(6, "Royal", 45, 18),
                new Product(7, "Mountain Dew", 45, 18),
                new Product(8, "Minute Maid", 50, 12),
                new Product(9, "Gatorade Blue", 60, 10),
                new Product(10, "Gatorade Red", 60, 10),
                new Product(11, "Yakult Pack", 95, 8),
                new Product(12, "Vitamilk", 40, 14),
                new Product(13, "Dutch Mill", 38, 14),
                new Product(14, "Bottled Water", 25, 25),
                new Product(15, "Iced Coffee", 75, 9)
            };

            CartSlot[] cartslots = new CartSlot[10];
            int used = 0;
            string next = "Y";

            while (!string.IsNullOrEmpty(next) && next.ToUpper() == "Y")
            {
                Console.WriteLine("\n+---( SOSYAL NA TINDAHAN )---+");

                foreach (Product p in products)
                {
                    p.DisplayProduct();
                }

                Console.Write("Select ID: ");
                int id = int.Parse(Console.ReadLine());

                Product item = products[id - 1];

                Console.Write("Enter quantity: ");
                int qty = int.Parse(Console.ReadLine());

                if (!item.HasEnoughStock(qty))
                {
                    Console.WriteLine("Insufficient stock.");
                    continue;
                }

                cartslots[used] = new CartSlot();
                cartslots[used].Drink = item;
                cartslots[used].Quantity = qty;
                cartslots[used].SubTotal = item.GetItemTotal(qty);
                used++;

                item.DeductStock(qty);

                Console.Write("Add more? (Y/N): ");
                next = Console.ReadLine();
            }

            // ================= RECEIPT (UPDATED FORMAT ONLY) =================
            double grandTotal = 0;

            Console.WriteLine("\n================ RECEIPT ================");
            Console.WriteLine($"Receipt #: {receiptNo}");
            Console.WriteLine($"Date: {DateTime.Now}");
            Console.WriteLine("----------------------------------------");

            for (int i = 0; i < used; i++)
            {
                Console.WriteLine($"{cartslots[i].Drink.Name} x{cartslots[i].Quantity} = PHP {cartslots[i].SubTotal:F2}");
                grandTotal += cartslots[i].SubTotal;
            }

            double discount = grandTotal >= 5000 ? grandTotal * 0.10 : 0;
            double finalTotal = grandTotal - discount;

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Total: PHP {grandTotal:F2}");
            Console.WriteLine($"Discount: PHP {discount:F2}");
            Console.WriteLine($"Final Total: PHP {finalTotal:F2}");

            // PAYMENT (for proper receipt like screenshot)
            double payment;

            while (true)
            {
                Console.Write("Enter payment: ");
                if (!double.TryParse(Console.ReadLine(), out payment))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                if (payment < finalTotal)
                {
                    Console.WriteLine("Insufficient payment.");
                    continue;
                }

                break;
            }

            double change = payment - finalTotal;

            Console.WriteLine($"Payment: PHP {payment:F2}");
            Console.WriteLine($"Change: PHP {change:F2}");

            Console.WriteLine("========================================");
            Console.WriteLine("LOW STOCK ALERT:");

            foreach (Product p in products)
            {
                if (p.RemainingStock <= 5)
                {
                    Console.WriteLine($"{p.Name} - Remaining: {p.RemainingStock}");
                }
            }

            Console.WriteLine("Checkout complete.");
            Console.WriteLine($"Continue shopping? (Y/N): ");

            receiptNo++;
        }
    }

    class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int RemainingStock;

        public Product(int id, string name, double price, int stock)
        {
            Id = id;
            Name = name;
            Price = price;
            RemainingStock = stock;
        }

        public void DisplayProduct()
        {
            Console.WriteLine($"{Id}. {Name} - PHP {Price} (Stock: {RemainingStock})");
        }

        public bool HasEnoughStock(int qty)
        {
            return qty <= RemainingStock;
        }

        public double GetItemTotal(int qty)
        {
            return Price * qty;
        }

        public void DeductStock(int qty)
        {
            RemainingStock -= qty;
        }
    }

    class CartSlot
    {
        public Product Drink;
        public int Quantity;
        public double SubTotal;
    }
}
