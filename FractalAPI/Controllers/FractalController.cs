using FractalAPI.Constants;
using FractalAPI.Data;
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
    IGetService gs,
    IDeleteService ds) : ControllerBase
  {
    private readonly AppDbContext _db = db;
    private readonly IMapService _ms = ms;
    private readonly IGetService _gs = gs;
    private readonly IDeleteService _ds = ds;

    [HttpGet]
    public async Task<ActionResult> Get(Guid id)
    {
      Fractal fractal = await _gs.GetFractalWithChildrenRecursively(id);
      return Ok(_ms.ToFractalDto(fractal));
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromBody] FractalDto[] fractalsDto)
    {
      _db.Fractals.AddRange(fractalsDto.Select(_ms.ToFractal).ToList());

      foreach (var fractalDto in fractalsDto)
      {
        Fractal? parent = await _gs.FindFractal(fractalDto.ParentId);
      }

      await _db.SaveChangesAsync();
      return Ok(fractalsDto);
    }

    [HttpPut]
    public async Task<ActionResult> Update([FromBody] FractalDto[] dto)
    {
      _db.Fractals.UpdateRange(dto.Select(_ms.ToFractal));
      await _db.SaveChangesAsync();
      return Ok(dto);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete([FromBody] ICollection<FractalDto> fractalsDto)
    {
      foreach (var fractalDto in fractalsDto)
      {
        Fractal fractal = await _gs.GetFractalWithChildrenRecursively(fractalDto.Id);
        Fractal? parent = await _gs.FindFractal(fractal.ParentId);

        if (parent != null)
        {
          Control? parentSort = parent.FindControl(SplitIndicators.Sort);
          parentSort?.Remove(fractal.GetControl(Indicators.Cursor).Data);
        }

        _ds.DeleteFractalChildrenRecursively(fractal.Fractals);
        _db.Fractals.Remove(fractal);
      }
      await _db.SaveChangesAsync();
      return Ok(fractalsDto);
    }
  }
}