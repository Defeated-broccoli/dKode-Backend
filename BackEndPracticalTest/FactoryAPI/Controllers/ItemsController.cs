using FactoryAPI.Interfaces;
using FactoryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FactoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository _itemRepository;

        public ItemsController(IItemsRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        // Create a new item
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> CreateItem([FromBody] Item item)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _itemRepository.Add(item);

            if(!result)
            {
                ModelState.AddModelError("", "Failed while adding the item");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Created!");
        }

        // Retrieve a single item
        [HttpGet("id")]
        [ProducesResponseType(200, Type = typeof(Item))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Item>> GetItemById(int id)
        {
            var item = await _itemRepository.GetItemById(id);

            if(item == null)
                return NotFound();

            return Ok(item);
        }

        // Retrieve all items (only if you'll be using an ORM framework)
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Item>))]
        public async Task<ActionResult<List<Item>>> GetAllItems()
        {
            var items = await _itemRepository.GetAllItems();

            return Ok(items);
        }

        // Update an existing item
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateItem([FromBody] Item item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _itemRepository.Update(item);

            if (!result)
            {
                ModelState.AddModelError("", "Failed while updating the item");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Updated!");
        }

        // Delete an item
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteItem(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _itemRepository.Delete(id);

            if(!result)
            {
                ModelState.AddModelError("", "Failed at deleting the item");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Deleted!");
        }

    }
}
