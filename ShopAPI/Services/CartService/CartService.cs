using Microsoft.EntityFrameworkCore;
using ShopAPI.Data;
using ShopAPI.DTOs.Cart;
using ShopAPI.Models;

namespace ShopAPI.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CartService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<GetCartDTO>> GetCartByCartID(int CartID)
        {
            var serviceResponse = new ServiceResponse<GetCartDTO>();

            try
            {
                var cart = await _context.Carts.FirstOrDefaultAsync(x => x.CartID == CartID);

                if (cart is null)
                    throw new Exception("Cart not found!");

                serviceResponse.Success = true;
                serviceResponse.Message = "Cart found!";
                serviceResponse.Data = _mapper.Map<GetCartDTO>(cart);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCartDTO>> GetCartByUserID(int UserID)
        {
            var serviceResponse = new ServiceResponse<GetCartDTO>();

            try
            {
                var cart = await _context.Carts.FirstOrDefaultAsync(x => x.UserID == UserID);

                if (cart is null)
                    throw new Exception("Cart not found!");

                serviceResponse.Success = true;
                serviceResponse.Message = "Cart found!";
                serviceResponse.Data = _mapper.Map<GetCartDTO>(cart);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCartDTO>> AddNewCart(AddCartDTO newCart)
        {
            var serviceresponse = new ServiceResponse<GetCartDTO>();

            try
            {
                //Cart cart = new Cart 
                //{ 
                //    UserID = newCart.UserID,
                //    CartTotal = newCart.CartTotal
                //};
                var cart = _mapper.Map<Cart>(newCart);

                await _context.Carts.AddAsync(cart);
                await _context.SaveChangesAsync();

                serviceresponse.Data = _mapper.Map<GetCartDTO>(cart);
                serviceresponse.Success = true;
                serviceresponse.Message = "New cart has been added successfully!";
            }
            catch (Exception ex)
            {
                serviceresponse.Success = false;
                serviceresponse.Message = ex.Message;
            }

            return serviceresponse;
        }

        public async Task<ServiceResponse<GetCartDTO>> AddCartItem(AddItemToCartDTO cartItem)
        {
            var serviceResponse = new ServiceResponse<GetCartDTO>();

            try
            {
                var existingCart= await _context.Carts.FirstOrDefaultAsync(x => x.UserID == cartItem.UserID);

                if (existingCart is null)
                    throw new Exception($"Cart with UserID '{cartItem.UserID}' does not exist!");

                Item newItem = new Item
                {
                    Name = cartItem.ItemName,
                    Description = cartItem.ItemDescription,
                    Price = cartItem.ItemPrice,
                    Id = cartItem.ItemID,
                    Image = cartItem.Image,
                    ItemsCategory = cartItem.ItemCategory
                };

                existingCart.CartItems.Add(newItem);
                existingCart.CartTotal += cartItem.ItemPrice;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCartDTO>(existingCart);
                serviceResponse.Success = true;
                serviceResponse.Message = "Successfully updated the cart!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCartDTO>>> RemoveCart(RemoveCartDTO removeCart)
        {
            var serviceResponse = new ServiceResponse<List<GetCartDTO>>();

            try
            {
                var existingCart = await _context.Carts.FirstOrDefaultAsync(x => x.CartID == removeCart.CartID && x.UserID == removeCart.UserID);

                if (existingCart is null)
                    throw new Exception($"Cart with CartID '{removeCart.CartID}' & UserID '{removeCart.UserID}' does not exist!");

                _context.Carts.Remove(existingCart);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Carts.Select(x => _mapper.Map<GetCartDTO>(x)).ToListAsync();
                serviceResponse.Success = true;
                serviceResponse.Message = "Successfully deleted cart!";
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCartDTO>> RemoveCartItem(int UserID, Item item)
        {
            var serviceResponse = new ServiceResponse<GetCartDTO>();

            try
            {
                var cart = await _context.Carts.FirstOrDefaultAsync(x => x.UserID == UserID);

                if (cart is null)
                {
                    serviceResponse.Success=false;
                    serviceResponse.Message = $"Cart with UserID '{UserID}' does not exist.";
                }
                else
                {
                    var ItemExists = cart.CartItems.Select(x => x.Id == item.Id);

                    if (ItemExists is null)
                    {
                        serviceResponse.Success=false;
                        serviceResponse.Message = $"Item with Id '{item.Id}' does not exist.";
                    }
                    else
                    {
                        bool DeletedItem = cart.CartItems.Remove(item);
                        cart.CartTotal -= item.Price;
                        await _context.SaveChangesAsync();
                        serviceResponse.Success = true;
                        serviceResponse.Message = "Item has been deleted from cart.";
                    }

                }
                
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        //public async Task<ServiceResponse<List<GetAllCartItemsDTO>>> GetAllCartItems(int UserID)
        //{
        //    var serviceresponse = new ServiceResponse<List<GetAllCartItemsDTO>>();

        //    try
        //    {
        //        var cart = await _context.Carts.FirstOrDefaultAsync(x => x.UserID == UserID);

        //        if (cart is null)
        //        {
        //            serviceresponse.Success = false;
        //        }
        //        else
        //        {
        //            var items = cart.CartItems.ToList();

        //            serviceresponse.Data = items;
        //            serviceresponse.Success = true;
        //            serviceresponse.Message = "Returned all cart items successfully!";
        //        }

                
        //    }
        //    catch (Exception ex)
        //    {
        //        serviceresponse.Success = false;
        //        serviceresponse.Message = ex.Message;
        //    }

        //    return serviceresponse;
        //}


    }
}

