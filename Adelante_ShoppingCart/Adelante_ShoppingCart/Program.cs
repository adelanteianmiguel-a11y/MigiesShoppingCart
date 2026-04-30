using System;

namespace Adelante_ShoppingCart
{
    class Program
    {
        static void Main()
        {
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

                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Search Product");
                Console.WriteLine("3. View Cart");
                Console.WriteLine("4. Remove Item from Cart");
                Console.WriteLine("5. Clear Cart");
                Console.WriteLine("6. Exit / Checkout");
                Console.Write("Choose: ");

                string choice = Console.ReadLine();

                // ================= VIEW PRODUCTS =================
                if (choice == "1")
                {
                    foreach (Product p in products)
                    {
                        p.DisplayProduct();
                    }

                    AddToCart(products, cartslots, ref used);
                }

                // ================= SEARCH =================
                else if (choice == "2")
                {
                    SearchProduct(products);
                }

                // ================= VIEW CART =================
                else if (choice == "3")
                {
                    Console.WriteLine("\n--- YOUR CART ---");

                    for (int i = 0; i < used; i++)
                    {
                        Console.WriteLine($"{i + 1}. {cartslots[i].Drink.Name} x{cartslots[i].Quantity} = Php {cartslots[i].SubTotal:F2}");
                    }
                }

                // ================= REMOVE ITEM =================
                else if (choice == "4")
                {
                    Console.Write("Enter item number to remove: ");
                    int index = int.Parse(Console.ReadLine()) - 1;

                    if (index >= 0 && index < used)
                    {
                        cartslots[index].Drink.AddStock(cartslots[index].Quantity);

                        for (int i = index; i < used - 1; i++)
                        {
                            cartslots[i] = cartslots[i + 1];
                        }

                        used--;

                        Console.WriteLine("Item removed.");
                    }
                }

                // ================= CLEAR CART =================
                else if (choice == "5")
                {
                    for (int i = 0; i < used; i++)
                    {
                        cartslots[i].Drink.AddStock(cartslots[i].Quantity);
                    }

                    used = 0;
                    Console.WriteLine("Cart cleared.");
                }

                // ================= EXIT / CHECKOUT =================
                else if (choice == "6")
                {
                    break;
                }
            }

            // ================= RECEIPT =================
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

        // ================= ADD TO CART =================
        static void AddToCart(Product[] products, CartSlot[] cartslots, ref int used)
        {
            Console.Write("Enter ID: ");
            int id = int.Parse(Console.ReadLine());

            Product item = products[id - 1];

            Console.Write("Enter quantity: ");
            int qty = int.Parse(Console.ReadLine());

            if (!item.HasEnoughStock(qty))
            {
                Console.WriteLine("Not enough stock.");
                return;
            }

            cartslots[used] = new CartSlot();
            cartslots[used].Drink = item;
            cartslots[used].Quantity = qty;
            cartslots[used].SubTotal = item.GetItemTotal(qty);

            used++;

            item.DeductStock(qty);

            Console.WriteLine("Added to cart.");
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
