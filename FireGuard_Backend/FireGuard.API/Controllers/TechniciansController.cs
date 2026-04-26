using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FireGuard.Application.Interfaces;

namespace FireGuard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TechniciansController : ControllerBase
{
    private readonly ITechnicianRepository _repo;

    public TechniciansController(ITechnicianRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await _repo.GetTechniciansAsync();
        return Ok(data);
    }
}