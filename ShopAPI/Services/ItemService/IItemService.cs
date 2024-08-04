using Microsoft.AspNetCore.Mvc;
using ShopAPI.DTOs.Item;
using ShopAPI.Models;

namespace ShopAPI.Services.ItemService
{
    public interface IItemService
    {
        Task<ServiceResponse<List<GetItemDTO>>> GetAllItems();
        Task<ServiceResponse<GetItemDTO>> GetItemByID(int ID);
        Task<ServiceResponse<GetItemDTO>> AddNewItem(AddItemDTO newItem);
        Task<ServiceResponse<GetItemDTO>> UpdateItem(UpdateItemDTO item);
        Task<ServiceResponse<List<GetItemDTO>>> DeleteItem(int Id);
        Task<ServiceResponse<List<ItemCategory>>> GetItemCategories();
        Task<ServiceResponse<List<GetItemDTO>>> GetItemsByCategory(ItemCategory itemCategory);
    }
}