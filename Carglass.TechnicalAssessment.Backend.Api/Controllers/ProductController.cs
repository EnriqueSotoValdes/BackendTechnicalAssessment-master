using Carglass.TechnicalAssessment.Backend.BL;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Carglass.TechnicalAssessment.Backend.Api.Controllers;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{

    private readonly IProductAppService _clientAppService;

    public ProductController(IProductAppService clientAppService)
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
    public IActionResult Create([FromBody] ProductDto dto)
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
    public IActionResult Update([FromBody] ProductDto dto)
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
    public IActionResult Delete([FromBody] ProductDto dto)
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
