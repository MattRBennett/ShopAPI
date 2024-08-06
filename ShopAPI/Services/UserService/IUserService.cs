using ShopAPI.DTOs.User;
using ShopAPI.Models;

namespace ShopAPI.Services.UserService
{
    public interface IUserService
    {

        Task<ServiceResponse<GetUserDetailsDTO>> GetUserDetailsByID(int UsersID);
    }
}
