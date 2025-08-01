using Carglass.TechnicalAssessment.Backend.BL;
using Microsoft.AspNetCore.Mvc;

namespace Carglass.TechnicalAssessment.Backend.Api.Controllers;

[ApiController]
[Route("healthCHeck")]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    public IActionResult KeepAlive()
    {
        return Ok();
    }
}
