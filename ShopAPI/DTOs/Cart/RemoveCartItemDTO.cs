using ShopAPI.Models;

namespace ShopAPI.DTOs.Cart
{
    public class RemoveCartItemDTO
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public string CartItems { get; set; } = string.Empty;
    }
}
