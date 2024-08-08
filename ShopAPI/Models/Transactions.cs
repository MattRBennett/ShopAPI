namespace ShopAPI.Models
{
    public class Transactions
    {
        public int TranscationID { get; set; }
        public List<Item> PurchasedItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
