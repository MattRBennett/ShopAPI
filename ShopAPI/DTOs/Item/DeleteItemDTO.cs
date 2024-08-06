using ShopAPI.Models;

namespace ShopAPI.DTOs.Item
{
    public class DeleteItemDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = "New Item Name";

        public string Description { get; set; } = "New Item Description";

        public decimal Price { get; set; } = decimal.Zero;

        public byte[] Image { get; set; } = new byte[0];

        public ItemCategory ItemsCategory { get; set; } = ItemCategory.Unassigned;
    }
}
