using MarineIT.Application.Vessels;
using Microsoft.AspNetCore.Mvc;

namespace MarineIT.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VesselsController : ControllerBase
{
    private readonly IVesselService _vessels;
    public VesselsController(IVesselService vessels) => _vessels = vessels;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<object>>> List()
        => Ok(await _vessels.ListAsync()); // ListAsync exists. :contentReference[oaicite:7]{index=7}

    public record CreateVesselRequest(string Name, string? Imo);

    [HttpPost]
    public async Task<ActionResult<object>> Create([FromBody] CreateVesselRequest body)
    {
        var created = await _vessels.CreateAsync(body.Name, body.Imo); // CreateAsync exists. :contentReference[oaicite:8]{index=8}
        var id = created.GetType().GetProperty("Id")?.GetValue(created);
        return Created($"/api/vessels/{id}", created);
    }
}
