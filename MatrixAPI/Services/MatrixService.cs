using MatrixAPI.Data;
using MatrixAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MatrixAPI.Services
{
  public class MatrixService(AppDbContext db) : IMatrixService
  {
    private readonly AppDbContext _db = db;

    public async Task UpdateAsync(Todo todo)
    {
      await Task.WhenAll(UpdateGroupsAsync(todo), UpdateUnitsAsync(todo), UpdateControlsAsync(todo));
    }

    public async Task<Matrix> GetAsync(Guid matrixId)
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

    private async Task UpdateGroupsAsync(Todo todo)
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

    private async Task UpdateUnitsAsync(Todo todo)
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

    private async Task UpdateControlsAsync(Todo todo)
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
  }

  public interface IMatrixService
  {
    Task UpdateAsync(Todo todo);
    Task<Matrix> GetAsync(Guid matrixId);
  }
}
