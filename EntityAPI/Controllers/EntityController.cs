using Microsoft.AspNetCore.Mvc;
using EntityAPI.Data;
using EntityAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityAPI.Controllers
{
    [Route("api/entity")]
    [ApiController]
    public class EntityController(AppDbContext db) : ControllerBase
    {
        private readonly AppDbContext _db = db;

        [HttpPost("create")]
        public async Task<ActionResult<EntityDto>> Create([FromBody] EntityDto entityDto)
        {
            try
            {
                var newEntity = EntityBuilder.ToEntity(entityDto);
                await _db.Entities.AddAsync(newEntity);
                await _db.SaveChangesAsync();
                return Ok(ResponseBuilder.Data(newEntity));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBuilder.Error(ex.Message));
            }
        }

        [HttpGet("one")]
        public async Task<ActionResult> GetOne(string name)
        {
            try
            {
                var entity = await _db.Entities
                    .Include(e => e.Sorts)
                    .Include(e => e.Items)
                    .ThenInclude(i => i.Fields)
                    .FirstOrDefaultAsync(e => e.Name == name);
                if (entity == null) return NotFound(ResponseBuilder.Error($"Find entity: {name}"));
                return Ok(ResponseBuilder.Data(entity));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBuilder.Error(ex.Message));
            }
        }

        [HttpPatch("update")]
        public async Task<ActionResult> Update([FromBody] EntityDto entityDto)
        {
            try
            {
                var entity = _db.Entities
                    .Include(e => e.Sorts)
                    .Include(e => e.Items)
                    .ThenInclude(i => i.Fields)
                    .FirstOrDefault(e => e.Name == entityDto.Name);
                if (entity == null) return NotFound(ResponseBuilder.Error($"Update entity: {entityDto.Name}"));
                if (entityDto.Sorts != null)
                {
                    entity.Sorts = entityDto.Sorts;
                    _db.Entry(entity.Sorts).State = EntityState.Modified;
                }
                if (entityDto.Items != null)
                {
                    foreach (var item in entityDto.Items)
                    {
                        var itemToUpdate = entity.Items.First(i => i.Id == item.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBuilder.Error(ex.Message));
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(string name)
        {
            try
            {
                var entity = await _db.Entities.FirstOrDefaultAsync(e => e.Name == name);
                if (entity == null) return NotFound(ResponseBuilder.Error($"Delete entity: {name}"));
                _db.Entities.Remove(entity);
                await _db.SaveChangesAsync();
                return Ok(ResponseBuilder.Data(entity));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBuilder.Error(ex.Message));
            }
        }
    }
}
