using MatrixAPI.Data;
using MatrixAPI.Dto;
using MatrixAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MatrixAPI.Services
{
  public class ControlService(AppDbContext db, IMapService maps) : IControlService
  {
    private readonly AppDbContext _db = db;
    private readonly IMapService _maps = maps;

    public async Task Add(ControlDictionaryDto dto, Guid? matrixId = null, Guid? unitId = null)
    {
      await _db.Controls.AddRangeAsync(dto.Values.Select(v => _maps.ToControl(v, matrixId, unitId)));
      await _db.SaveChangesAsync();
    }

    public async Task Update(ControlDictionaryDto dto)
    {
      _db.Controls.UpdateRange(dto.Values.Select(v => _maps.ToControl(v)));
      await _db.SaveChangesAsync();
    }

    public async Task Delete(ControlDictionaryDto dto)
    {
      _db.Controls.RemoveRange(_maps.ToControls(dto));
      await _db.SaveChangesAsync();
    }
  }
}
