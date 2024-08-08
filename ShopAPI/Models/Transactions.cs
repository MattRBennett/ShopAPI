namespace ShopAPI.Models
{
    public class Transactions
    {
        public int TranscationID { get; set; }
        public string PurchasedItems { get; set; } = string.Empty;
        //public List<Item> PurchasedItems { get; set; }
        ///public decimal TotalPrice { get; set; }
    }
}
