using ShopAPI.DTOs.Cart;
using ShopAPI.Models;

namespace ShopAPI.Services.CartService
{
    public interface ICartService
    {
        //Task<ServiceResponse<List<GetAllCartItemsDTO>>> GetAllCartItems();

        Task<ServiceResponse<GetCartDTO>> RemoveCartItem(int UserID, Item item);

        Task<ServiceResponse<List<GetCartDTO>>> RemoveCart(RemoveCartDTO removeCart);

        Task<ServiceResponse<GetCartDTO>> AddCartItem(AddItemToCartDTO cartItem);

        Task<ServiceResponse<GetCartDTO>> AddNewCart(AddCartDTO newCart);

        Task<ServiceResponse<GetCartDTO>> GetCartByUserID(int UserID);

        Task<ServiceResponse<GetCartDTO>> GetCartByCartID(int CartID);
    }
}
