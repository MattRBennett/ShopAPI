using Microsoft.EntityFrameworkCore;
using ShopAPI.Data;
using ShopAPI.Models;

namespace ShopAPI.Services.ItemService
{
    public class ItemService : IItemService
    {
        //private static List<Item> items = new List<Item>
        //{
        //    new Item(),
        //    new Item { Id = 1, Name = "Apple", Description = "A delicious fruit", ItemsCategory = ItemCategory.Groceries, Price = 1.49M}

        //};
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

            var dbItems = await _context.items.ToListAsync();

            serviceresponse.Data = dbItems.Select(x => _mapper.Map<GetItemDTO>(x)).ToList() ;

            return serviceresponse;
        }

        public async Task<ServiceResponse<GetItemDTO>> GetItemByID(int ID)
        {
            var serviceResponse = new ServiceResponse<GetItemDTO>();

            //var item = items.FirstOrDefault(x => x.Id == ID);

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

        public async Task<ServiceResponse<List<GetItemDTO>>> AddNewItem(AddItemDTO newItem)
        {
            
                var serviceresponse = new ServiceResponse<List<GetItemDTO>>();

                var item = _mapper.Map<Item>(newItem);

                //item.Id = items.Max(x => x.Id) + 1;

                //items.Add(item);

                await _context.items.AddAsync(item);

                await _context.SaveChangesAsync();
                
                serviceresponse.Data = await _context.items.Select(x => _mapper.Map<GetItemDTO>(x)).ToListAsync();

                return serviceresponse;

        }

        public async Task<ServiceResponse<GetItemDTO>> UpdateItem(UpdateItemDTO item)
        {
            var serviceResponse = new ServiceResponse<GetItemDTO>();

            try
            {
                //var existingItem = items.FirstOrDefault(x => x.Id == item.Id);

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
                //items.Remove(existingItem);

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
    }

    
}
