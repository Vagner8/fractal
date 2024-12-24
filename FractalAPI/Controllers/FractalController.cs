using FractalAPI.Data;
using FractalAPI.Models;
using FractalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FractalAPI.Controllers
{
  [Route("api/fractal")]
  [ApiController]
  public class FractalController(AppDbContext db, IFractalService fs) : ControllerBase
  {
    private readonly AppDbContext _db = db;
    private readonly IFractalService _fs = fs;

    [HttpGet]
    public async Task<ActionResult> Get(Guid id)
    {
      var fractal = await _db.Fractals.Include(u => u.Controls)
        .FirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception("Fractal not found");
      await _fs.LoadFractalsRecursively(fractal);
      return Ok(_fs.ToFractalDto(fractal));
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] FractalDto[] dto)
    {
      _db.Fractals.AddRange(dto.Select(_fs.ToFractal).ToList());
      await _db.SaveChangesAsync();
      return Ok(dto);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] FractalDto[] dto)
    {
      _db.Fractals.UpdateRange(dto.Select(_fs.ToFractal));
      await _db.SaveChangesAsync();
      return Ok(dto);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] ICollection<FractalDto> dto)
    {
      _db.Fractals.RemoveRange(dto.Select(_fs.ToFractal));
      await _db.SaveChangesAsync();
      return Ok(dto);
    }
  }
}