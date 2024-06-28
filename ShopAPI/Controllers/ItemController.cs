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
        public async Task<ActionResult<ServiceResponse<AddItemDTO>>> AddNewItem(AddItemDTO newItem)
        {
            return Ok(await _itemService.AddNewItem(newItem));
        }
    }
}
