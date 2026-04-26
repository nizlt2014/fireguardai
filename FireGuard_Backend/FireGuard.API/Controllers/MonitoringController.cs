using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FireGuard.Application.Interfaces;

namespace FireGuard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MonitoringController : ControllerBase
{
    private readonly IMonitoringRepository _repo;

    public MonitoringController(IMonitoringRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await _repo.GetMonitoringAsync();
        return Ok(data);
    }
}