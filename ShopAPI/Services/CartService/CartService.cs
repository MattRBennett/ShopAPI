using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using ShopAPI.Data;
using ShopAPI.DTOs.Cart;
using ShopAPI.Models;
using Newtonsoft.Json;

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


            var existingCart = await _context.Carts.FirstOrDefaultAsync(x => x.UserID == cartItem.UserID);

            if (existingCart is null)
            {
                try
                {
                    var cart = _mapper.Map<Cart>(cartItem);
                    await _context.Carts.AddAsync(cart);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetCartDTO>(existingCart);
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Successfully created a new cart!";

                }
                catch (Exception ex)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = ex.Message;
                }
            }
            else
            {
                try
                {
                    var FindItem = await _context.Carts.Where(x => x.UserID == cartItem.UserID).FirstOrDefaultAsync();
                    FindItem.CartItems = cartItem.CartItems;
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetCartDTO>(FindItem);
                    serviceResponse.Success = true;
                    serviceResponse.Message = "Successfully updated the cart!";
                }
                catch (Exception ex)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = ex.Message;
                }

            }


            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCartDTO>>> RemoveCart(int UserID)
        {
            var serviceResponse = new ServiceResponse<List<GetCartDTO>>();

            try
            {
                var existingCart = await _context.Carts.FirstOrDefaultAsync(x => x.UserID == UserID);

                if (existingCart is null)
                    throw new Exception($"Cart with UserID '{UserID}' does not exist!");

                _context.Carts.Remove(existingCart);
                await _context.SaveChangesAsync();

                
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

        public async Task<ServiceResponse<GetCartDTO>> RemoveCartItem(int UserID, int ItemID)
        {
            var serviceResponse = new ServiceResponse<GetCartDTO>();

            try
            {
                var cart = await _context.Carts.FirstOrDefaultAsync(x => x.UserID == UserID);

                if (cart is null)
                {
                    serviceResponse.Success = false;
                    //serviceResponse.Message = $"Cart with UserID '{removeCart.UserID}' does not exist.";
                }
                else
                {
                    var DeserializeItems = JsonConvert.DeserializeObject<List<Item>>(cart.CartItems);
                    List<Item> items = new List<Item>();


                    if (DeserializeItems != null)
                    {
                        
                        items.AddRange(DeserializeItems);

                        for (var i = 0; i < items.Count; i++)
                        {
                            if (items[i].Id == ItemID)
                            {
                                items.Remove(items[i]);
                            }
                        }

                        var SeserializedItems = System.Text.Json.JsonSerializer.Serialize(items);

                        cart.CartItems = SeserializedItems;

                        await _context.SaveChangesAsync();
                        serviceResponse.Success = true;
                        serviceResponse.Message = "Item has been deleted from cart.";

                    }
                    else
                    {
                        serviceResponse.Success = false;

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

