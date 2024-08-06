using Microsoft.AspNetCore.Mvc;
using ShopAPI.DTOs.User;
using ShopAPI.Models;
using ShopAPI.Services.UserService;
using System.Runtime.InteropServices;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) 
        { 
            _userService = userService;
        }

        [HttpGet("GetUserDetailsByID")]
        public async Task<ActionResult<ServiceResponse<GetUserDetailsDTO>>> GetUserDetailsByID(int UsersID)
        {
            return Ok(await _userService.GetUserDetailsByID(UsersID));
        }
    }
}
