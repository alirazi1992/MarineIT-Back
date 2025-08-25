using MarineIT.Application.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace MarineIT.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _tickets;
    public TicketsController(ITicketService tickets) => _tickets = tickets;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<object>>> List([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        => Ok(await _tickets.ListAsync(page, pageSize)); // ListAsync(page, pageSize) exists. :contentReference[oaicite:2]{index=2}

    [HttpGet("{id:int}")]
    public async Task<ActionResult<object>> Get(int id)
        => (await _tickets.GetAsync(id)) is { } t ? Ok(t) : NotFound(); // GetAsync(id) exists. :contentReference[oaicite:3]{index=3}

    public record CreateTicketRequest(string Title, string Category, int VesselId, int? AssetId, string Priority);

    [HttpPost]
    public async Task<ActionResult<object>> Create([FromBody] CreateTicketRequest body)
    {
        var created = await _tickets.CreateAsync(
            new CreateTicketDto(body.Title, body.Category, body.VesselId, body.AssetId, body.Priority),
            reporterId: "0"
        ); // uses your CreateTicketDto signature. :contentReference[oaicite:4]{index=4}

        var id = created.GetType().GetProperty("Id")?.GetValue(created);
        return Created($"/api/tickets/{id}", created);
    }

    public record UpdateTicketRequest(string? Title, string? Category, int? AssetId, string? Priority, string? Status, DateTime? DueUtc);

    [HttpPut("{id:int}")]
    public async Task<ActionResult<object>> Update(int id, [FromBody] UpdateTicketRequest body)
    {
        var updated = await _tickets.UpdateAsync(
            id,
            new UpdateTicketDto(body.Title, body.Category, body.AssetId, body.Priority, body.Status, body.DueUtc),
            actorId: "0"
        ); // UpdateAsync signature matches. :contentReference[oaicite:5]{index=5}

        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => await _tickets.DeleteAsync(id) ? NoContent() : NotFound(); // DeleteAsync exists. :contentReference[oaicite:6]{index=6}
}
