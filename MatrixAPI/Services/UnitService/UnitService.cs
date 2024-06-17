using MatrixAPI.Data;
using MatrixAPI.Dto;

namespace MatrixAPI.Services
{
  public class UnitService(AppDbContext db, IMapService maps) : IUnitService
  {
    private readonly AppDbContext _db = db;
    private readonly IMapService _maps = maps;

    public async Task Add(UnitDto dto, Guid? matrixId, Guid? unitId)
    {
      if (matrixId.HasValue)
      {
        await AddToMatrix(dto, matrixId.Value);
      }
      if (unitId.HasValue)
      {
        await AddToUnit(dto, unitId.Value);
      }
      await _db.SaveChangesAsync();
    }

    public async Task Delete(ICollection<UnitDto> dto)
    {
      var units = dto.Select(u => _maps.ToUnit(u));
      _db.Units.RemoveRange(units);
      await _db.SaveChangesAsync();
    }

    private async Task AddToMatrix(UnitDto dto, Guid id)
    {
      var parent = await _db.Matrixes.FindAsync(id) ?? throw new Exception("Parent matrix not found");
      var unit = _maps.ToUnit(dto);
      parent.Units.Add(unit);
      await _db.Units.AddRangeAsync(unit);
    }

    private async Task AddToUnit(UnitDto dto, Guid id)
    {
      var parent = await _db.Units.FindAsync(id) ?? throw new Exception("Parent unit not found");
      var unit = _maps.ToUnit(dto);
      parent.Units.Add(unit);
      await _db.Units.AddRangeAsync(unit);
    }
  }
}
