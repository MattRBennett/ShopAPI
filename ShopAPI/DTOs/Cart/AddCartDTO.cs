namespace ShopAPI.DTOs.Cart
{
    public class AddCartDTO
    {
        public int UserID { get; set; }
        public string CartItem { get; set; } = string.Empty;
    }
}
