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
                var newEntity = new Entity
                {
                    Name = entityDto.Name
                };
                var fields = entityDto.Fields.Select(field => new Field
                {
                    Name = field.Name,
                    Value = field.Value,
                    Entity = newEntity,
                });
                newEntity.Fields = fields.ToList();
                await _db.Entities.AddAsync(newEntity);
                await _db.SaveChangesAsync();
                await _db.Entry(newEntity).Collection(entit => entit.Fields).LoadAsync();
                return Ok(ResponseBuilder.Data(newEntity));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBuilder.Error(ex.Message));
            }
        }

        [HttpPatch("update")]
        public async Task<ActionResult<EntityDto>> Update([FromBody] EntityDto entityDto)
        {
            try
            {
                var existingEntity = await _db.Entities
                    .Include(e => e.Fields)
                    .FirstOrDefaultAsync(e => e.Id == entityDto.Id);
                if (existingEntity == null) return NotFound(ResponseBuilder.Error($"Entity id: {entityDto.Id}"));

                foreach (var fieldDto in entityDto.Fields)
                {
                    if (fieldDto.Action == FieldAction.Create)
                    {
                        existingEntity.Fields.Add(new Field
                        {
                            Name = fieldDto.Name,
                            Value = fieldDto.Value,
                            Entity = existingEntity
                        });
                        _db.Entry(existingEntity.Fields.Last()).State = EntityState.Added;
                    }
                    if (fieldDto.Action == FieldAction.Update)
                    {
                        var existingField = existingEntity.Fields.FirstOrDefault(f => f.Id == fieldDto.Id);
                        if (existingField == null) return NotFound(ResponseBuilder.Error($"Update field: {fieldDto.Id}"));
                        existingField.Name = fieldDto.Name;
                        existingField.Value = fieldDto.Value;
                        _db.Entry(existingField).State = EntityState.Modified;
                    }
                    if (fieldDto.Action == FieldAction.Delete)
                    {
                        var existingField = existingEntity.Fields.FirstOrDefault(f => f.Id == fieldDto.Id);
                        if (existingField == null) return NotFound(ResponseBuilder.Error($"Delete field: {fieldDto.Id}"));
                        existingEntity.Fields.Remove(existingField);
                        _db.Entry(existingField).State = EntityState.Deleted;
                    }
                }
                await _db.SaveChangesAsync();
                return Ok(ResponseBuilder.Data(existingEntity));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBuilder.Error(ex.Message));
            }
        }


        [HttpGet("all")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await _db.Entities.Include(e => e.Fields).ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBuilder.Error(ex.Message));
            }
        }

        [HttpGet("one")]
        public async Task<ActionResult> GetOne(Guid entityId)
        {
            try
            {
                var entity = await _db.Entities.Include(e => e.Fields).FirstOrDefaultAsync(e => e.Id == entityId);
                if (entity == null) return NotFound(ResponseBuilder.Error($"Find entity: {entityId}"));
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBuilder.Error(ex.Message));
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(List<Guid> entityIds)
        {
            try
            {
                foreach (var entityId in entityIds)
                {
                    var entityToDelete = await _db.Entities.FirstOrDefaultAsync(e => e.Id == entityId);
                    if (entityToDelete == null) return NotFound(ResponseBuilder.Error($"Delete entity: {entityId}"));
                    _db.Entities.Remove(entityToDelete);
                }
                await _db.SaveChangesAsync();
                return Ok(ResponseBuilder.Data(entityIds));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBuilder.Error(ex.Message));
            }
        }
    }
}
