using MatrixAPI.Data;
using MatrixAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MatrixAPI.Services
{
  public class MatrixService(AppDbContext db) : IMatrixService
  {
    private readonly AppDbContext _db = db;
    public async Task<Matrix> GetByIdAsync(Guid id)
    {
      return await _db.Matrixes
        .Include(m => m.Controls)
        .Include(m => m.Units)
            .ThenInclude(u => u.Controls)
        .Include(m => m.Units)
            .ThenInclude(u => u.Units)
                .ThenInclude(subUnit => subUnit.Controls)
        .FirstAsync(m => m.Id == id);
    }
  }
}
