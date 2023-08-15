using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CountryGwp.Models;

namespace CountryGwp.Controllers
{
    /// <summary>
    /// Controller for CRUD operations on GWP items
    /// </summary>
    [Route("server/api/gwp/[controller]")]
    [ApiController]
    public class GwpItemsController : ControllerBase
    {
        private readonly GwpContext _context;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="context">Database context</param>
        public GwpItemsController(GwpContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all GwpItem from database
        /// </summary>
        /// <returns>List of GwpItem present in the database</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GwpItem>>> GetGwpItems()
        {
          if (_context.GwpItems == null)
          {
              return NotFound();
          }
            return await _context.GwpItems.ToListAsync();
        }

        /// <summary>
        /// Gets a GwpItem with given id
        /// </summary>
        /// <param name="id">Id of the GwpItem</param>
        /// <returns>GwpItem corresponding to given id, 404 if not found</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GwpItem>> GetGwpItem(long id)
        {
          if (_context.GwpItems == null)
          {
              return NotFound();
          }
            var gwpItem = await _context.GwpItems.FindAsync(id);

            if (gwpItem == null)
            {
                return NotFound();
            }

            return gwpItem;
        }

        /// <summary>
        /// Updates an existing GwpItem
        /// </summary>
        /// <param name="id">Id of the GwpItem</param>
        /// <param name="gwpItem">Updated GwpItem</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGwpItem(long id, GwpItem gwpItem)
        {
            if (id != gwpItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(gwpItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GwpItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Creates new GwpItem
        /// </summary>
        /// <param name="gwpItem">GwpItem object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<GwpItem>> PostGwpItem(GwpItem gwpItem)
        {
          if (_context.GwpItems == null)
          {
              return Problem("Entity set 'GwpContext.GwpItems'  is null.");
          }
            _context.GwpItems.Add(gwpItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGwpItem), new { id = gwpItem.Id }, gwpItem);
        }

        /// <summary>
        /// Deletes GwpItem from database with given id
        /// </summary>
        /// <param name="id">Id of the GwpItem<</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGwpItem(long id)
        {
            if (_context.GwpItems == null)
            {
                return NotFound();
            }
            var gwpItem = await _context.GwpItems.FindAsync(id);
            if (gwpItem == null)
            {
                return NotFound();
            }

            _context.GwpItems.Remove(gwpItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Check if a GwpItem exists with given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool GwpItemExists(long id)
        {
            return (_context.GwpItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
