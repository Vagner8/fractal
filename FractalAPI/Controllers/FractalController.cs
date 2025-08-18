using FractalAPI.Data;
using FractalAPI.Dto;
using FractalAPI.Models;
using FractalAPI.Services;
using Microsoft.AspNetCore.Mvc;


namespace FractalAPI.Controllers
{
  [Route("api/fractal")]
  [ApiController]
  public class FractalController(
    AppDbContext db,
    IMapService ms,
    IFractalService fs) : ControllerBase
  {
    private readonly AppDbContext _db = db;
    private readonly IMapService _ms = ms;
    private readonly IFractalService _fs = fs;

    [HttpGet]
    public async Task<ActionResult> Get(string cursor)
    {
      Fractal fractal = await _fs.GetWithChildrenRecursively(cursor);
      return Ok(_ms.ToFractalDto(fractal));
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] ICollection<ICollection<FractalDto>> payload)
    {
      await _db.SaveChangesAsync();
      return Ok(payload);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] FractalDto[] dto)
    {
      await _db.SaveChangesAsync();
      return Ok(dto);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] ICollection<FractalDto> fractalsDto)
    {
      foreach (var fractalDto in fractalsDto)
      {
        Fractal fractal = await _fs.GetWithChildrenRecursively(fractalDto.Cursor);
        _fs.DeleteWithChildrenRecursively(fractal.Children);
        _db.Fractals.Remove(fractal);
      }
      await _db.SaveChangesAsync();
      return Ok(fractalsDto);
    }
  }
}