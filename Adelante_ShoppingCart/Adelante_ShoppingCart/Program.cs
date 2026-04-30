using System;

namespace Adelante_ShoppingCart
{
    class Program
    {
        static void Main()
        {
            // Drink Section of the Shop
            Product[] products =
            {
                new Product(1, "C2 Apple", "Drinks", 35, 20),
                new Product(2, "C2 Lemon", "Drinks", 35, 20),
                new Product(3, "Pocari Sweat", "Drinks", 55, 15),
                new Product(4, "Coke Zero", "Drinks", 45, 18),
                new Product(5, "Sprite", 45, "Drinks", 18),
                new Product(6, "Royal", 45, "Drinks", 18),
                new Product(7, "Mountain Dew", "Drinks", 45, 18),
                new Product(8, "Minute Maid", "Drinks", 50, 12),
                new Product(9, "Gatorade Blue", "Drinks", 60, 10),
                new Product(10, "Gatorade Red", "Drinks", 60, 10),
                new Product(11, "Yakult Pack", "Drinks", 95, 8),
                new Product(12, "Vitamilk", 40, "Drinks", 14),
                new Product(13, "Dutch Mill", 38, "Drinks", 14),
                new Product(14, "Bottled Water", "Drinks", 25, 25),
                new Product(15, "Iced Coffee", "Drinks", 75, 9)
            };

            CartSlot[] cartslots = new CartSlot[10];
            int used = 0;
            string next = "Y";

            // shopping loop
            while (!string.IsNullOrEmpty(next) && next.ToUpper() == "Y")
            {
                Console.WriteLine("+---( SOSYAL NA TINDAHAN )---+");

                foreach (Product p in products)
                {
                    p.DisplayProduct();
                }

                Console.Write("Select ID: ");
                int id;
                bool validCode = int.TryParse(Console.ReadLine(), out id);

                if (!validCode)
                {
                    Console.WriteLine("Invalid entry.");
                    continue;
                }

                if (id < 1 || id > products.Length)
                {
                    Console.WriteLine("ID does not exist.");
                    continue;
                }

                Product item = products[id - 1];

                if (item.RemainingStock == 0)
                {
                    Console.WriteLine("Empty shelf.");
                    continue;
                }

                Console.Write("Enter quantity: ");
                int buy;
                bool intQuantity = int.TryParse(Console.ReadLine(), out buy);

                if (!intQuantity)
                {
                    Console.WriteLine("Quantity error.");
                    continue;
                }

                if (buy <= 0)
                {
                    Console.WriteLine("Quantity must be greater than zero.");
                    continue;
                }

                if (!item.HasEnoughStock(buy))
                {
                    Console.WriteLine("Insufficient stock.");
                    continue;
                }

                // check if item already exists in cart
                int pos = -1;
                for (int i = 0; i < used; i++)
                {
                    if (cartslots[i]?.Drink != null &&
                        cartslots[i].Drink.Id == item.Id)
                    {
                        pos = i;
                        break;
                    }
                }

                double subtotal = item.GetItemTotal(buy);

                if (pos == -1)
                {
                    if (used == cartslots.Length)
                    {
                        Console.WriteLine("Cart is full.");
                        continue;
                    }

                    cartslots[used] = new CartSlot();
                    cartslots[used].Drink = item;
                    cartslots[used].Quantity = buy;
                    cartslots[used].SubTotal = subtotal;
                    used++;
                }
                else
                {
                    cartslots[pos].Quantity += buy;
                    cartslots[pos].SubTotal =
                        cartslots[pos].Drink.GetItemTotal(cartslots[pos].Quantity);
                }

                item.DeductStock(buy);
                Console.WriteLine("Added to cart.");

                Console.Write("Add more? (Y/N): ");
                next = Console.ReadLine();

                if (string.IsNullOrEmpty(next))
                {
                    next = "N";
                }
            }

            double grandTotal = 0;

            Console.WriteLine("\n===== RECEIPT =====");

            for (int i = 0; i < used; i++)
            {
                if (cartslots[i] != null && cartslots[i].Drink != null)
                {
                    Console.WriteLine($"{cartslots[i].Drink.Name} x{cartslots[i].Quantity} = Php {cartslots[i].SubTotal:F2}");
                    grandTotal += cartslots[i].SubTotal;
                }
            }

            Console.WriteLine($"\nGrand Total: Php {grandTotal:F2}");

            double discount = 0;
            if (grandTotal >= 5000)
            {
                discount = grandTotal * 0.10;
            }

            Console.WriteLine($"Discount: Php {discount:F2}");
            Console.WriteLine($"Final Total: Php {(grandTotal - discount):F2}");

            Console.WriteLine("\n===== UPDATED STOCK =====");
            foreach (Product p in products)
            {
                p.DisplayProduct();
            }
        }
    }
}
