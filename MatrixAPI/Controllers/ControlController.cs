using MatrixAPI.Dto;
using MatrixAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MatrixAPI.Controllers
{
  [Route("api/control")]
  [ApiController]
  public class ControlController(IControlService cs) : ControllerBase
  {
    private readonly IControlService _cs = cs;

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] ControlDictionaryDto dto, Guid? matrixId, Guid? unitId)
    {
      try
      {
        await _cs.Add(dto, matrixId, unitId);
        return Ok();
      }
      catch (Exception ex) {
        return BadRequest(ex);
      }
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] ControlDictionaryDto dto)
    {
      try
      {
        await _cs.Update(dto);
        return Ok();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }


    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] ControlDictionaryDto dto)
    {
      try
      {
        await _cs.Delete(dto);
        return Ok();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }
  }
}
