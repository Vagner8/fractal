using Microsoft.AspNetCore.Mvc;
using EntityAPI.Data;
using EntityAPI.Models;

namespace EntityAPI.Controllers
{
    [Route("api/entity")]
    [ApiController]
    public class EntityController(AppDbContext db) : ControllerBase
    {
        private readonly AppDbContext _db = db;

        [HttpPost]
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

        [HttpPatch]
        public async Task<ActionResult<EntityDto>> Update([FromBody] EntityDto entityDto)
        {
            try
            {
                var existingEntity = await _db.Entities.FindAsync(entityDto.Id);
                if (existingEntity == null) return NotFound(ResponseBuilder.Error($"Entity id: {entityDto.Id}"));
                foreach (var fieldDto in entityDto.Fields)
                {
                    var existingField = existingEntity.Fields.FirstOrDefault(f => f.Id == fieldDto.Id);

                    if (existingField == null)
                    {
                        var newField = new Field
                        {
                            Name = fieldDto.Name,
                            Value = fieldDto.Value,
                            Entity = existingEntity
                        };
                        await _db.Fields.AddAsync(newField);
                        existingEntity.Fields.Add(newField);
                    }
                    else
                    {
                        existingField.Name = fieldDto.Name;
                        existingField.Value = fieldDto.Value;
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
    }
}
