using ShopAPI.DTOs.User;
using ShopAPI.Models;

namespace ShopAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Item, GetItemDTO>();
            CreateMap<AddItemDTO, Item>();
            CreateMap<UpdateItemDTO, Item>();
            CreateMap<DeleteItemDTO, Item>();

            CreateMap<User, GetUserDetailsDTO>();
        }
    }
}
