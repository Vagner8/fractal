using Microsoft.AspNetCore.Mvc;
using FractalAPI.Services;
using FractalAPI.Dto;

namespace FractalAPI.Controllers
{
  [Route("api/fractal")]
  [ApiController]
  public class FractalController(IFractalController us) : ControllerBase
  {
    private readonly IFractalController _us = us;

    [HttpGet]
    public async Task<ActionResult> Get(Guid id)
    {
      return Ok(await _us.Get(id));
    }

    [HttpPost]
    public async Task<ActionResult> AddFractal([FromBody] FractalDto dto)
    {
      await _us.Add(dto);
      return Ok(dto);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] ICollection<FractalDto> dto)
    {
      await _us.Delete(dto);
      return Ok(dto);
    }
  }
}
