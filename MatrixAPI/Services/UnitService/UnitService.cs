using MatrixAPI.Data;
using MatrixAPI.Dto;
using MatrixAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MatrixAPI.Services
{
  public class UnitService(AppDbContext db, IMapService maps) : IUnitService
  {
    private readonly AppDbContext _db = db;
    private readonly IMapService _maps = maps;

    public async Task<UnitDto> Get(Guid unitId)
    {
      var unit = await _db.Units.Include(u => u.Controls).FirstOrDefaultAsync(u => u.Id == unitId) ?? throw new Exception("Unit not found");
      await LoadUnitsRecursively(unit);
      return _maps.ToUnitDto(unit);
    }

    private async Task LoadUnitsRecursively(Unit parent)
    {
      var children = await _db.Units.Where(u => u.ParentId == parent.Id).Include(u => u.Controls).ToListAsync();
      if (children.Count == 0) return;
      parent.Units = children;
      foreach (var child in children) await LoadUnitsRecursively(child);
    }

    public async Task Add(UnitDto dto)
    {
      var parent = await _db.Units.FindAsync(dto.ParentId) ?? throw new Exception("Parent unit not found");
      var unit = _maps.ToUnit(dto);
      parent.Units.Add(unit);
      await _db.Units.AddAsync(unit);
      await _db.SaveChangesAsync();
    }

    public async Task Delete(ICollection<UnitDto> dto)
    {
      var units = dto.Select(_maps.ToUnit);
      _db.Units.RemoveRange(units);
      await _db.SaveChangesAsync();
    }
  }
}
