using Microsoft.AspNetCore.Mvc;
using MatrixAPI.Models;
using MatrixAPI.Services;
using MatrixAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace MatrixAPI.Controllers
{
  [Route("api/matrix")]
  [ApiController]
  public class MatrixController(
      AppDbContext db,
      IMapService map,
      ISaveService save,
      IFilterService filter,
      IMatrixService matrix,
      IResponseService response) : ControllerBase
  {
    private readonly AppDbContext _db = db;
    private readonly IMapService _map = map;
    private readonly ISaveService _save = save;
    private readonly IFilterService _filter = filter;
    private readonly IMatrixService _matrix = matrix;
    private readonly IResponseService _response = response;

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] List<MatrixDto> matricesDto)
    {
      try
      {
        var filtered = _filter.MatrixDto(matricesDto);
        if (filtered.MatricesToAdd.Count > 0)
        {
          await _db.Matrices.AddRangeAsync(filtered.MatricesToAdd);
        }
        if (filtered.MatricesToRemove.Count > 0)
        {
          _db.Controls.RemoveRange(filtered.MatricesToRemove.SelectMany(m => m.Controls).ToList());
          _db.Matrices.RemoveRange(filtered.MatricesToRemove);
        }
        if (filtered.UnitsToAdd.Count > 0)
        {
          await _db.Units.AddRangeAsync(filtered.UnitsToAdd);
        }
        if (filtered.UnitsToRemove.Count > 0)
        {
          _db.Units.RemoveRange(filtered.UnitsToRemove);
        }
        if (filtered.ControlsToAdd.Count > 0)
        {
          await _db.Controls.AddRangeAsync(filtered.ControlsToAdd);
        }
        if (filtered.ControlsToRemove.Count > 0)
        {
          _db.Controls.RemoveRange(filtered.ControlsToRemove);
        }
        if (filtered.ControlsToUpdate.Count > 0)
        {
          _db.Controls.UpdateRange(filtered.ControlsToUpdate);
        }
        await _save.SaveChangesAsync();
        var matrices = await _matrix.GetManyAsync();
        return Ok(_response.Data(matrices.Select(m => _map.ToMatrixDto(m))));
      }
      catch (Exception ex)
      {
        return BadRequest(_response.Error(ex.Message));
      }
    }

    [HttpGet("Matrix")]
    public async Task<ActionResult> FindById(Guid id)
    {
      try
      {
        var matrixes = await _matrix.GetOneAsync(id);
        return Ok(_response.Data(matrixes.Select(_map.ToMatrixDto)));
      }
      catch (Exception ex)
      {
        return BadRequest(_response.Error(ex.Message));
      }
    }

    [HttpGet("Matrixes")]
    public async Task<ActionResult> GetMany()
    {
      try
      {
        var matrixes = await _matrix.GetManyAsync();
        return Ok(_response.Data(matrixes.Select(_map.ToMatrixDto)));
      }
      catch (Exception ex)
      {
        return BadRequest(_response.Error(ex.Message));
      }
    }
  }
}
