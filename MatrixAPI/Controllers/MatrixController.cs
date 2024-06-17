using MatrixAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MatrixAPI.Controllers
{
  [Route("api/matrix")]
  [ApiController]
  public class MatrixController(IMatrixService ms, IMapService maps) : ControllerBase
  {
    private readonly IMatrixService _ms = ms;
    private readonly IMapService _maps = maps;

    [HttpGet]
    public async Task<ActionResult> Get(Guid id)
    {
      try
      {
        var matrix = await _ms.GetByIdAsync(id);
        return Ok(_maps.ToMatrixDto(matrix));
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }
  }
}
