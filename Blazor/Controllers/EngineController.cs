using Microsoft.AspNetCore.Mvc;
using Blazor.Data;
using Blazor.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreeDModelsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ThreeDModelsController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<_3DModels>>> GetThreeDModels()
        {
            return await _context.ThreeDModels.ToListAsync();
        }

        
        [HttpGet("{object_name}")]
        public async Task<ActionResult<_3DModels>> GetThreeDModel(string object_name)
        {
            var threeDModel = await _context.ThreeDModels.FindAsync(object_name);

            if (threeDModel == null)
            {
                return NotFound();
            }

            return threeDModel;
        }

        
        [HttpPut("{object_name}")]
        public async Task<IActionResult> PutThreeDModel(string object_name, _3DModels threeDModel)
        {
            if (object_name != threeDModel.object_name)
            {
                return BadRequest();
            }

            _context.Entry(threeDModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThreeDModelExists(object_name))
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
        public async Task<ActionResult<_3DModels>> PostThreeDModel(_3DModels threeDModel)
        {
            _context.ThreeDModels.Add(threeDModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetThreeDModel", new { object_name = threeDModel.object_name }, threeDModel);
        }

        
        [HttpDelete("{object_name}")]
        public async Task<IActionResult> DeleteThreeDModel(string object_name)
        {
            var threeDModel = await _context.ThreeDModels.FindAsync(object_name);
            if (threeDModel == null)
            {
                return NotFound();
            }

            _context.ThreeDModels.Remove(threeDModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ThreeDModelExists(string object_name)
        {
            return _context.ThreeDModels.Any(e => e.object_name == object_name);
        }
    }
}

