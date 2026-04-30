using System;
using System.Collections.Generic;

namespace Adelante_ShoppingCart
{
    class Program
    {
        static int receiptNo = 1;
        static List<string> orderHistory = new List<string>();

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

            CartSlot[] cart = new CartSlot[20];
            int used = 0;

            while (true)
            {
                Console.WriteLine("\n+---( SOSYAL NA TINDAHAN )---+");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Search Product");
                Console.WriteLine("3. View Cart");
                Console.WriteLine("4. Remove Item");
                Console.WriteLine("5. Clear Cart");
                Console.WriteLine("6. Checkout");
                Console.Write("Choose: ");

                string choice = Console.ReadLine();

                // ================= VIEW PRODUCTS =================
                if (choice == "1")
                {
                    foreach (Product p in products)
                        p.DisplayProduct();

                    AddToCart(products, cart, ref used);
                }

                // ================= SEARCH =================
                else if (choice == "2")
                {
                    SearchProduct(products);
                }

                // ================= VIEW CART =================
                else if (choice == "3")
                {
                    Console.WriteLine("\n--- CART ---");

                    for (int i = 0; i < used; i++)
                    {
                        Console.WriteLine($"{i + 1}. {cart[i].Drink.Name} x{cart[i].Quantity} = Php {cart[i].SubTotal:F2}");
                    }
                }

                // ================= REMOVE =================
                else if (choice == "4")
                {
                    Console.Write("Enter item #: ");
                    int index = int.Parse(Console.ReadLine()) - 1;

                    if (index >= 0 && index < used)
                    {
                        cart[index].Drink.AddStock(cart[index].Quantity);

                        for (int i = index; i < used - 1; i++)
                            cart[i] = cart[i + 1];

                        used--;

                        Console.WriteLine("Removed.");
                    }
                }

                // ================= CLEAR =================
                else if (choice == "5")
                {
                    for (int i = 0; i < used; i++)
                        cart[i].Drink.AddStock(cart[i].Quantity);

                    used = 0;
                    Console.WriteLine("Cart cleared.");
                }

                // ================= CHECKOUT =================
                else if (choice == "6")
                {
                    break;
                }
            }

            // ================= RECEIPT =================
            double grandTotal = 0;

            Console.WriteLine("\n===== RECEIPT =====");
            Console.WriteLine($"Receipt No: {receiptNo}");
            Console.WriteLine($"Date: {DateTime.Now}");

            for (int i = 0; i < used; i++)
            {
                Console.WriteLine($"{cart[i].Drink.Name} x{cart[i].Quantity} = Php {cart[i].SubTotal:F2}");
                grandTotal += cart[i].SubTotal;
            }

            double discount = grandTotal >= 5000 ? grandTotal * 0.10 : 0;
            double finalTotal = grandTotal - discount;

            Console.WriteLine($"\nGrand Total: Php {grandTotal:F2}");
            Console.WriteLine($"Discount: Php {discount:F2}");
            Console.WriteLine($"Final Total: Php {finalTotal:F2}");

            // ================= PAYMENT =================
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

            Console.WriteLine($"Payment: Php {payment:F2}");
            Console.WriteLine($"Change: Php {change:F2}");

            // ================= ORDER HISTORY =================
            orderHistory.Add($"Receipt #{receiptNo} - Total: {finalTotal:F2}");
            receiptNo++;

            Console.WriteLine("\n===== ORDER HISTORY =====");

            foreach (string h in orderHistory)
                Console.WriteLine(h);

            // ================= LOW STOCK ALERT =================
            Console.WriteLine("\n===== LOW STOCK ALERT =====");

            foreach (Product p in products)
            {
                if (p.RemainingStock <= 5)
                    Console.WriteLine($"{p.Name} only has {p.RemainingStock} left.");
            }

            // ================= UPDATED STOCK =================
            Console.WriteLine("\n===== UPDATED STOCK =====");

            foreach (Product p in products)
                p.DisplayProduct();
        }

        // ================= ADD TO CART =================
        static void AddToCart(Product[] products, CartSlot[] cart, ref int used)
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

            cart[used] = new CartSlot();
            cart[used].Drink = item;
            cart[used].Quantity = qty;
            cart[used].SubTotal = item.GetItemTotal(qty);

            used++;

            item.DeductStock(qty);

            Console.WriteLine("Added to cart.");
        }

        // ================= SEARCH =================
        static void SearchProduct(Product[] products)
        {
            Console.Write("Enter product name: ");
            string input = Console.ReadLine().ToLower();

            bool found = false;

            foreach (Product p in products)
            {
                if (p.Name.ToLower().Contains(input))
                {
                    Console.WriteLine($"{p.Id}. [{p.Category}] {p.Name} - Php {p.Price} - Stock: {p.RemainingStock}");
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("No product found.");
        }
    }
}
