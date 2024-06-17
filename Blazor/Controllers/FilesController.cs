using Blazor.Data;
using Blazor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly AppDbContext _Filecontext;

        public FilesController(AppDbContext context)
        {
            _Filecontext = context;
        }
        // find all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Files>>> GetFiles()
        {
            return await _Filecontext.Files.ToListAsync();
        }

        // find id
        [HttpGet("{id}")]
        public async Task<ActionResult<Files>> GetFile(int id)
        {
            var files = await _Filecontext.Files.FindAsync(id);

            if (files == null)
            {
                return NotFound();
            }

            return files;
        }

        // create
        [HttpPost]
        public async Task<ActionResult<Files>> PostFile(Files files)
        {
            _Filecontext.Files.Add(files);
            await _Filecontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFile), new { id = files.file_id }, files);
        }

        // update id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFile(int id, Files files)
        {
            if (id != files.file_id)
            {
                return BadRequest();
            }

            _Filecontext.Entry(files).State = EntityState.Modified;

            try
            {
                await _Filecontext.SaveChangesAsync();
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

        // delete id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFile(int id)
        {
            var files = await _Filecontext.Files.FindAsync(id);
            if (files == null)
            {
                return NotFound();
            }

            _Filecontext.Files.Remove(files);
            await _Filecontext.SaveChangesAsync();

            return NoContent();
        }

        private bool FileExists(int id)
        {
            return _Filecontext.Files.Any(e => e.file_id == id);
        }
    }
};
