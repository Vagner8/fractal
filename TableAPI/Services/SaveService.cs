using MatrixAPI.Data;
using MatrixAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MatrixAPI.Services
{
  public class SaveService(AppDbContext db) : ISaveService
  {
    private readonly AppDbContext _db = db;

    public async Task SaveChangesAsync()
    {
      var maxAttempts = 5;
      var attempts = 0;
      var saved = false;
      while (!saved)
      {
        try
        {
          await _db.SaveChangesAsync();
          saved = true;
        }
        catch (DbUpdateConcurrencyException ex)
        {
          attempts++;
          if (attempts > maxAttempts)
          {
            throw new Exception("Maximum retry attempts exceeded");
          }
          foreach (var entry in ex.Entries)
          {
            var name = entry.Metadata.Name;
            if (entry.Entity is Control)
            {
              var databaseValues = entry.GetDatabaseValues()
                  ?? throw new Exception($"Entity {name} was deleted by another user");
              entry.OriginalValues.SetValues(databaseValues);
              await _db.SaveChangesAsync();
            }
            else
            {
              throw new NotSupportedException($"Can't handle concurrency conflicts for {name}");
            }
          }
        }
      }
    }
  }

  public interface ISaveService
  {
    Task SaveChangesAsync();
  }
}
