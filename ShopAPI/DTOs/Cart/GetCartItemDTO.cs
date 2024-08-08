using ShopAPI.Models;

namespace ShopAPI.DTOs.Cart
{
    public class GetCartItemDTO
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public List<Models.Item> CartItems { get; set; } = new List<Models.Item>();
        public decimal CartTotal { get; set; } = decimal.Zero;
    }
}
