using MatrixAPI.Data;
using MatrixAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MatrixAPI.Services
{
  public class MatrixService(AppDbContext db) : IMatrixService
  {
    private readonly AppDbContext _db = db;

    public async Task<Matrix> Get(Guid matrixId)
    {
      return await _db.Matrixes
        .Include(m => m.Groups)
          .ThenInclude(g => g.Controls)
        .Include(m => m.Controls)
        .FirstAsync(m => m.Id == matrixId);
    }
  }

  public interface IMatrixService
  {
    Task<Matrix> Get(Guid matrixId);
  }
}
