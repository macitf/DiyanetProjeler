using Microsoft.EntityFrameworkCore;
using MacitMustafa.Data;
using MacitMustafa.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class IlController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public IlController(ApplicationDbContext context)
    {
        _context = context;
    }

    // tüm illeri getirir
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Il>>> GetIller()
    {
        return await _context.Iller.ToListAsync();
    }

    // id ile il getirir
    [HttpGet("{id}")]
    public async Task<ActionResult<Il>> GetIl(int id)
    {
        var il = await _context.Iller.FirstOrDefaultAsync(i => i.Id == id);

        if (il == null)
        {
            return NotFound();
        }

        return il;
    }

    // queryden yeni il ekle
    [HttpPost]
    public async Task<ActionResult<Il>> AddIl([FromBody] Il il)
    {
        _context.Iller.Add(il);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetIl), new { id = il.Id }, il);
    }

    // id ile il sil
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIl(int id)
    {
        var il = await _context.Iller.FirstOrDefaultAsync(i => i.Id == id);

        if (il == null)
        {
            return NotFound();
        }

        _context.Iller.Remove(il);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // id ile il adı güncelle
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIl(int id, [FromBody] Il updatedIl)
    {
        if (id != updatedIl.Id)
        {
            return BadRequest();
        }

        _context.Entry(updatedIl).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Iller.Any(e => e.Id == id))
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
}
