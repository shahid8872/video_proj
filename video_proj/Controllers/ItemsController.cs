using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using video_proj.DataModel;

namespace StaticApiExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        // Static in-memory list to act as storage
        private static List<Item> Items = new List<Item>
        {
            new Item { Id = 1, Name = "Item One" },
            new Item { Id = 2, Name = "Item Two" }
        };

        // GET: api/items
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetAll()
        {
            return Ok(Items);
        }

        // GET: api/items/{id}
        [HttpGet("{id}")]
        public ActionResult<Item> GetById(int id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/items
        [HttpPost]
        public ActionResult<Item> Create(Item newItem)
        {
            newItem.Id = Items.Any() ? Items.Max(i => i.Id) + 1 : 1;
            Items.Add(newItem);
            return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
        }

        // PUT: api/items/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, Item updatedItem)
        {
            var existingItem = Items.FirstOrDefault(i => i.Id == id);
            if (existingItem == null) return NotFound();

            existingItem.Name = updatedItem.Name;
            return NoContent();
        }

        // DELETE: api/items/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();

            Items.Remove(item);
            return NoContent();
        }
    }
}
