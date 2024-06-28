using ShopAPI.Models;

namespace ShopAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        { 
            CreateMap<Item, GetItemDTO>();

            

            CreateMap<AddItemDTO, Item>();
        }
    }
}
