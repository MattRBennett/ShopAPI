using ShopAPI.Models;

namespace ShopAPI.DTOs.Cart
{
    public class AddItemToCartDTO
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public string ItemDescription { get; set; } = string.Empty;
        public decimal ItemPrice { get; set; } = decimal.Zero;
        public ItemCategory ItemCategory { get; set; } = ItemCategory.Unassigned;
        public byte[] Image { get; set; } = new byte[0];

    }
}
