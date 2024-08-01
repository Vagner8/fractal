using Microsoft.AspNetCore.Mvc;
using FractalAPI.Services;
using FractalAPI.Dto;

namespace FractalAPI.Controllers
{
  [Route("api/fractal")]
  [ApiController]
  public class FractalController(IFractalService fs) : ControllerBase
  {
    private readonly IFractalService _fs = fs;

    [HttpGet]
    public async Task<ActionResult> Get(Guid id)
    {
      return Ok(await _fs.Get(id));
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] FractalDto dto)
    {
      await _fs.Add(dto);
      return Ok(dto);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] FractalDto dto)
    {
      await _fs.Update(dto);
      return Ok(dto);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] ICollection<FractalDto> dto)
    {
      await _fs.Delete(dto);
      return Ok(dto);
    }
  }
}
