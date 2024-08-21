using Microsoft.AspNetCore.Mvc;
using Blazor.Data;
using Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelTagsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ModelTagsController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<model_tags>>> GetModelTags()
        {
            return await _context.Model_Tag.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<model_tags>> GetModelTag(int id)
        {
            var modelTag = await _context.Model_Tag.FindAsync(id);

            if (modelTag == null)
            {
                return NotFound();
            }

            return modelTag;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModelTag(int id, model_tags modelTag)
        {
            if (id != modelTag.model_id)
            {
                return BadRequest();
            }

            _context.Entry(modelTag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelTagExists(id))
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

        
        [HttpPost]
        public async Task<ActionResult<model_tags>> PostModelTag(model_tags modelTag)
        {
            _context.Model_Tag.Add(modelTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModelTag", new { id = modelTag.model_id }, modelTag);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModelTag(int id)
        {
            var modelTag = await _context.Model_Tag.FindAsync(id);
            if (modelTag == null)
            {
                return NotFound();
            }

            _context.Model_Tag.Remove(modelTag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModelTagExists(int id)
        {
            return _context.Model_Tag.Any(e => e.model_id == id);
        }
    }
}

