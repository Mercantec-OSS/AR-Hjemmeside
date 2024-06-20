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
    public class QrCodesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QrCodesController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<qr_code>>> GetQrCodes()
        {
            return await _context.qr_Code.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<qr_code>> GetQrCode(int id)
        {
            var qrCode = await _context.qr_Code.FindAsync(id);

            if (qrCode == null)
            {
                return NotFound();
            }

            return qrCode;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQrCode(int id, qr_code qrCode)
        {
            if (id != qrCode.id)
            {
                return BadRequest();
            }

            _context.Entry(qrCode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QrCodeExists(id))
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
        public async Task<ActionResult<qr_code>> PostQrCode(qr_code qrCode)
        {
            _context.qr_Code.Add(qrCode);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQrCode", new { id = qrCode.id }, qrCode);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQrCode(int id)
        {
            var qrCode = await _context.qr_Code.FindAsync(id);
            if (qrCode == null)
            {
                return NotFound();
            }

            _context.qr_Code.Remove(qrCode);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QrCodeExists(int id)
        {
            return _context.qr_Code.Any(e => e.id == id);
        }
    }
}
