namespace ShopAPI.DTOs.Cart
{
    public class GetAllCartItemsDTO
    {
        public int UserID { get; set; }
        public string CartItems { get; set; } = string.Empty;
    }
}
