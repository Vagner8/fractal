using Microsoft.AspNetCore.Mvc;
using MatrixAPI.Services;
using MatrixAPI.Dto;
using MatrixAPI.Models;

namespace MatrixAPI.Controllers
{
  [Route("api/unit")]
  [ApiController]
  public class UnitController(IUnitService us) : ControllerBase
  {
    private readonly IUnitService _us = us;

    [HttpPost]
    public async Task<ActionResult> AddUnit([FromBody] UnitDto dto, Guid? matrixId = null, Guid? unitId = null)
    {
      try
      {
        await _us.Add(dto, matrixId, unitId);
        return Ok("Unit added successfully.");
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }
    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] ICollection<UnitDto> dto)
    {
      try
      {
        await _us.Delete(dto);
        return Ok();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }
  }
}
