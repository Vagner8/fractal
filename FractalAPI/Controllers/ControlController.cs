using FractalAPI.Models;
using FractalAPI.Services;

using Microsoft.AspNetCore.Mvc;

namespace FractalAPI.Controllers
{
  [Route("api/control")]
  [ApiController]
  public class ControlController(IControlService cs) : ControllerBase
  {
    private readonly IControlService _cs = cs;

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] Fractal fractal)
    {
      await _cs.Add(fractal);
      return Ok(fractal);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] Fractal fractal)
    {
      await _cs.Update(fractal);
      return Ok();
    }


    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] Fractal fractal)
    {
      await _cs.Delete(fractal);
      return Ok(fractal);
    }
  }
}