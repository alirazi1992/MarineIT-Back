using MarineIT.Domain.Entities;
using MarineIT.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarinIT.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public TicketsController(AppDbContext db) => _db = db;

        // GET /api/tickets?page=1&pageSize=20
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            if (page < 1) page = 1;
            if (pageSize < 1 || pageSize > 200) pageSize = 20;

            var query = _db.Tickets
                .AsNoTracking()
                .OrderByDescending(t => t.CreatedUtc);

            var total = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new { total, page, pageSize, items });
        }

        // GET /api/tickets/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ticket = await _db.Tickets
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            return ticket is null ? NotFound() : Ok(ticket);
        }

        // POST /api/tickets
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ticket ticket)
        {
            ticket.CreatedUtc = DateTime.UtcNow;
            _db.Tickets.Add(ticket);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = ticket.Id }, ticket);
        }

        // PUT /api/tickets/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Ticket update)
        {
            var t = await _db.Tickets.FindAsync(id);
            if (t is null) return NotFound();

            t.Title = update.Title;
            t.Category = update.Category;
            t.Priority = update.Priority;
            t.Status = update.Status;
            t.VesselId = update.VesselId;
            t.AssetId = update.AssetId;
            t.SLAId = update.SLAId;
            t.DueUtc = update.DueUtc;
            t.ResolvedUtc = update.ResolvedUtc;

            await _db.SaveChangesAsync();
            return Ok(t);
        }

        // DELETE /api/tickets/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var t = await _db.Tickets.FindAsync(id);
            if (t is null) return NotFound();
            _db.Tickets.Remove(t);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
