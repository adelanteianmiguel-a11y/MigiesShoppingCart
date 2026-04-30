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
                new Product(5, "Sprite", "Drinks", 45, 18),
                new Product(6, "Royal", "Drinks", 45, 18),
                new Product(7, "Mountain Dew", "Drinks", 45, 18),
                new Product(8, "Minute Maid", "Drinks", 50, 12),
                new Product(9, "Gatorade Blue", "Drinks", 60, 10),
                new Product(10, "Gatorade Red", "Drinks", 60, 10),
                new Product(11, "Yakult Pack", "Drinks", 95, 8),
                new Product(12, "Vitamilk", "Drinks", 40, 14),
                new Product(13, "Dutch Mill", "Drinks", 38, 14),
                new Product(14, "Bottled Water", "Drinks", 25, 25),
                new Product(15, "Iced Coffee", "Drinks", 75, 9)
            };

            CartSlot[] cartslots = new CartSlot[10];
            int used = 0;
            string next = "Y";

            while (!string.IsNullOrEmpty(next) && next.ToUpper() == "Y")
            {
                Console.WriteLine("\n+---( SOSYAL NA TINDAHAN )---+");

                // MENU OPTION FOR SEARCH
                Console.WriteLine("Type 'S' to search product OR press ENTER to continue");

                string menuChoice = Console.ReadLine();

                if (menuChoice.ToUpper() == "S")
                {
                    SearchProduct(products);
                    continue;
                }

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

                if (!intQuantity || buy <= 0)
                {
                    Console.WriteLine("Invalid quantity.");
                    continue;
                }

                if (!item.HasEnoughStock(buy))
                {
                    Console.WriteLine("Insufficient stock.");
                    continue;
                }

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
                Console.WriteLine($"{cartslots[i].Drink.Name} x{cartslots[i].Quantity} = Php {cartslots[i].SubTotal:F2}");
                grandTotal += cartslots[i].SubTotal;
            }

            double discount = grandTotal >= 5000 ? grandTotal * 0.10 : 0;
            double final = grandTotal - discount;

            Console.WriteLine($"\nGrand Total: Php {grandTotal:F2}");
            Console.WriteLine($"Discount: Php {discount:F2}");
            Console.WriteLine($"Final Total: Php {final:F2}");

            Console.WriteLine("\n===== UPDATED STOCK =====");
            foreach (Product p in products)
            {
                p.DisplayProduct();
            }
        }

        // ================= SEARCH FUNCTION =================
        static void SearchProduct(Product[] products)
        {
            Console.Write("Enter product name to search: ");
            string input = Console.ReadLine().ToLower();

            bool found = false;

            Console.WriteLine("\n--- SEARCH RESULTS ---");

            foreach (Product p in products)
            {
                if (p.Name.ToLower().Contains(input))
                {
                    Console.WriteLine($"{p.Id}. [{p.Category}] {p.Name} - Php {p.Price} - Stock: {p.RemainingStock}");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No product found.");
            }
        }
    }
}
