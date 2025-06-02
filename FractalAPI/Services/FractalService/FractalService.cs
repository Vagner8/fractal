using FractalAPI.Data;
using FractalAPI.Models;
using FractalAPI.Services.ControlService;
using Microsoft.EntityFrameworkCore;

namespace FractalAPI.Services.FractalService
{
  public class FractalService(AppDbContext db, IControlService cs) : IFractalService
  {
    private readonly AppDbContext _db = db;
    private readonly IControlService _cs = cs;

    public async Task<Fractal?> FindFractal(Guid? id)
    {
      return await _db.Fractals
        .Include(f => f.Controls)
        .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Fractal> GetFractal(Guid? id)
    {
      return await FindFractal(id) ?? throw new Exception($"Unable to get fractal by id: {id}");
    }

    public async Task<Fractal> GetFractalWithChildren(Guid id)
    {
      return await _db.Fractals
        .Include(f => f.Controls)
        .Include(f => f.Fractals)
        .ThenInclude(f => f.Controls)
        .FirstOrDefaultAsync(f => f.Id == id)
        ?? throw new Exception($"Unable to get fractal with children by id: {id}");
    }

    public Fractal CreateFractal(FractalDto dto)
    {
      return new Fractal
      {
        Id = dto.Id,
        ParentId = dto.ParentId,
        Fractals = dto.Fractals?.Values.Select(CreateFractal).ToList() ?? [],
        Controls = dto.Controls.Values.Select(_cs.CreateControl).ToList()
      };
    }

    public FractalDto CreateFractalDto(Fractal fractal)
    {
      var childFractalsDto = new Dictionary<string, FractalDto>();
      var controlsDto = fractal.Controls.ToDictionary(c => c.Indicator, _cs.CreateControlDto);

      foreach (var child in fractal.Fractals)
      {
        var cursorControl = child.Controls.FirstOrDefault(c => c.Indicator == "Cursor");
        if (cursorControl == null)
          continue;

        childFractalsDto[cursorControl.Data] = CreateFractalDto(child);
      }

      return new FractalDto
      {
        Id = fractal.Id,
        ParentId = fractal.ParentId,
        Fractals = childFractalsDto.Count > 0 ? childFractalsDto : null,
        Controls = controlsDto,
      };
    }

    public async Task<Fractal> GetFractalWithChildrenRecursively(Guid id)
    {
      Fractal fractal = await GetFractalWithChildren(id);

      if (fractal.Fractals != null)
      {
        foreach (Fractal child in fractal.Fractals)
        {
          child.Fractals = (await GetFractalWithChildrenRecursively(child.Id)).Fractals;
        }
      }

      return fractal;
    }

    public void DeleteFractalChildrenRecursively(ICollection<Fractal> fractals)
    {
      foreach (var fractal in fractals)
      {
        if (fractal.Fractals != null) DeleteFractalChildrenRecursively(fractal.Fractals);
        _db.Fractals.RemoveRange(fractals);
      }
    }
  }
}
