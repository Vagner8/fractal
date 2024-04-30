using MatrixAPI.Data;
using MatrixAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MatrixAPI.Services
{
  public class MatrixService(AppDbContext db) : IMatrixService
  {
    private readonly AppDbContext _db = db;

    public async Task<List<Matrix>> GetManyAsync()
    {
      return await _db.Matrices
        .Include(m => m.Units)
        .ThenInclude(u => u.Controls)
        .Include(m => m.Controls)
        .ToListAsync();
    }

    public async Task<List<Matrix>> GetOneAsync(Guid id)
    {
      return await _db.Matrices
        .Include(m => m.Units)
        .Include(m => m.Controls)
        .Where(m => m.Id == id)
        .ToListAsync();
    }
  }

  public interface IMatrixService
  {
    Task<List<Matrix>> GetManyAsync();
    Task<List<Matrix>> GetOneAsync(Guid id);
  }
}
