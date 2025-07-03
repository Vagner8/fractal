using FractalAPI.Data;

namespace FractalAPI.Services
{
  public class ControlService(AppDbContext db) : IControlService
  {
    private readonly AppDbContext _db = db;
  }
}
