using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopAPI.Data;
using ShopAPI.Models;

namespace ShopAPI.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public ItemService(IMapper mapper, DataContext context) 
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetItemDTO>>> GetAllItems()
        {
            var serviceresponse = new ServiceResponse<List<GetItemDTO>>();

            try
            {
                var dbItems = await _context.items.ToListAsync();

                serviceresponse.Data = dbItems.Select(x => _mapper.Map<GetItemDTO>(x)).ToList();
                serviceresponse.Success = true;
                serviceresponse.Message = "Returned all items successfully!";
            }
            catch (Exception ex)
            {
                serviceresponse.Success = false;
                serviceresponse.Message = ex.Message;
            }

            return serviceresponse;
        }

        public async Task<ServiceResponse<GetItemDTO>> GetItemByID(int ID)
        {
            var serviceResponse = new ServiceResponse<GetItemDTO>();

            try
            {
                var item = await _context.items.FirstOrDefaultAsync(x => x.Id == ID);
                
                if (item is null)
                    throw new Exception("Item not found!");

                serviceResponse.Success = true;
                serviceResponse.Message = "Item found!";
                serviceResponse.Data = _mapper.Map<GetItemDTO>(item);
            }
            catch (Exception ex) 
            { 
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetItemDTO>> AddNewItem(AddItemDTO newItem)
        {
            var serviceresponse = new ServiceResponse<GetItemDTO>();

            try
            {
                var item = _mapper.Map<Item>(newItem);

                await _context.items.AddAsync(item);
                await _context.SaveChangesAsync();

                serviceresponse.Data = _mapper.Map<GetItemDTO>(item);
                serviceresponse.Success = true;
                serviceresponse.Message = "New item has been added successfully!";
            }
            catch (Exception ex)
            {
                serviceresponse.Success = false;
                serviceresponse.Message = ex.Message;
            }     

            return serviceresponse;
        }

        public async Task<ServiceResponse<GetItemDTO>> UpdateItem(UpdateItemDTO item)
        {
            var serviceResponse = new ServiceResponse<GetItemDTO>();

            try
            {
                var existingItem = await _context.items.FirstOrDefaultAsync(x => x.Id == item.Id);

                if (existingItem is null)
                    throw new Exception($"Item with Id '{item.Id}' does not exist!");

                existingItem.Name = item.Name;
                existingItem.Description = item.Description;
                existingItem.ItemsCategory = item.ItemsCategory;
                existingItem.Price = item.Price;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetItemDTO>(existingItem);
                serviceResponse.Success = true;
                serviceResponse.Message = "Successfully updated!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetItemDTO>>> DeleteItem(int Id)
        {
            var serviceResponse = new ServiceResponse<List<GetItemDTO>>();

            try
            {
                var existingItem = await _context.items.FirstOrDefaultAsync(x => x.Id == Id);
                if (existingItem is null)
                    throw new Exception($"Item with Id '{Id}' does not exist!");
                
                _context.items.Remove(existingItem);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.items.Select(x => _mapper.Map<GetItemDTO>(x)).ToListAsync();
                serviceResponse.Success = true;
                serviceResponse.Message = "Successfully deleted!";
            }
            catch (Exception ex) 
            { 
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ItemCategory>>> GetItemCategories()
        {
            var serviceResponse = new ServiceResponse<List<ItemCategory>>();

            try
            {
                var categoryItems = Enum.GetValues(typeof(ItemCategory)).Cast<ItemCategory>().ToList();

                if (categoryItems.Count == 0 || categoryItems is null)
                    throw new Exception("There are no categories to display.");

                serviceResponse.Data = categoryItems;
                serviceResponse.Success = true;
                serviceResponse.Message = "Successfully returned categories!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
