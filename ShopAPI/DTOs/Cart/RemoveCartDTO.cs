namespace ShopAPI.DTOs.Cart
{
    public class RemoveCartDTO
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public List<Models.Item> CartItems { get; set; } = new List<Models.Item>();
        public decimal CartTotal { get; set; } = decimal.Zero;
    }
}
