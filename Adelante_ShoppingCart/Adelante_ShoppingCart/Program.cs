using System.Drawing;

namespace Adelante_ShoppingCart
{
    class Program
    {
        // changing it to static 
        static void Main()
        {
            // Drink Section of the Shop
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
            int used = 0;       // number of used cart slot
            string next = "Y"; // assuming that user wants to continue first (acting as bool)

            // shopping loop
            while (next.ToUpper() == "Y")
            {
                Console.WriteLine("+---( SOSYAL NA TINDAHAN )---+");
                // Show all drinks one by one
                foreach (Product p in products)
                {
                    p.DisplayProduct();
                }

                // ask which drink they want
                Console.Write("Select ID: ");
                int id = 0;
                bool validCode = int.TryParse(Console.ReadLine(), out id);

                if (validCode == false)
                {
                    Console.WriteLine("Invalid entry.");
                    continue;
                }

                if (id < 1 || id > products.Length)
                {
                    Console.WriteLine("ID does not exist.");
                    continue;
                }

                // getting the item
                Product item = products[id - 1];

                // If no stock left, let the user know
                if (item.RemainingStock == 0)
                {
                    Console.WriteLine("Empty shelf.");
                    continue;
                }

                // quantity checker
                Console.Write("Enter quantity: ");
                int buy;
                bool intQuantity = int.TryParse(Console.ReadLine(), out buy);

                if (intQuantity == false)
                {
                    Console.WriteLine("Quantity error.");
                    continue;
                }

                if (buy <= 0)
                {
                    Console.WriteLine("Quantity must be greater than zero.");
                    continue;
                }

                if (item.HasEnoughStock(buy) == false)
                {
                    Console.WriteLine("Insufficient stock.");
                    continue;
                }

                // check if this drink is already in the cart
                int pos = -1;
                for (int i = 0; i < used; i++)
                {
                    if (cartslots[i].Drink.Id == item.Id)
                    {
                        pos = i;
                        break;
                    }
                }

                // computing the subtotal
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
                    // if its already in cart, just update quantity and subtotal
                    cartslots[pos].Quantity += buy;
                    cartslots[pos].SubTotal =
                        cartslots[pos].Drink.GetItemTotal(cartslots[pos].Quantity);
                }

                // deduct the stock after adding the item to the cart
                item.DeductStock(buy);
                Console.WriteLine("Added to cart.");

                // ask the user if they still want to add more drinks
                Console.Write("Add more? (Y/N): ");
                next = Console.ReadLine(); // the bool that handles the loop

            }

        }
    }
}