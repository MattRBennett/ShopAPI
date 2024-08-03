namespace ShopAPI.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; } = "New Item Name";

        public string Description { get; set; } = "New Item Description";

        public decimal Price { get; set; } = decimal.Zero;

        public ItemCategory ItemsCategory { get; set; } = ItemCategory.Unassigned;
    }
}

// dotnet ef migrations add <MigrationName>
// dotnet ef database update
