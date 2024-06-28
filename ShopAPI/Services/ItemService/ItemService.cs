using ShopAPI.Models;

namespace ShopAPI.Services.ItemService
{
    public class ItemService : IItemService
    {
        private static List<Item> items = new List<Item>
        {
            new Item(),
            new Item { Id = 1, Name = "Apple", Description = "A delicious fruit", ItemsCategory = ItemCategory.Groceries, Price = 1.49M}

        };
        private readonly IMapper _mapper;

        public ItemService(IMapper mapper) 
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetItemDTO>>> GetAllItems()
        {
            var serviceresponse = new ServiceResponse<List<GetItemDTO>>();

            serviceresponse.Data = items.Select(x => _mapper.Map<GetItemDTO>(x)).ToList() ;

            return serviceresponse;
        }

        public async Task<ServiceResponse<GetItemDTO>> GetItemByID(int ID)
        {
            var serviceResponse = new ServiceResponse<GetItemDTO>();

            var item = items.FirstOrDefault(x => x.Id == ID);

            serviceResponse.Data = _mapper.Map<GetItemDTO>(item);

            if (serviceResponse is not null)
                return serviceResponse;
            throw new Exception("Item not found!");
        }

        public async Task<ServiceResponse<AddItemDTO>> AddNewItem(AddItemDTO newItem)
        {
            var serviceresponse = new ServiceResponse<AddItemDTO>();

            var item = _mapper.Map<AddItemDTO>(newItem);

            serviceresponse.Data = _mapper.Map<AddItemDTO>(item);

            return serviceresponse;
        }
    }
}
