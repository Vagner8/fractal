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
    public async Task<ActionResult> Add([FromBody] FractalDto dto)
    {
      Fractal parent = await _db.Fractals.FindAsync(dto.ParentId) ?? throw new Exception("Parent fractal not found");
      Fractal newFractal = _fs.ToFractal(dto);
      parent.Fractals.Add(newFractal);
      await _db.Fractals.AddAsync(newFractal);
      await _db.SaveChangesAsync();
      return Ok(_fs.ToFractalDto(newFractal));
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] FractalDto dto)
    {
      _db.Fractals.Update(_fs.ToFractal(dto));
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