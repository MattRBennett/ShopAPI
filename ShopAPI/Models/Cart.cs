namespace ShopAPI.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public List<Item> CartItems { get; set; } = new List<Item>();
        public decimal CartTotal { get; set; } = decimal.Zero;
    }
}
