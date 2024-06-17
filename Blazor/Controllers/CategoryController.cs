using Blazor.Data;
using Blazor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;



namespace Blazor.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
        private readonly AppDbContext _Categorycontext;

		public CategoryController(AppDbContext context)
		{
			_Categorycontext = context;
		}
        // find all
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
		{
			return await _Categorycontext.Category.ToListAsync();
		}

        // find id
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _Categorycontext.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // create
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _Categorycontext.Category.Add(category);
            await _Categorycontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategory), new { id = category.file_id }, category);
        }

        // update id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.file_id)
            {
                return BadRequest();
            }

            _Categorycontext.Entry(category).State = EntityState.Modified;

            try
            {
                await _Categorycontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // delete id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _Categorycontext.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _Categorycontext.Category.Remove(category);
            await _Categorycontext.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _Categorycontext.Category.Any(e => e.file_id == id);
        }
    }
};
	