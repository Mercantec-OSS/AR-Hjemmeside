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
    public class FilesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FilesController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Files>>> GetFiles()
        {
            return await _context.File.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Files>> GetFile(int id)
        {
            var file = await _context.File.FindAsync(id);

            if (file == null)
            {
                return NotFound();
            }

            return file;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFile(int id, Files file)
        {
            if (id != file.file_id)
            {
                return BadRequest();
            }

            _context.Entry(file).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileExists(id))
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
        public async Task<ActionResult<Files>> PostFile(Files file)
        {
            _context.File.Add(file);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFile", new { id = file.file_id }, file);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var file = await _context.File.FindAsync(id);
            if (file == null)
            {
                return NotFound();
            }

            _context.File.Remove(file);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FileExists(int id)
        {
            return _context.File.Any(e => e.file_id == id);
        }
    }
}

