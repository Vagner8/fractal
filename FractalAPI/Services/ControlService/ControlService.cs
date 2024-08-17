using FractalAPI.Data;
using FractalAPI.Dto;

namespace FractalAPI.Services
{
  public class ControlService(AppDbContext db, IMapService maps) : IControlService
  {
    private readonly AppDbContext _db = db;
    private readonly IMapService _maps = maps;

    public async Task Add(ControlDictionaryDto dto)
    {
      var controls = dto.Values.Select(_maps.ToControl);
      await _db.Controls.AddRangeAsync(controls);
      await _db.SaveChangesAsync();
    }

    public async Task Update(ControlDictionaryDto dto)
    {
      _db.Controls.UpdateRange(dto.Values.Select(_maps.ToControl));
      await _db.SaveChangesAsync();
    }

    public async Task Delete(ControlDictionaryDto dto)
    {
      _db.Controls.RemoveRange(_maps.ToControls(dto));
      await _db.SaveChangesAsync();
    }
  }
}