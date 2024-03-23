using System.ComponentModel.DataAnnotations;

namespace EntityAPI.Models
{
    public class Entity
    {
        [Key] public Guid Id { get; set; }
        public required string Name { get; set; }
        public required ICollection<Sort> Sorts { get; set; }
        public required ICollection<Item> Items { get; set; }
    }

    public class EntityDto
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Sort>? Sorts { get; set; }
        public ICollection<Item>? Items { get; set; }
    }

    public class EntityBuilder
    {
        public static Entity ToEntity(EntityDto entityDto)
        {
            return new Entity
            {
                Name = entityDto.Name,
                Sorts = entityDto.Sorts ?? [],
                Items = entityDto.Items ?? [],
            };
        }
    }
}
