using Microsoft.AspNetCore.Mvc;
using ShopAPI.DTOs.Cart;
using ShopAPI.Models;
using ShopAPI.Services.CartService;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        //[HttpGet("GetAllCartItems")]
        //public async Task<ActionResult<List<GetAllCartItemsDTO>>> GetAllCartItems()
        //{
        //    return Ok(await _cartService.GetAllCartItems());
        //}

        [HttpGet("GetCartByCartID")]
        public async Task<ActionResult<GetCartDTO>> GetCartByCartID(int CartID)
        {
            return Ok(await _cartService.GetCartByCartID(CartID));
        }

        [HttpGet("GetCartByUserID")]
        public async Task<ActionResult<GetCartDTO>> GetCartByUserID(int UserID)
        {
            return Ok(await _cartService.GetCartByUserID(UserID));
        }
        [HttpPost("AddNewCart")]
        public async Task<ActionResult<GetCartDTO>> AddNewCart(AddCartDTO newCart)
        {
            return Ok(await _cartService.AddNewCart(newCart));
        }
        [HttpPost("AddCartItem")]
        public async Task<ActionResult<GetCartDTO>> AddCartItem(AddItemToCartDTO cartItem)
        {
            return Ok(await _cartService.AddCartItem(cartItem));
        }
        [HttpDelete("RemoveCart")]
        public async Task<ActionResult<List<GetCartDTO>>> RemoveCart(RemoveCartDTO removeCart)
        {
            return Ok(await _cartService.RemoveCart(removeCart));
        }
        [HttpDelete("RemoveCartItem")]
        public async Task<ActionResult<GetCartDTO>> RemoveCartItem(int UserID, int ItemID)
        {
            return Ok(await _cartService.RemoveCartItem(UserID, ItemID));
        }
        
    }
}
