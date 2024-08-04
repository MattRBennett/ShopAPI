using Microsoft.AspNetCore.Mvc;
using ShopAPI.Models;
using ShopAPI.Services.ItemService;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService) 
        {
            _itemService = itemService;
        }

        [HttpGet("GetAllItems")]
        public async Task<ActionResult<ServiceResponse<List<GetItemDTO>>>> GetAllItems()
        {
            return Ok(await _itemService.GetAllItems());
        }

        [HttpGet("{ID}")]
        public async Task<ActionResult<ServiceResponse<GetItemDTO>>> GetItemByID(int ID)
        {
            return Ok(await _itemService.GetItemByID(ID));
        }

        [HttpPost("AddNewItem")]
        public async Task<ActionResult<ServiceResponse<GetItemDTO>>> AddNewItem(AddItemDTO newItem)
        {
            return Ok(await _itemService.AddNewItem(newItem));
        }

        [HttpPut("UpdateItem")]
        public async Task<ActionResult<ServiceResponse<GetItemDTO>>> UpdateItem(UpdateItemDTO item)
        {
            var response = await _itemService.UpdateItem(item);

            if (response.Data is null) 
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpDelete("DeleteItem")]
        public async Task<ActionResult<ServiceResponse<List<GetItemDTO>>>> DeleteItem(int Id)
        {
            var response = await _itemService.DeleteItem(Id);

            if (response.Data is null)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpGet("GetItemCategories")]
        public async Task<ActionResult<ServiceResponse<List<ItemCategory>>>> GetItemCategories()
        {
            var response = await _itemService.GetItemCategories();

            if (response.Data is null)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpGet("GetItemsByCategory")]
        public async Task<ActionResult<ServiceResponse<List<GetItemDTO>>>> GetItemsByCategory(ItemCategory itemCategory)
        {
            var response = await _itemService.GetItemsByCategory(itemCategory);

            if (response.Data is null)
            {
                return NotFound(response);
            }
            else
            {
                return Ok(response);
            }
        }
    }
}
