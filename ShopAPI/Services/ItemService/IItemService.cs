using ShopAPI.Models;

namespace ShopAPI.Services.ItemService
{
    public interface IItemService
    {
        Task<ServiceResponse<List<Item>>> GetAllItems();
        Task<ServiceResponse<Item>> GetItemByID(int ID);
        Task<ServiceResponse<Item>> AddNewItem(Item newItem);
    }
}