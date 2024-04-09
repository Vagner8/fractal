using Microsoft.AspNetCore.Mvc;
using MatrixAPI.Models;
using MatrixAPI.Services;

namespace MatrixAPI.Controllers
{
    [Route("api/matrix")]
    [ApiController]
    public class MatrixController(IMapService map, IMatrixService matrix, IResponseService response) : ControllerBase
    {
        private readonly IMapService _map = map;
        private readonly IMatrixService _matrix = matrix;
        private readonly IResponseService _response = response;

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] List<MatrixDto> matricesDto)
        {
            try
            {
                foreach (var matrixDto in matricesDto)
                {
                    if (matrixDto.Id == null) return Ok(await _matrix.Add(matrixDto));
                    return Ok(await _matrix.Update(matrixDto));
                }
                return Ok(_response.Data(new List<MatrixDto>()));
            }
            catch (Exception ex)
            {
                return BadRequest(_response.Error(ex.Message));
            }
        }

        [HttpGet("one")]
        public async Task<ActionResult> GetOne(Guid id)
        {
            try
            {
                var matrix = await _matrix.GetMatrix(id);
                return Ok(_response.Data(_map.ToMatrixDto(matrix)));
            } catch (Exception ex)
            {
                return BadRequest(_response.Error(ex.Message));
            }
        }

        [HttpGet("many")]
        public async Task<ActionResult> GetMany()
        {
            try
            {
                var matrixs = await _matrix.GetMatrices();
                return Ok(_response.Data(matrixs.Select(_map.ToMatrixDto)));
            }
            catch (Exception ex)
            {
                return BadRequest(_response.Error(ex.Message));
            }
        }
    }
}
