using Blazor.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

 /*namespace Blazor.Controllers
{
   [Route("api/[controller]")]
    [ApiController]
    public class ModelTagsController : ControllerBase
    {
        private readonly AppDbContext _model_tags_context;

        public ModelTagsController(AppDbContext context)
        {
            _model_tags_context = context;
        }

        // find all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelTag>>> GetModelTags()
        {
            return await _model_tags_context.Model_Tag.ToListAsync();
        }
            // find id
            [HttpGet("{id}")]
        public async Task<ActionResult<ModelTag>> GetModelTag(int id)
        {
            var modelTag = await _model_tags_context.Model_Tag.FindAsync(id);

            if (modelTag == null)
            {
                return NotFound();
            }

            return modelTag;
        }

        // create
        [HttpPost]
        public async Task<ActionResult<ModelTag>> PostModelTag(ModelTag modelTag)
        {
            _model_tags_context.Model_Tag.Add(modelTag);
            await _model_tags_context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetModelTag), new { id = modelTag.Id }, modelTag);
        }

        // update id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModelTag(int id, ModelTag modelTag)
        {
            if (id != modelTag.Id)
            {
                return BadRequest();
            }

            _model_tags_context.Entry(modelTag).State = EntityState.Modified;

            try
            {
                await _model_tags_context.SaveChangesAsync();
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

        // delete id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModelTag(int id)
        {
            var modelTag = await _model_tags_context.Model_Tag.FindAsync(id);
            if (modelTag == null)
            {
                return NotFound();
            }

            _model_tags_context.Model_Tag.Remove(modelTag);
            await _model_tags_context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModelTagExists(int id)
        {
            return _model_tags_context.Model_Tag.Any(e => e.Id == id);
        }
    }

    // Assuming the ModelTag class is defined as follows:
    public class ModelTag
    {
        public int Id { get; set; }
        // Add other properties as required
    }
}*/
