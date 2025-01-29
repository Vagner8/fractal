using FractalAPI.Data;
using FractalAPI.Models;
using FractalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FractalAPI.Controllers
{
  [Route("api/control")]
  [ApiController]
  public class ControlController(AppDbContext db, IMapService ms) : ControllerBase
  {
    private readonly AppDbContext _db = db;
    private readonly IMapService _ms = ms;

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] ICollection<ControlDto> dto)
    {
      _db.Controls.AddRange(dto.Select(_ms.ToControl));
      await _db.SaveChangesAsync();
      return Ok(dto);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] ICollection<ControlDto> dto)
    {
      _db.Controls.RemoveRange(dto.Select(_ms.ToControl));
      await _db.SaveChangesAsync();
      return Ok(dto);
    }
  }
}
