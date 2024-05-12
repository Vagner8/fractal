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
      IWorkService work,
      IMatrixService matrix,
      IResponseService response) : ControllerBase
  {
    private readonly AppDbContext _db = db;
    private readonly IMapService _map = map;
    private readonly ISaveService _save = save;
    private readonly IWorkService _work = work;
    private readonly IMatrixService _matrix = matrix;
    private readonly IResponseService _response = response;

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] MatrixDto matrix, Guid matrixId)
    {
      try
      {
        var work = _work.OnInit(matrix);

        if (work.MatrixToAdd != null) await _db.Matrixes.AddAsync(work.MatrixToAdd);

        if (work.GroupsToAdd.Count > 0) await _db.Groups.AddRangeAsync(work.GroupsToAdd);
        if (work.GroupsToRemove.Count > 0) _db.Groups.RemoveRange(work.GroupsToAdd);

        if (work.UnitsToAdd.Count > 0) await _db.Units.AddRangeAsync(work.UnitsToAdd);
        if (work.UnitsToRemove.Count > 0) _db.Units.RemoveRange(work.UnitsToRemove);

        if (work.ControlsToAdd.Count > 0) await _db.Controls.AddRangeAsync(work.ControlsToAdd);
        if (work.ControlsToRemove.Count > 0) _db.Controls.RemoveRange(work.ControlsToRemove);
        if (work.ControlsToUpdate.Count > 0) _db.Controls.UpdateRange(work.ControlsToUpdate);

        await _save.SaveChangesAsync();
        var matrixDto = _map.ToMatrixDto(await _matrix.Get(matrixId));
        return Ok(_response.Data(matrixDto));
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
        var matrix = await _matrix.Get(matrixId);
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
