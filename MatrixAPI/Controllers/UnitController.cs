using Microsoft.AspNetCore.Mvc;
using MatrixAPI.Services;
using MatrixAPI.Dto;

namespace MatrixAPI.Controllers
{
  [Route("api/unit")]
  [ApiController]
  public class UnitController(IUnitService us) : ControllerBase
  {
    private readonly IUnitService _us = us;

    [HttpGet]
    public async Task<ActionResult> Get(Guid id)
    {
      return Ok(await _us.Get(id));
    }

    [HttpPost]
    public async Task<ActionResult> AddUnit([FromBody] UnitDto dto)
    {
      await _us.Add(dto);
      return Ok(dto);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] ICollection<UnitDto> dto)
    {
      await _us.Delete(dto);
      return Ok(dto);
    }
  }
}
