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
    public class SubjectsController : ControllerBase
    {
        private readonly AppDbContext _subjectcontext;

        public SubjectsController(AppDbContext context)
        {
            _subjectcontext = context;
        }
        // find all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subjects>>> Getsubjects()
        {
            return await _subjectcontext.subjects.ToListAsync();
        }

        // find id
        [HttpGet("{id}")]
        public async Task<ActionResult<Subjects>> Getsubjects(int id)
        {
            var subject = await _subjectcontext.subjects.FindAsync(id);

            if (subject== null)
            {
                return NotFound();
            }

            return subject;
        }

        // create
        [HttpPost]
        public async Task<ActionResult<Category>> Postsubject(Subjects subjects)
        {
            _subjectcontext.subjects.Add(subjects);
            await _subjectcontext.SaveChangesAsync();

            return CreatedAtAction(nameof(Getsubjects), new { id = subjects.subject_id }, subjects);
        }

        // update id
        [HttpPut("{id}")]
        public async Task<IActionResult> Putsubject(int id, Subjects subjects)
        {
            if (id != subjects.subject_id)
            {
                return BadRequest();
            }

            _subjectcontext.Entry(subjects).State = EntityState.Modified;

            try
            {
                await _subjectcontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!subjectExists(id))
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
        public async Task<IActionResult> Deletesubject(int id)
        {
            var subject = await _subjectcontext.subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _subjectcontext.subjects.Remove(subject);
            await _subjectcontext.SaveChangesAsync();

            return NoContent();
        }

        private bool subjectExists(int id)
        {
            return _subjectcontext.subjects.Any(e => e.subject_id == id);
        }
    }
};
