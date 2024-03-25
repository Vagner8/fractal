using System.ComponentModel.DataAnnotations;

namespace EntityAPI.Models
{
    public class Entity
    {
        [Key] public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Columns { get; set; }
        public required ICollection<Item> Items { get; set; }
    }

    public class EntityDto
    {
        public Guid? Id { get; set; }
        public required string Name { get; set; }
        public required List<string> Columns { get; set; }
        public required List<ItemDto> Items { get; set; }
    }

    public class EntityBuilder
    {
        public static Entity ToEntity(EntityDto entityDto)
        {
            return new Entity
            {
                Name = entityDto.Name,
                Columns = ToColumns(entityDto.Columns),
                Items = entityDto.Items.Select(i => ItemBuilder.ToItem(i)).ToList(),
            };
        }

        public static string ToColumns(List<string> columns)
        {
            return string.Join(":", columns);
        }
    }
}
