using Microsoft.EntityFrameworkCore;
using ShopAPI.Data;
using ShopAPI.DTOs.User;
using ShopAPI.Models;
using System.Diagnostics.Eventing.Reader;

namespace ShopAPI.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UserService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetUserDetailsDTO>> GetUserDetailsByID(int UsersID)
        {
            var serviceresponse = new ServiceResponse<GetUserDetailsDTO>();

            try
            {
                var UserDetails = await _context.Users.FirstOrDefaultAsync(x => x.Id == UsersID);

                if (UserDetails != null)
                {
                    serviceresponse.Data = _mapper.Map<GetUserDetailsDTO>(UserDetails);
                    serviceresponse.Success = true;
                    serviceresponse.Message = $"Successfully retrieved {UserDetails.Username}'s details.";

                }
                else
                {
                    serviceresponse.Success = false;
                    serviceresponse.Message = $"Failed to find details for that user.";
                }

                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetUserDetailsByID Error: {ex.Message}");
            }

            return serviceresponse;
        }
    }
}
