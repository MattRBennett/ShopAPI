namespace ShopAPI.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public string CartItems { get; set; } = string.Empty;
        //public decimal CartTotal { get; set; } = decimal.Zero;
    }
}
