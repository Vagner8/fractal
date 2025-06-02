using FractalAPI.Data;
using FractalAPI.Models;
using FractalAPI.Services.FractalService;
using Microsoft.AspNetCore.Mvc;


namespace FractalAPI.Controllers
{
  [Route("api/fractal")]
  [ApiController]
  public class FractalController(
    AppDbContext db,
    IFractalService fs) : ControllerBase
  {
    private readonly AppDbContext _db = db;
    private readonly IFractalService _fs = fs;

    [HttpGet]
    public async Task<ActionResult> Get(Guid id)
    {
      Fractal fractal = await _fs.GetFractalWithChildrenRecursively(id);
      return Ok(_fs.CreateFractalDto(fractal));
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] FractalDto[] fractalsDto)
    {
      _db.Fractals.AddRange(fractalsDto.Select(_fs.CreateFractal).ToList());

      foreach (var fractalDto in fractalsDto)
      {
        Fractal? parent = await _fs.FindFractal(fractalDto.ParentId);
      }

      await _db.SaveChangesAsync();
      return Ok(fractalsDto);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] FractalDto[] dto)
    {
      _db.Fractals.UpdateRange(dto.Select(_fs.CreateFractal));
      await _db.SaveChangesAsync();
      return Ok(dto);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] ICollection<FractalDto> fractalsDto)
    {
      foreach (var fractalDto in fractalsDto)
      {
        Fractal fractal = await _fs.GetFractalWithChildrenRecursively(fractalDto.Id);
        _fs.DeleteFractalChildrenRecursively(fractal.Fractals);
        _db.Fractals.Remove(fractal);
      }
      await _db.SaveChangesAsync();
      return Ok(fractalsDto);
    }
  }
}