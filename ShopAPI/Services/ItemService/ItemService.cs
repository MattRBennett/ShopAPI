using ShopAPI.Models;

namespace ShopAPI.Services.ItemService
{
    public class ItemService : IItemService
    {
        private static List<Item> items = new List<Item>
        {
            new Item(),
            new Item {Name = "Apple", Description = "A delicious fruit", ItemsCategory = ItemCategory.Groceries, Price = 1.49M}

        };

        public async Task<ServiceResponse<List<Item>>> GetAllItems()
        {
            var serviceresponse = new ServiceResponse<List<Item>>();
            serviceresponse.Data = items;
            return serviceresponse;
        }

        public async Task<ServiceResponse<Item>> GetItemByID(int ID)
        {
            var serviceResponse = new ServiceResponse<Item>();

            var item = items.FirstOrDefault(x => x.Id == ID);

            serviceResponse.Data = item;

            if (serviceResponse is not null)
                return serviceResponse;
            throw new Exception("Item not found!");
        }

        public async Task<ServiceResponse<Item>> AddNewItem(Item newItem)
        {
            var serviceresponse = new ServiceResponse<Item>();

            items.Add(newItem);

            return serviceresponse;
        }
    }
}
