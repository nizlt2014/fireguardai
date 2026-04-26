using Microsoft.AspNetCore.Mvc;
using FireGuard.Application.Interfaces;
using FireGuard.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace FireGuard.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SitesController : ControllerBase
{
    private readonly ISiteRepository _repo;

    public SitesController(ISiteRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _repo.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var site = await _repo.GetByIdAsync(id);
        if (site == null) return NotFound();

        return Ok(site);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Site site)
    {
        var id = await _repo.CreateAsync(site);
        site.Id = id;

        return CreatedAtAction(nameof(Get), new { id }, site);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Site site)
    {
        site.Id = id;

        var updated = await _repo.UpdateAsync(site);

        if (!updated) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repo.DeleteAsync(id);

        if (!deleted) return NotFound();

        return NoContent();
    }
}