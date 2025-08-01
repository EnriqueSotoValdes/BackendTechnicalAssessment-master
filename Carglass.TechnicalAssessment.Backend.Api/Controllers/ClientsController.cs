using Carglass.TechnicalAssessment.Backend.BL;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Carglass.TechnicalAssessment.Backend.Api.Controllers;

[ApiController]
[Route("clients")]
public class ClientsController : ControllerBase
{
    private readonly IClientAppService _clientAppService;

    public ClientsController(IClientAppService clientAppService)
    {
        this._clientAppService = clientAppService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_clientAppService.GetAll());
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var result = _clientAppService.GetById(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] ClientDto dto)
    {
        try
        {
            _clientAppService.Create(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    public IActionResult Update([FromBody] ClientDto dto)
    {
        try
        {
            _clientAppService.Update(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] ClientDto dto)
    {
        try
        {
            _clientAppService.Delete(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}