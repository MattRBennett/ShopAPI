using System.Numerics;

namespace ShopAPI.DTOs.Cart
{
    public class GetCartDTO
    {
        public int CartID { get; set; }
        public int UserID { get; set; }

        public string CartItems { get; set; } = string.Empty;
    }
}
