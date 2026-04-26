using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FireGuard.Application.Interfaces;

namespace FireGuard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AIInsightsController : ControllerBase
{
    private readonly IAIInsightsService _service;

    public AIInsightsController(IAIInsightsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetAsync());
    }
}