using Blazor.Data;
using Blazor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _Userscontext;

        public UsersController(AppDbContext context)
        {
            _Userscontext = context;
        }
        // find all
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _Userscontext.Users.ToListAsync();
        }

        // find id
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var users = await _Userscontext.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // create
        [HttpPost]
        public async Task<ActionResult<Users>> PostUser(Users users)
        {
            _Userscontext.Users.Add(users);
            await _Userscontext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = users.Id }, User);
        }

        // update id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFile(int id, Users users)
        {
            if (id !=  users.Id)
            {
                return BadRequest();
            }

            _Userscontext.Entry(User).State = EntityState.Modified;

            try
            {
                await _Userscontext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _Userscontext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _Userscontext.Users.Remove(user);
            await _Userscontext.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _Userscontext.Users.Any(e => e.Id == id);
        }
    }
};
