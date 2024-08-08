using ShopAPI.DTOs.User;
using ShopAPI.Models;

namespace ShopAPI.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<GetUserDetailsDTO>> Register(User user, string password);
        Task<ServiceResponse<int>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
