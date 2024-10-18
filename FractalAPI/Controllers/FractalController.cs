using FractalAPI.Models;
using FractalAPI.Services;

using Microsoft.AspNetCore.Mvc;

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
      var fractal = await _fs.Get(id);
      return Ok(fractal);
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] Fractal fractal)
    {
      await _fs.Add(fractal);
      return Ok(fractal);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] Fractal fractal)
    {
      await _fs.Update(fractal);
      return Ok(fractal);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] ICollection<Fractal> fractal)
    {
      await _fs.Delete(fractal);
      return Ok(fractal);
    }
  }
}