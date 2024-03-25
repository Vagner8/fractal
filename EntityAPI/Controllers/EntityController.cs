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

        [HttpPatch("update")]
        public async Task<ActionResult> Update([FromBody] EntityDto entityDto)
        {
            try
            {
                var entity = _db.Entities
                    .Include(e => e.Items)
                    .ThenInclude(i => i.Fields)
                    .FirstOrDefault(e => e.Name == entityDto.Name);
                if (entity == null) return NotFound(ResponseBuilder.Error($"Update entity: {entityDto.Name}"));
                entity.Columns = EntityBuilder.ToColumns(entityDto.Columns);
                foreach (var itemDto in entityDto.Items)
                {
                    switch (itemDto.Actions)
                    {
                        case Actions.Add:
                            entity.Items.Add(ItemBuilder.ToItem(itemDto));
                            break;
                        case Actions.Update:
                            var itemToUpdate = entity.Items.SingleOrDefault(i => i.Id == itemDto.Id);
                            if (itemToUpdate == null) return NotFound(ResponseBuilder.Error($"Update item: {itemDto.Id}"));
                            entity.Items.Remove(itemToUpdate);
                            entity.Items.Add(ItemBuilder.ToItem(itemDto));
                            break;
                        case Actions.Delete:
                            var itemToDelete = entity.Items.SingleOrDefault(i => i.Id == itemDto.Id);
                            if (itemToDelete == null) return NotFound(ResponseBuilder.Error($"Delete item: {itemDto.Id}"));
                            entity.Items.Remove(itemToDelete);
                            break;
                        default:
                            return BadRequest(ResponseBuilder.Error($"Unknown action: {itemDto.Actions} for item: {itemDto.Id}"));
                    }
                }
                await _db.SaveChangesAsync();
                return Ok(ResponseBuilder.Data(entity));
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

        [HttpGet("one")]
        public async Task<ActionResult> GetOne(string name)
        {
            try
            {
                var entity = await _db.Entities
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
    }
}
