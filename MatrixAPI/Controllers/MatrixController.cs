using Microsoft.AspNetCore.Mvc;
using MatrixAPI.Models;
using MatrixAPI.Services;
using MatrixAPI.Data;

namespace MatrixAPI.Controllers
{
  [Route("api/matrix")]
  [ApiController]
  public class MatrixController(
      AppDbContext db,
      IMapService map,
      ISaveService save,
      ITodoService todo,
      IMatrixService matrix,
      IResponseService response) : ControllerBase
  {
    private readonly AppDbContext _db = db;
    private readonly IMapService _map = map;
    private readonly ISaveService _save = save;
    private readonly ITodoService _todo = todo;
    private readonly IMatrixService _matrix = matrix;
    private readonly IResponseService _response = response;

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] MatrixDto dto)
    {
      try
      {
        var todo = _todo.Matrix(dto);
        await _matrix.UpdateAsync(todo);
        await _db.SaveChangesAsync();

        var matrix = await _matrix.GetAsync(dto.Id);
        var matrixDto = _map.ToMatrixDto(matrix);
        var response = _response.Data(matrixDto);
        return Ok(response);
      }
      catch (Exception ex)
      {
        return BadRequest(_response.Error(ex.Message));
      }
    }

    [HttpGet]
    public async Task<ActionResult> GetMany(Guid matrixId)
    {
      try
      {
        var matrix = await _matrix.GetAsync(matrixId);
        var matrixDto = _map.ToMatrixDto(matrix);
        return Ok(_response.Data(matrixDto));
      }
      catch (Exception ex)
      {
        return BadRequest(_response.Error(ex.Message));
      }
    }
  }
}
