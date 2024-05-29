using MatrixAPI.Data;
using MatrixAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MatrixAPI.Services
{
  public class MatrixService(AppDbContext db) : IMatrixService
  {
    private readonly AppDbContext _db = db;

    public async Task GroupsUpdate(Todo todo)
    {
      if (todo.GroupsToAdd.Count > 0)
      {
        await _db.Groups.AddRangeAsync(todo.GroupsToAdd);
      }
      if (todo.GroupsToRemove.Count > 0)
      {
        foreach (var group in todo.GroupsToRemove)
        {
          var groupToDelete = await _db.Groups
            .Include(g => g.Units)
            .Include(g => g.Controls)
            .FirstOrDefaultAsync(g => g.Id == group.Id);
          
          if (groupToDelete != null)
          {
            _db.Controls.RemoveRange(groupToDelete.Controls);
            _db.Units.RemoveRange(groupToDelete.Units);
            _db.Groups.Remove(groupToDelete);
          }
        }
      }
    }

    public async Task UnitsUpdate(Todo todo)
    {
      if (todo.UnitsToAdd.Count > 0)
      {
        await _db.Units.AddRangeAsync(todo.UnitsToAdd);
      }
      if (todo.UnitsToRemove.Count > 0)
      {
        _db.Units.RemoveRange(todo.UnitsToRemove);
      }
    }
    public async Task ControlsUpdate(Todo todo)
    {
      if (todo.ControlsToAdd.Count > 0)
      {
        await _db.Controls.AddRangeAsync(todo.ControlsToAdd);
      }
      if (todo.ControlsToUpdate.Count > 0)
      {
        _db.Controls.UpdateRange(todo.ControlsToUpdate);
      }
      if (todo.ControlsToRemove.Count > 0)
      {
        _db.Controls.RemoveRange(todo.ControlsToRemove);
      }
    }

    public async Task<Matrix> Get(Guid matrixId)
    {
      return await _db.Matrixes
        .Include(m => m.Controls)
        .Include(m => m.Groups)
          .ThenInclude(g => g.Units)
            .ThenInclude(u => u.Controls)
        .Include(m => m.Groups)
          .ThenInclude(g => g.Controls)
        .FirstAsync(m => m.Id == matrixId);
    }
  }

  public interface IMatrixService
  {
    Task GroupsUpdate(Todo todo);
    Task UnitsUpdate(Todo todo);
    Task ControlsUpdate(Todo todo);
    Task<Matrix> Get(Guid matrixId);
  }
}
