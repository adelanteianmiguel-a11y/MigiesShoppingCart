namespace Adelante_ShoppingCart
{
    class CartSlot
    {
        public Product Drink { get; set; }
        public int Quantity { get; set; }
        public double SubTotal { get; set; }

        // optional constructor (safe default)
        public CartSlot()
        {
            Drink = null;
            Quantity = 0;
            SubTotal = 0;
        }
    }
}
