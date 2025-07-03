using FractalAPI.ControlTools;
using FractalAPI.Data;
using FractalAPI.Dto;
using FractalAPI.Services;
using Microsoft.AspNetCore.Mvc;


namespace FractalAPI.Controllers
{
  [Route("api/control")]
  [ApiController]
  public class ControlController(AppDbContext db, IControlService cs) : ControllerBase
  {
    private readonly AppDbContext _db = db;
    private readonly IControlService _cs = cs;

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] ICollection<ControlDto> dto)
    {
      _db.Controls.AddRange(dto.Select(ControlMap.ToControl));
      await _db.SaveChangesAsync();
      return Ok(dto);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] ICollection<ControlDto> dto)
    {
      _db.Controls.RemoveRange(dto.Select(ControlMap.ToControl));
      await _db.SaveChangesAsync();
      return Ok(dto);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] ICollection<ControlDto> dto)
    {
      _db.Controls.UpdateRange(dto.Select(ControlMap.ToControl));
      await _db.SaveChangesAsync();
      return Ok(dto);
    }
  }
}
