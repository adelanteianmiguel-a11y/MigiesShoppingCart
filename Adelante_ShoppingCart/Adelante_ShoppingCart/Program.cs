using System.Drawing;

namespace Adelante_ShoppingCart
{
    class Program
    {
        public void Main()
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

            }
        }
    }
}