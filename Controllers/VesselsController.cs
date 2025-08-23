using MarineIT.Domain.Entities;
using MarineIT.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarinIT.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VesselsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public VesselsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> List()
            => Ok(await _db.Vessels.AsNoTracking().OrderBy(v => v.Name).ToListAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Vessel v)
        {
            _db.Vessels.Add(v);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(List), new { id = v.Id }, v);
        }
    }
}
