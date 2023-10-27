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
            //dependency injection
            _itemRepository = itemRepository;
        }

        // Create a new item
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> CreateItem([FromBody] Item item)
        {
            if(item.Price < 0)
            {
                ModelState.AddModelError("Error", "You should be possitive in your life, especially when setting the price!");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _itemRepository.Add(item);

            if(!result)
            {
                //if there are no changes in database after adding (Add must have failed)
                ModelState.AddModelError("Error", "Failed while adding the item");
                return StatusCode(500, ModelState);
            }

            return Ok(item.ID);
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
            //check item validation
            if (item.Price < 0)
            {
                ModelState.AddModelError("Error", "You should be possitive in your life, especially when setting the price!");
                return StatusCode(400, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var dbItem = await _itemRepository.GetItemById(item.ID);

            if (dbItem == null)
            {
                ModelState.AddModelError("Error", "Couldn't find the item");
                return NotFound(ModelState);
            }

            var result = await _itemRepository.Update(item);

            if (!result)
            {
                ModelState.AddModelError("Error", "Failed while updating the item");
                return StatusCode(500, ModelState);
            }

            return Ok(item.ID);
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

            var dbItem = await _itemRepository.GetItemById(id);

            if(dbItem == null)
            {
                ModelState.AddModelError("Error", "Couldn't find the item");
                return NotFound(ModelState);
            }

            var result = await _itemRepository.Delete(id);

            if(!result)
            {
                ModelState.AddModelError("Error", "Failed at deleting the item");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Deleted!");
        }

    }
}
