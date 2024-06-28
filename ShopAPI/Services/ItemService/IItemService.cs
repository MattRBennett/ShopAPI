using ShopAPI.DTOs.Item;
using ShopAPI.Models;

namespace ShopAPI.Services.ItemService
{
    public interface IItemService
    {
        Task<ServiceResponse<List<GetItemDTO>>> GetAllItems();
        Task<ServiceResponse<GetItemDTO>> GetItemByID(int ID);
        Task<ServiceResponse<AddItemDTO>> AddNewItem(AddItemDTO newItem);
    }
}