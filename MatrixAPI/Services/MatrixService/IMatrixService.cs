using MatrixAPI.Models;

namespace MatrixAPI.Services
{
  public interface IMatrixService
  {
    Task<Matrix> GetByIdAsync(Guid id); 
  }
}
