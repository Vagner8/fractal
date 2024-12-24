using FractalAPI.Data;
using FractalAPI.Models;
using FractalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FractalAPI.Controllers
{
  [Route("api/control")]
  [ApiController]
  public class ControlController(AppDbContext db, IFractalService fs) : ControllerBase
  {
    private readonly AppDbContext _db = db;
    private readonly IFractalService _fs = fs;

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] ICollection<ControlDto> dto)
    {
      _db.Controls.AddRange(dto.Select(_fs.ToControl));
      await _db.SaveChangesAsync();
      return Ok(dto);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] ICollection<ControlDto> dto)
    {
      _db.Controls.RemoveRange(dto.Select(_fs.ToControl));
      await _db.SaveChangesAsync();
      return Ok(dto);
    }
  }
}
