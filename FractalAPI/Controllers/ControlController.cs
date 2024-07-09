using FractalAPI.Dto;
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
    public async Task<ActionResult> Add([FromBody] ControlDictionaryDto dto)
    {
      await _cs.Add(dto);
      return Ok(dto);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] ControlDictionaryDto dto)
    {
      await _cs.Update(dto);
      return Ok();
    }


    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] ControlDictionaryDto dto)
    {
      await _cs.Delete(dto);
      return Ok(dto);
    }
  }
}
